using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        public int ClassNumber { get; set; }
        public string ClassName { get; set; }
     
        public TeacherDto Teacher { get; set; }
        public List<StudentDto> Students { get; set; }
  
    }
}
