using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class SubjectsTaughtByTeacherDto
    {
        public int SubjectsTaughtByTeacherId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public TeacherDto Teacher { get; set; }
        public SubjectDto Subject { get; set; }
    }
}
