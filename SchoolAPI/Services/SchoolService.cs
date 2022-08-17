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
            var classes = _dbcontext.Classes
            .Include(r => r.Student)
            .Include(r => r.Teacher)
            .ThenInclude(r => r.Subject)
            .Where(r => r.ClassId == classId)
            .FirstOrDefault();
            var classesDto = _mapper.Map<ClassDto>(classes);
            return classesDto;
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            var students = _dbcontext.Students.ToList();
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return studentsDto;
        }

        public StudentDto GetStudentById(int studentId)
        {
            var student = _dbcontext.Students.Where(r => r.StudentId == studentId).FirstOrDefault();
            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
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

            var teacher = _dbcontext.Teachers
            .Where(r => r.TeacherId == teacherId)
            .Include(subject => subject.Subject)
            .FirstOrDefault();

            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return teacherDto;

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
        public IEnumerable<int> GetAllClassesId()
        {
            var classesId = _dbcontext.Classes
                .Select(s => s.ClassId)
                .ToList();
            return classesId;
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

        

        // DELETE

        public bool RemoveStudent(int studentId)
        {
            var student = _dbcontext.Students.Where(r => r.StudentId == studentId).FirstOrDefault();
            if (student is null) return false;
            _dbcontext.Students.Remove(student);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool RemoveTeacherThatIsNotATutor(int teacherId)
        {
            var teacher = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .FirstOrDefault();

            var teachersInClasses = _dbcontext.Classes
                .Select(s => s.TeacherId)
                .ToList();

            if (teacher is null || teachersInClasses.Contains(teacherId)) return false;

            _dbcontext.Teachers.Remove(teacher);

            var subject = _dbcontext.Subjects
                .Where(r => r.TeacherId == teacherId)
                .ToList();            
            _dbcontext.Subjects.RemoveRange(subject);

            _dbcontext.SaveChanges();
            return true;
        }


        //PUT

        public bool UpdateTeacher(UpdateTeacherDto dto,int teacherId)
        {
            var teacher = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .FirstOrDefault();
            if (teacher is null) return false;

            teacher.TeacherName = dto.TeacherName;
            teacher.TeacherSecondName = dto.TeacherSecondName;
            teacher.TeacherTitle = dto.TeacherTitle;
            _dbcontext.SaveChanges();
            return true;
        }

    }
}
