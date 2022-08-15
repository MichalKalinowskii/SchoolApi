using SchoolAPI.DataBaseContext;
using SchoolAPI.Entities;
using SchoolAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DataSeed
{
    public class SchoolSeeder : ISchoolSeeder
    {
        private readonly SchoolDbContext _dbContext;

        public SchoolSeeder(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Classes.Any())
                {
                    var classes = GetClasses();
                    _dbContext.Classes.AddRange(classes);
                    _dbContext.SaveChanges();

                }
            }
        }

        private IEnumerable<Class> GetClasses()
        {
            var classes = new List<Class>()
            {
                new Class()
                {
                    ClassNumber = 7,
                    ClassName="A",

                    Student = new List<Student>()
                    {
                        new Student()
                        {
                            StudentName="Adam",
                            StudentSecondName="Nowak",
                            StudentAge=13
                        },
                        new Student()
                        {
                            StudentName="Ala",
                            StudentSecondName="Nowak",
                            StudentAge=13
                        },
                        new Student()
                        {
                            StudentName="Zuzia",
                            StudentSecondName="Kowalska",
                            StudentAge=13
                        }
                    },

                    Teacher = new Teacher()
                    {
                        TeacherName="Zbigniew",
                        TeacherSecondName="Zabola",
                        TeacherTitle="Magister",

                        Subject = new List<Subject>()
                        {
                            new Subject()
                            {
                                NameOfTheSubject="Informatyka"
                            },
                            new Subject()
                            {
                                NameOfTheSubject="Technika"
                            }
                        }
                    }
                },

                new Class()
                {
                    ClassNumber = 8,
                    ClassName="B",

                    Student = new List<Student>()
                    {
                        new Student()
                        {
                            StudentName="Kacper",
                            StudentSecondName="Kowalski",
                            StudentAge=14
                        },
                        new Student()
                        {
                            StudentName="Robert",
                            StudentSecondName="Zamorski",
                            StudentAge=14
                        },
                        new Student()
                        {
                            StudentName="Kuba",
                            StudentSecondName="Pufalo",
                            StudentAge=14
                        },

                    },

                    Teacher = new Teacher()
                    {
                        TeacherName="Barbara",
                        TeacherSecondName="Owadzka",
                        TeacherTitle="Profesor",

                        Subject = new List<Subject>()
                        {
                            new Subject()
                            {
                                NameOfTheSubject="Matematyka"
                            },
                            new Subject()
                            {
                                NameOfTheSubject="Fizyka"
                            }
                        }
                    }
                }
            };
            return classes;
        }

    }
}
