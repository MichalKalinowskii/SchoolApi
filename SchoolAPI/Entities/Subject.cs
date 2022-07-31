using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Entities
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string NameOfTheSubject { get; set; }        
        public virtual List<SubjectsTaughtByTeacher> SubjectsTaughtByTeacher { get; set; }

    }
}
