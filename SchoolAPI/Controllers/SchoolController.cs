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


        //GET

        [HttpGet("classes")]
        public ActionResult<IEnumerable<ClassDto>> GetAllClasses()
        {
            var classes = _dbcontext.Classes
                .Include(r=>r.Student)
                .Include(r=>r.Teacher)
                .ThenInclude(r=>r.Subject)
                .ToList();
            
            if (classes is null)
            {
                return NotFound();
            }
            var classesDto = _mapper.Map<List<ClassDto>>(classes);
            return Ok(classesDto);
        }

        [HttpGet("classes/{classId}")]
        public ActionResult<IEnumerable<ClassDto>> GetAllClassesById([FromRoute] int classId)
        {
            var classes = _dbcontext.Classes
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .ThenInclude(r => r.Subject)
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

        [HttpGet("teachers")]
        public ActionResult<IEnumerable<StudentDto>> GetAllTeachers()
        {
            var teachers = _dbcontext.Teachers
                .Include(subject=>subject.Subject)
                .ToList();
            if (teachers.Count == 0)
            {
                return NotFound();
            }
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teachersDto);
        }

        [HttpGet("teacher/{teacherId}")]
        public ActionResult<IEnumerable<StudentDto>> GetTeacherByTeacherId([FromRoute] int teacherId)
        {
            var teachers = _dbcontext.Teachers
                .Where(r => r.TeacherId == teacherId)
                .Include(subject=>subject.Subject)
                .ToList();
            if (teachers.Count == 0)
            {
                return NotFound();
            }
            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teachersDto);
        }

        [HttpGet("teachers/{subject}")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachersBySubject([FromRoute] string subject)
        {
            var teacherBySubject = _dbcontext.Teachers
                .Include(s => s.Subject)
                .Join(_dbcontext.Subjects,
                    post=>post.TeacherId,
                    meta=>meta.TeacherId,
                    (post, meta) => new { Post = post, Meta = meta,})
                .Where(r=>r.Meta.NameOfTheSubject.Equals(subject))
                .ToList();

            if (teacherBySubject.Count == 0)
            {
                return NotFound();
            }

            List<Teacher> teachers = new();
            foreach (var item in teacherBySubject)
            {
                teachers.Add(item.Post);
            }

            var teacherDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teacherDto);
        }

        [HttpGet("subjects")]
        public ActionResult<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            var subjects = _dbcontext.Subjects
                .ToList()
                .GroupBy(g=>g.NameOfTheSubject)
                .Select(s=>s.First());

            if (!subjects.Any())
            {
                return NotFound();
            }
            
            var subjectsDto = _mapper.Map<List<SubjectDto>>(subjects);
            return Ok(subjectsDto);
        }
        
        
        //POST

        [HttpPost("class/teacher")]
        public ActionResult CreateClassAndTeacherToIt([FromBody] CreateClassAndTeacherDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classes = _mapper.Map<Class>(group);
            _dbcontext.Classes.Add(classes);
            _dbcontext.SaveChanges();
            return Created($"/api/school/classes/{classes.ClassId}",null);
        }

        [HttpPost("teacher/subjects")]
        public ActionResult CreateTeacherAndSubjectsToHim([FromBody] CreateTeacherAndSubjectDto teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacheres = _mapper.Map<Teacher>(teacher);
            _dbcontext.Teachers.Add(teacheres);
            _dbcontext.SaveChanges();
            return Created($"/api/school/teacher/{teacheres.TeacherId}", null);
        }

        [HttpPost("student/class")]
        public ActionResult CreateStudentAndAssignClassToHim([FromBody] CreateStudentAndAssignClassDto student)
        {
            var classesId = _dbcontext.Classes
                .Select(s => s.ClassId)
                .ToList();

            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            
            if (!classesId.Contains(student.ClassId))
            {
                return BadRequest("Given ClassId doesn't exist");
            }

            var students = _mapper.Map<Student>(student);
            _dbcontext.Students.Add(students);
            _dbcontext.SaveChanges();
            return Created($"/api/school/student/{students.StudentId}", null);
        }


    }
}
