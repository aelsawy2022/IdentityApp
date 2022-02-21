using SchoolManagement.Persistance.Data.Entities;
using IdentityApplication.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagement.Persistance.Data;
using static SchoolManagement.Models.Models.Enums;
using SchoolManagement.Core.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using Serilog.Context;
using IdentityApplication.Middlewares;
using SchoolManagement.Infrastructure.CustomFilters;
using Hangfire;
using Hangfire.SqlServer;
using SchoolManagement.Core.Services.Interfaces;

namespace IdentityApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddDefaultUI()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = Configuration["Jwt:Issuer"],
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // extension method to register services
            services.RegisterRepositories();
            services.RegisterServices();
            services.RegisterCustomFilters();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSuperAdmin",
                     policy => policy.RequireRole(SystemRoles.SuperAdmin.ToString()));
            });

            services.AddSwaggerGen(
                swagger =>
                {
                    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
                }
                );
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ActivityLoggingFilter));
            });
            services.AddRazorPages();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHangfireDashboard();
            });

            //app.UseWhen(context => context.Request.Path.Value.StartsWith("/api"), subBranch =>
            //{
            //    subBranch.UseAuthenticationOverride(JwtBearerDefaults.AuthenticationScheme);
            //});
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management Api");
                //c.RoutePrefix = string.Empty;
            });

            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate("RemoveUsersActivitiesThatHasBeenOveraMonth",
                () => serviceProvider.GetService<IActivityLogService>().RemoveUsersActivitiesThatHasBeenOveraMonth(),
                Cron.Daily(23));
        }
    }
}
