using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.DataBaseContext;
using SchoolAPI.DTOs;
using SchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolDbContext _dbcontext;
        private readonly IMapper _mapper;

        public SchoolController(SchoolDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        //this one doesnt show subjects run by teacher
        [HttpGet("classes")]
        public ActionResult<IEnumerable<ClassDto>> GetAllClasses()
        {
            var classes = _dbcontext.Classes
                .Include(r=>r.Student)
                .Include(r=>r.Teacher)
                .ThenInclude(r=>r.SubjectsTaughtByTeacher)
                .ThenInclude(r=>r.Subject)
                .ToList();
            
            if (classes is null)
            {
                return NotFound();
            }
            var classesDto = _mapper.Map<List<ClassDto>>(classes);
            return Ok(classesDto);
        }

        //this one doesnt show subjects run by teacher
        [HttpGet("classes/{classId}")]
        public ActionResult<IEnumerable<ClassDto>> GetAllClasses([FromRoute] int classId)
        {
            var classes = _dbcontext.Classes
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .Where(r => r.ClassId == classId)
                .ToList();

            if (classes is null)
            {
                return NotFound();
            }
            var classesDto = _mapper.Map<List<ClassDto>>(classes);
            return Ok(classesDto);
        }

        [HttpGet("students")]
        public ActionResult<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = _dbcontext.Students.ToList();
            if (students.Count == 0)
            {
                return NotFound();
            }
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentsDto);
        }

        [HttpGet("student/{studentId}")]
        public ActionResult<IEnumerable<StudentDto>> GetStudentsByStudentId([FromRoute] int studentId)
        {
            var students = _dbcontext.Students.Where(r => r.StudentId == studentId).ToList();
            if (students.Count == 0)
            {
                return NotFound();
            }
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentsDto);
        }

        [HttpGet("students/{age}")]
        public ActionResult<IEnumerable<StudentDto>> GetStudentsByAge([FromRoute] int age)
        {
            var students = _dbcontext.Students.Where(r => r.StudentAge == age).ToList();
            if(students.Count == 0)
            {
                return NotFound();
            }
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentsDto);
        }

        //this one doesnt show subjects run by teacher
        [HttpGet("teachers")]
        public ActionResult<IEnumerable<StudentDto>> GetAllTeachers()
        {
            var teachers = _dbcontext.Teachers
                .Include(subjects => subjects.SubjectsTaughtByTeacher)
                .ThenInclude(subject=>subject.Subject)
                .ToList();
            if (teachers.Count == 0)
            {
                return NotFound();
            }
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teachersDto);
        }

        //this one doesnt show subjects run by teacher
        [HttpGet("teacher/{teacherId}")]
        public ActionResult<IEnumerable<StudentDto>> GetTeacherByTeacherId([FromRoute] int teacherId)
        {
            var teachers = _dbcontext.Teachers.Where(r => r.TeacherId == teacherId).ToList();
            if (teachers.Count == 0)
            {
                return NotFound();
            }
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teachersDto);
        }

        //this one doesnt show subjects run by teacher
        [HttpGet("teachers/{subject}")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachersBySubject([FromRoute] string subject)
        {
            var subjectId = _dbcontext.Subjects
                .Where(w => w.NameOfTheSubject.Equals(subject))
                .Select(s => s.SubjectId)
                .FirstOrDefault();  
            if (subjectId == 0)
            {
                return NotFound();
            }

            var subjectsTaughtByTeacher =
                _dbcontext.Teachers
                .Join(_dbcontext.SubjectsTaughtByTeacher,
                post=>post.TeacherId,
                meta=>meta.TeacherId,
                (post, meta) => new { Post = post, Meta = meta,})
                .Where(r=>r.Meta.SubjectId == subjectId);

            List<Teacher> listOfTeachers = new();
            foreach(var item in subjectsTaughtByTeacher)
            {
                listOfTeachers.Add(item.Post);
            }
            if (listOfTeachers.Count == 0)
            {
                return NotFound();
            }

            List<Subject> listOfSubjects = new();
            foreach(var item in listOfTeachers)
            {
                var subjects = 
                _dbcontext.Subjects
                .Join(_dbcontext.SubjectsTaughtByTeacher,
                post => post.SubjectId,
                meta => meta.SubjectId,
                (post, meta) => new { Post = post, Meta = meta, })
                .Where(r => r.Meta.TeacherId == item.TeacherId);

                foreach (var sub in subjects)
                {
                    listOfSubjects.Add(sub.Post); 
                }
            }

            var subjectsDto = _mapper.Map<List<SubjectDto>>(listOfSubjects);
            var teachersDto = _mapper.Map<List<TeacherDto>>(listOfTeachers);
            return Ok(teachersDto);
        }

        [HttpGet("subjects")]
        public ActionResult<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            var subjects = _dbcontext.Subjects.ToList();
            if (subjects.Count == 0)
            {
                return NotFound();
            }
            var subjectsDto = _mapper.Map<List<SubjectDto>>(subjects);
            return Ok(subjectsDto);
        }

        //uri doesnt work
        [HttpPost("class/teacher")]
        public ActionResult CreateClassAndTeacher([FromBody] CreateClassAndTeacherDto group)
        {
            var classes = _mapper.Map<Class>(group);
            _dbcontext.Classes.Add(classes);
            _dbcontext.SaveChanges();
            return Created($"/api/school/classes/created/{classes.ClassId}",null);
        }

        [HttpPost("teacher")]
        public ActionResult AddTeacher([FromBody] AddTeacherDto teacher)
        {
            return null;
        }

    }
}
