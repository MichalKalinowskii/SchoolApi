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
    [ApiController]
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
            var classesDto = _schoolService.GetAllClasses();
            return Ok(classesDto);
        }

        [HttpGet("classes/{classId}")]
        public ActionResult<ClassDto> GetClassById([FromRoute] int classId)
        {
            var classesDto = _schoolService.GetClassById(classId);
            return Ok(classesDto);
        }

        [HttpGet("students")]
        public ActionResult<IEnumerable<StudentDto>> GetAllStudents()
        {
            var studentsDto = _schoolService.GetAllStudents();            
            return Ok(studentsDto);
        }

        [HttpGet("student/{studentId}")]
        public ActionResult<StudentDto> GetStudentById([FromRoute] int studentId)
        {
            var studentDto = _schoolService.GetStudentById(studentId);            
            return Ok(studentDto);
        }

        [HttpGet("students/{age}")]
        public ActionResult<IEnumerable<StudentDto>> GetStudentsByAge([FromRoute] int age)
        {
            var studentsDto = _schoolService.GetStudentsByAge(age);     
            return Ok(studentsDto);
        }

        [HttpGet("teachers")]
        public ActionResult<IEnumerable<TeacherDto>> GetAllTeachers()
        {
            var teachersDto = _schoolService.GetAllTeachers();
            return Ok(teachersDto);
        }

        [HttpGet("teacher/{teacherId}")]
        public ActionResult<TeacherDto> GetTeacherById([FromRoute] int teacherId)
        {
            var teacherDto = _schoolService.GetTeacherById(teacherId);            
            return Ok(teacherDto);
        }

        [HttpGet("teachers/{subject}")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachersBySubject([FromRoute] string subject)
        {
            var teacherDto = _schoolService.GetTeachersBySubject(subject);           
            return Ok(teacherDto);
        }

        [HttpGet("subjects")]
        public ActionResult<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            var subjectsDto = _schoolService.GetAllSubjects();
            return Ok(subjectsDto);
        }


        //POST

        [HttpPost("class/teacher")]
        public ActionResult CreateClassAndTeacherToIt([FromBody] CreateClassAndTeacherDto group)
        {
            var id = _schoolService.CreateClassAndTeacherToIt(group);
            return Created($"/api/school/classes/{id}", null);
        }

        [HttpPost("teacher/subjects")]
        public ActionResult CreateTeacherAndSubjectsToHim([FromBody] CreateTeacherAndSubjectDto teacher)
        {
            var id = _schoolService.CreateTeacherAndSubjectsToHim(teacher);
            return Created($"/api/school/teacher/{id}", null);
        }

        [HttpPost("student/class")]
        public ActionResult CreateStudentAndAssignClassToHim([FromBody] CreateStudentAndAssignClassDto student)
        {                       
            var id = _schoolService.CreateStudentAndAssignClassToHim(student);
            return Created($"/api/school/student/{id}", null);
        }


        //DELETE

        [HttpDelete("remove/student/{studentId}")]
        public ActionResult RemoveStudent([FromRoute] int studentId)
        {
            _schoolService.RemoveStudent(studentId);
            return NoContent();
        }

        [HttpDelete("remove/teacher/{teacherId}")]
        public ActionResult RemoveTeacherThatIsNotATutor([FromRoute] int teacherId)
        {
            _schoolService.RemoveTeacherThatIsNotATutor(teacherId);
            return NoContent();
        }


        //PUT

        [HttpPut("update/teacher/{teacherId}")]
        public ActionResult UpdateTeacher([FromBody] UpdateTeacherDto dto, [FromRoute] int teacherId)
        {
            _schoolService.UpdateTeacher(dto,teacherId);
             return Ok();
        }
    }
}
