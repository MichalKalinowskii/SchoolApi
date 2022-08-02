using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class AddTeacherDto
    {
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
        public string TeacherTitle { get; set; }
    }
}
