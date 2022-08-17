using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.DataBaseContext;
using SchoolAPI.DTOs;
using SchoolAPI.Entities;
using SchoolAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Controllers
{
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }


        //GET

        [HttpGet("classes")]
        public ActionResult<IEnumerable<ClassDto>> GetAllClasses()
        {
            var classes = _schoolService.GetAllClasses();
            
            if (!classes.Any())
            {
                return NotFound();
            }

            return Ok(classes);
        }

        [HttpGet("classes/{classId}")]
        public ActionResult<ClassDto> GetClassById([FromRoute] int classId)
        {
            var classes = _schoolService.GetClassById(classId);

            if (classes is null)
            {
                return NotFound();
            }

            return Ok(classes);
        }

        [HttpGet("students")]
        public ActionResult<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = _schoolService.GetAllStudents();
            if (!students.Any())
            {
                return NotFound();
            } 
            return Ok(students);
        }

        [HttpGet("student/{studentId}")]
        public ActionResult<StudentDto> GetStudentById([FromRoute] int studentId)
        {
            var student = _schoolService.GetStudentById(studentId);
            if (student is null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("students/{age}")]
        public ActionResult<IEnumerable<StudentDto>> GetStudentsByAge([FromRoute] int age)
        {
            var students = _schoolService.GetStudentsByAge(age);
            if(!students.Any())
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("teachers")]
        public ActionResult<IEnumerable<TeacherDto>> GetAllTeachers()
        {
            var teachers = _schoolService.GetAllTeachers();
            if (!teachers.Any())
            {
                return NotFound();
            }
            return Ok(teachers);
        }

        [HttpGet("teacher/{teacherId}")]
        public ActionResult<TeacherDto> GetTeacherById([FromRoute] int teacherId)
        {
            var teacher = _schoolService.GetTeacherById(teacherId);
            if (teacher is null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpGet("teachers/{subject}")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachersBySubject([FromRoute] string subject)
        {
            var teacherBySubject = _schoolService.GetTeachersBySubject(subject);
            if (!teacherBySubject.Any())
            {
                return NotFound();
            }
            return Ok(teacherBySubject);
        }

        [HttpGet("subjects")]
        public ActionResult<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            var subjects = _schoolService.GetAllSubjects();

            if (!subjects.Any())
            {
                return NotFound();
            }

            return Ok(subjects);
        }
        
        
        //POST

        [HttpPost("class/teacher")]
        public ActionResult CreateClassAndTeacherToIt([FromBody] CreateClassAndTeacherDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _schoolService.CreateClassAndTeacherToIt(group);
            return Created($"/api/school/classes/{id}",null);
        }

        [HttpPost("teacher/subjects")]
        public ActionResult CreateTeacherAndSubjectsToHim([FromBody] CreateTeacherAndSubjectDto teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _schoolService.CreateTeacherAndSubjectsToHim(teacher);
            return Created($"/api/school/teacher/{id}", null);
        }

        [HttpPost("student/class")]
        public ActionResult CreateStudentAndAssignClassToHim([FromBody] CreateStudentAndAssignClassDto student)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            var classesId = _schoolService.GetAllClassesId();

            if (!classesId.Contains(student.ClassId))
            {
                return BadRequest("Given ClassId doesn't exist");
            }

            var id = _schoolService.CreateStudentAndAssignClassToHim(student);
            return Created($"/api/school/student/{id}", null);
        }


    }
}
