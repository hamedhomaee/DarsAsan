using System;
using DarsAsan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DarsAsan.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentUser> Students { get; set; }
        public DbSet<TeacherUser> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(m => m.UserName).IsUnique(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(m => m.Email).IsUnique(true);

            modelBuilder.Entity<StudentUser>()
                .HasBaseType<ApplicationUser>();

            modelBuilder.Entity<TeacherUser>()
                .HasBaseType<ApplicationUser>();
        }
    }
}