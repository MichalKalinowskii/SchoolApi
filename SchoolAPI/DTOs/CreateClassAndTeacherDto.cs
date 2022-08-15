using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class CreateClassAndTeacherDto
    {
        [Required]
        [Range(1, 8, ErrorMessage = "Please enter a value form scope in 1 to 8")]
        public int ClassNumber { get; set; }
        [Required]
        [MaxLength(3)]
        public string ClassName { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string TeacherSecondName { get; set; }
        public string TeacherTitle { get; set; }
    }
}
