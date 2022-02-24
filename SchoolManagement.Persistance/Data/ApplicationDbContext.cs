using SchoolManagement.Persistance.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
                                                          UserRole, IdentityUserLogin<Guid>,
                                                          IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClassUser>().HasKey(cu => new { cu.ClassId, cu.UserId, cu.UserTypeId, cu.SeasonId });
            modelBuilder.Entity<ClassUser>()
                .HasOne<User>(cu => cu.User)
                .WithMany(s => s.ClassUsers)
                .HasForeignKey(cu => cu.UserId);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ActivityClass>().HasKey(ac => new { ac.ActivityId, ac.ClassId });
            modelBuilder.Entity<ActivityUserType>().HasKey(aut => new { aut.ActivityId, aut.UserTypeId });

            modelBuilder.Entity<Role>().HasData(
                            new Role { Id = Guid.NewGuid(), Active = true, Name = "Admin", NormalizedName = "ADMIN" },
                            new Role { Id = Guid.NewGuid(), Active = true, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" });

            modelBuilder.Entity<UserType>().HasData(
                new UserType() { Id = Guid.NewGuid(), Name = "Student", CreationDate = DateTime.Now, Active = true },
                new UserType() { Id = Guid.NewGuid(), Name = "Teacher", CreationDate = DateTime.Now, Active = true },
                new UserType() { Id = Guid.NewGuid(), Name = "Manager", CreationDate = DateTime.Now, Active = true });

            modelBuilder.Entity<Governorate>().HasData(
                            new Governorate { Id = Guid.NewGuid(), Name = "Cairo", CreationDate = DateTime.Now },
                            new Governorate { Id = Guid.NewGuid(), Name = "Giza", CreationDate = DateTime.Now });

            modelBuilder.Entity<User>().HasData(
                            new User { 
                                Id = Guid.NewGuid(), 
                                Name = "Admin", 
                                UserName = "admin@school.com",
                                NormalizedUserName = "ADMIN@SCHOOL.COM",
                                Email = "admin@school.com",
                                NormalizedEmail = "ADMIN@SCHOOL.COM",
                                EmailConfirmed = true,
                                PasswordHash = "AQAAAAEAACcQAAAAEBufQAQJbYDau/j+n+KO6uup6jdG4PwIXKoCyUCE3ctCHNDSJkWl5U4HJxmNIJ6EEw==",
                                SecurityStamp = "MHERALYVWRDCTGRJYR4MHFEK77FFQ6JU",
                                ConcurrencyStamp = "b9bf9aea-a77d-42f6-b466-a8ce0221b807",
                                LockoutEnabled = true,
                                Active = true
                            });

            //modelBuilder.Entity<ActivityClass>().HasKey(ac => new { ac.ActivityId, ac.ClassId });
            //modelBuilder.Entity<ActivityUserType>().HasKey(ac => new { ac.ActivityId, ac.UserTypeId });
        }

        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<Management> Managements { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<ClassUser> ClassUsers { get; set; }
        public virtual DbSet<ActivitySlot> ActivitySlots { get; set; }
        public virtual DbSet<ActivityUserType> ActivityUserTypes { get; set; }
        public virtual DbSet<ActivityClass> ActivityClasses { get; set; }
        public virtual DbSet<ActivityInstance> ActivityInstances { get; set; }
        public virtual DbSet<ActivityInstanceDetail> ActivityInstanceDetails { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    }
}
