using System;
using Microsoft.EntityFrameworkCore;

namespace MockProject.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjectses { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new {Id = 1, Name = "admin" },
                new {Id = 2,  Name = "teacher" },
                new {Id = 3, Name = "student" }
            );
            modelBuilder.Entity<Faculty>().HasData(
                new {Id = 1, Name = "ABC1", Credits = 20, IsActive = true},
                new {Id = 2, Name = "ABC2", Credits = 25, IsActive = true},
                new {Id = 3, Name = "ABC3", Credits = 20, IsActive = true},
                new {Id = 4, Name = "ABC4", Credits = 20, IsActive = true}
            );
            modelBuilder.Entity<User>().HasData(
                new {Id = 1, Username = "admin", Password = "admin", Code = "admin", Name = "Admin", Birthday = new DateTime(1995,1,1), Gender = true, Address = "abc", IsActive = true, RoleId  = 1}
            );
        }
    }
}