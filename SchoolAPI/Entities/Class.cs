using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Entities
{
    public class Class
    {   
        [Key]
        public int ClassId { get; set; }
        public int ClassNumber { get; set; }
        public string ClassName { get; set; }       
        public int TeacherId { get; set; }
        public virtual List<Student> Student { get; set; }
        public virtual Teacher Teacher { get; set; }


    }
}
