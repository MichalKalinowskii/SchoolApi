using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using SchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DatabaseContext
{
    public class SchoolDbContext: DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>().Property(r => r.ClassNumber).IsRequired();
            modelBuilder.Entity<Class>().Property(r => r.ClassName).IsRequired().HasMaxLength(3);

            modelBuilder.Entity<Student>().Property(r => r.StudentName).IsRequired();
            modelBuilder.Entity<Student>().Property(r => r.StudentSecondName).IsRequired();

            modelBuilder.Entity<Teacher>().Property(r => r.TeacherName).IsRequired();
            modelBuilder.Entity<Teacher>().Property(r => r.TeacherSecondName).IsRequired();
            
            modelBuilder.Entity<Subject>().Property(r => r.NameOfTheSubject).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

    }
}
