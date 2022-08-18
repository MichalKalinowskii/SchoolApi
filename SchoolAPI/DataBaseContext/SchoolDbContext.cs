using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using SchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DataBaseContext
{
    public class SchoolDbContext: DbContext
    {
        private readonly string _connectionString = 
       
        @"Server=SQLOLEDB.1;Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = SchoolDb; Data Source = localhost;";

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
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
