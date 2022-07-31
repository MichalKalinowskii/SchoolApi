using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class TeacherDto
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
        public string TeacherTitle { get; set; }
        public List<SubjectsTaughtByTeacherDto> Subjects { get; set; }

    }
}
