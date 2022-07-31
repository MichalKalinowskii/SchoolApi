using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Entities
{
    public class SubjectsTaughtByTeacher
    {
        [Key]
        public int SubjectsTaughtByTeacherId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
