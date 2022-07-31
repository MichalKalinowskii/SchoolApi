using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Entities
{
    public class Student
    {
        [Key]
        public int StudentId {get; set;}
        public string StudentName { get; set; }
        public string StudentSecondName { get; set; }
        public int StudentAge { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
