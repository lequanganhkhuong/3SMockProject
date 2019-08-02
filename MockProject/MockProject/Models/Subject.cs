using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockProject.Models
{
    public class Subject
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}