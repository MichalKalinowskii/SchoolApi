using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.DTOs
{
    public class CreateStudentAndAssignClassDto
    {
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string StudentSecondName { get; set; }
        [Required]
        public int StudentAge { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}
