using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Entities
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSecondName { get; set; }
        public string TeacherTitle { get; set; }
        public virtual Class Class { get; set; }
        public virtual List<SubjectsTaughtByTeacher> SubjectsTaughtByTeacher { get; set; }

    }
}