using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolAPI.DatabaseContext;
using SchoolAPI.DTOs;
using SchoolAPI.Entities;
using SchoolAPI.Exceptions;
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
        private readonly ILogger<SchoolService> _logger;

        public SchoolService(SchoolDbContext dbcontext, IMapper mapper, ILogger<SchoolService> logger)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _logger = logger;
        }


        //GET


        public IEnumerable<ClassDto> GetAllClasses()
        {
            var classes = _dbcontext.Classes
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .ThenInclude(r => r.Subject)
                .ToList();
            if (!classes.Any()) throw new NotFoundException("Classes doesn't found");
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
            if (classes is null) throw new NotFoundException("Class don't found");           
            var classesDto = _mapper.Map<ClassDto>(classes);
            return classesDto;
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            var students = _dbcontext.Students.ToList();
            if (!students.Any()) throw new NotFoundException("Students doesn't found");
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return studentsDto;
        }

        public StudentDto GetStudentById(int studentId)
        {
            var student = _dbcontext.Students.Where(r => r.StudentId == studentId).FirstOrDefault();
            if (student is null) throw new NotFoundException("Student don't found");
            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public IEnumerable<StudentDto> GetStudentsByAge(int age)
        {
            var students = _dbcontext.Students.Where(r => r.StudentAge == age).ToList();
            if (!students.Any()) throw new NotFoundException("Students doesn't found");
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return studentsDto;
        }

        public IEnumerable<TeacherDto> GetAllTeachers()
        {
            var teachers = _dbcontext.Teachers
                .Include(subject => subject.Subject)
                .ToList();
            if (!teachers.Any()) throw new NotFoundException("Teachers doesn't found");
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return teachersDto;
        }

        public TeacherDto GetTeacherById(int teacherId)
        {

            var teacher = _dbcontext.Teachers
            .Where(r => r.TeacherId == teacherId)
            .Include(subject => subject.Subject)
            .FirstOrDefault();
            if (teacher is null) throw new NotFoundException("Teacher don't found");
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
            if (!teachersBySubject.Any()) throw new NotFoundException("Teachers doesn't found");
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
            if (!subjects.Any()) throw new NotFoundException("Subjects doesn't found");
            var subjectsDto = _mapper.Map<List<SubjectDto>>(subjects);
            return subjectsDto;
        }
        


        // POST

        public int CreateClassAndTeacherToIt(CreateClassAndTeacherDto group)
        {
            _logger.LogWarning($"Create class and teacher to it, POST action was invoked");
            var classes = _mapper.Map<Class>(group);
            _dbcontext.Classes.Add(classes);
            _dbcontext.SaveChanges();
            _logger.LogWarning($"Class with ClassId = {classes.ClassId}, was created");
            return classes.ClassId;
        }

        public int CreateTeacherAndSubjectsToHim(CreateTeacherAndSubjectDto teacher)
        {
            _logger.LogWarning($"Create teacher and subjects to him, POST action was invoked");
            var teachers = _mapper.Map<Teacher>(teacher);
            _dbcontext.Teachers.Add(teachers);
            _dbcontext.SaveChanges();
            _logger.LogWarning($"Teacher with TeacherId = {teachers.TeacherId}, was created");
            return teachers.TeacherId;
        }
        /// <summary>
        /// Create student and assign class to him by ClassId
        /// </summary>
        /// <param name="student">Take object of class CreateStudentAndAssignClassDto</param>
        /// <returns>StudentId of created student</returns>
        public int CreateStudentAndAssignClassToHim(CreateStudentAndAssignClassDto student)
        {
            _logger.LogWarning($"Create student and assign class to him, POST action was invoked");
            var classesId = GetAllClassesId();
            if (!classesId.Contains(student.ClassId))
            {
                _logger.LogWarning($"Class with ClassId =  {student.ClassId}, was not found");
                throw new NotFoundException("Class don't found");
            }                       
            var students = _mapper.Map<Student>(student);
            _dbcontext.Students.Add(students);
            _dbcontext.SaveChanges();
            _logger.LogWarning($"Student with StudentId = {students.StudentId}, was created");
            return students.StudentId;
        }

        private IEnumerable<int> GetAllClassesId()
        {
            var classesId = _dbcontext.Classes
                .Select(s => s.ClassId)
                .ToList();
            return classesId;
        }


        // DELETE

        public void RemoveStudent(int studentId)
        {
            _logger.LogWarning($"Student with StudentId = {studentId}, DELETE action was invoked");

            var student = _dbcontext.Students.Where(r => r.StudentId == studentId).FirstOrDefault();
            if (student is null)
            {
                _logger.LogWarning($"Student with StudentId = {studentId}, DELETE action have failed");
                throw new NotFoundException("Student not found");       
            }
                _dbcontext.Students.Remove(student);
            _dbcontext.SaveChanges();
            _logger.LogWarning($"Student with StudentId = {studentId}, DELETE action have succeed");
        }

        public void RemoveTeacherThatIsNotATutor(int teacherId)
        {
            _logger.LogWarning($"Teacher with TeacherId = {teacherId}, DELETE action was invoked");       
            var teacher = GetTeacher(teacherId);

            var teachersInClasses = GetListofTeacherIdInClasses();

            if (teacher is null || teachersInClasses.Contains(teacherId))
            {
                _logger.LogWarning($"Teacher with TeacherId = {teacherId}, DELETE action have failed");
                throw new NotFoundException("Teacher not found");
            }
            _dbcontext.Teachers.Remove(teacher);

            _dbcontext.SaveChanges();

            _logger.LogWarning($"Teacher with TeacherId = {teacherId}, DELETE action have succeed");

            RemoveSubjectsRunByTeacher(teacherId);
        }

        private Teacher GetTeacher(int teacherId)
        {
            var teacher = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .FirstOrDefault();
            return teacher;
        }

        private IEnumerable<int> GetListofTeacherIdInClasses()
        {
            var list =_dbcontext.Classes
                .Select(s => s.TeacherId)
                .ToList();
            return list;
    }

        private void RemoveSubjectsRunByTeacher(int teacherId)
        {
            var subject = _dbcontext.Subjects
                .Where(r => r.TeacherId == teacherId)
                .ToList();

            _dbcontext.Subjects.RemoveRange(subject);

            _dbcontext.SaveChanges();
        }

        //PUT

        public void UpdateTeacher(UpdateTeacherDto dto,int teacherId)
        {
            _logger.LogWarning($"Teacher with TeacherId = {teacherId}, PUT action was invoked");
            var teacher = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .FirstOrDefault();
            if (teacher is null)
            {
                _logger.LogWarning($"Teacher with TeacherId = {teacherId}, PUT action have failed");
                throw new NotFoundException("Teacher not found");
            }
            teacher.TeacherName = dto.TeacherName;
            teacher.TeacherSecondName = dto.TeacherSecondName;
            teacher.TeacherTitle = dto.TeacherTitle;
            _dbcontext.SaveChanges();
            _logger.LogWarning($"Teacher with TeacherId = {teacherId}, PUT action have succeed");
        }

    }
}
