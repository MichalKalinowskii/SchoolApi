using SchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class CreateTeacherAndSubjectDto
    {
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string TeacherSecondName { get; set; }
        public string TeacherTitle { get; set; }
        [Required]
        public List<Subject> Subjects { get; set; }
    }
}
