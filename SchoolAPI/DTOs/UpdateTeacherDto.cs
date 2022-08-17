using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class UpdateTeacherDto
    {
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string TeacherSecondName { get; set; }
        [Required]
        public string TeacherTitle { get; set; }
    }
}
