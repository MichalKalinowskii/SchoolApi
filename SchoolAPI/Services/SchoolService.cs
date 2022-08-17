using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.DataBaseContext;
using SchoolAPI.DTOs;
using SchoolAPI.Entities;
using SchoolAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext _dbcontext;
        private readonly IMapper _mapper;

        public SchoolService(SchoolDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }


        //GET

        public IEnumerable<ClassDto> GetAllClasses()
        {
            var classes = _dbcontext.Classes
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .ThenInclude(r => r.Subject)
                .ToList();

            var classesDto = _mapper.Map<List<ClassDto>>(classes);
            return classesDto;
        }

        public ClassDto GetClassById(int classId)
        {
            try
            {
                var classes = _dbcontext.Classes
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .ThenInclude(r => r.Subject)
                .Where(r => r.ClassId == classId)
                .First();
                var classesDto = _mapper.Map<ClassDto>(classes);
                return classesDto;
            }
            catch
            {
                return null;
            }                     
            
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            var students = _dbcontext.Students.ToList();
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return studentsDto;
        }

        public StudentDto GetStudentById(int studentId)
        {
            try
            {
                var student = _dbcontext.Students.Where(r => r.StudentId == studentId).First();
                var studentDto = _mapper.Map<StudentDto>(student);
                return studentDto;
            }
            catch
            {
                return null;
            }
            
        }

        public IEnumerable<StudentDto> GetStudentsByAge(int age)
        {
            var students = _dbcontext.Students.Where(r => r.StudentAge == age).ToList();
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return studentsDto;
        }

        public IEnumerable<TeacherDto> GetAllTeachers()
        {
            var teachers = _dbcontext.Teachers
                .Include(subject => subject.Subject)
                .ToList();
            
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return teachersDto;
        }

        public TeacherDto GetTeacherById(int teacherId)
        {
            try
            {
                var teacher = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .Include(subject => subject.Subject)
                .First();

                var teacherDto = _mapper.Map<TeacherDto>(teacher);
                return teacherDto;
            }
            catch
            {
                return null;
            }
            
        }

        public IEnumerable<TeacherDto> GetTeachersBySubject(string subject)
        {
            var teachersBySubject = _dbcontext.Teachers
                .Include(s => s.Subject)
                .Join(_dbcontext.Subjects,
                    post => post.TeacherId,
                    meta => meta.TeacherId,
                    (post, meta) => new { Post = post, Meta = meta, })
                .Where(r => r.Meta.NameOfTheSubject.Equals(subject))
                .ToList();

            List<Teacher> teachers = new();
            foreach (var item in teachersBySubject)
            {
                teachers.Add(item.Post);
            }

            var teacherDto = _mapper.Map<List<TeacherDto>>(teachers);
            return teacherDto;
        }

        public IEnumerable<SubjectDto> GetAllSubjects()
        {
            var subjects = _dbcontext.Subjects
                .ToList()
                .GroupBy(g => g.NameOfTheSubject)
                .Select(s => s.First());

            var subjectsDto = _mapper.Map<List<SubjectDto>>(subjects);
            return subjectsDto;
        }


        // POST

        public int CreateClassAndTeacherToIt(CreateClassAndTeacherDto group)
        {
            var classes = _mapper.Map<Class>(group);
            _dbcontext.Classes.Add(classes);
            _dbcontext.SaveChanges();
            return classes.ClassId;
        }

        public int CreateTeacherAndSubjectsToHim(CreateTeacherAndSubjectDto teacher)
        {
            var teachers = _mapper.Map<Teacher>(teacher);
            _dbcontext.Teachers.Add(teachers);
            _dbcontext.SaveChanges();
            return teachers.TeacherId;
        }

        public int CreateStudentAndAssignClassToHim(CreateStudentAndAssignClassDto student)
        {
            var students = _mapper.Map<Student>(student);
            _dbcontext.Students.Add(students);
            _dbcontext.SaveChanges();
            return students.StudentId;
        }

        public IEnumerable<int> GetAllClassesId()
        {
            var classesId = _dbcontext.Classes
                .Select(s => s.ClassId)
                .ToList();
            return classesId;
        }


    }
}
