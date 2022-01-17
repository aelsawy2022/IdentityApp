using IdentityApplication.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
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
    }
}
