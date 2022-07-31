using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSecondName { get; set; }
        public int StudentAge { get; set; }
    }
}
