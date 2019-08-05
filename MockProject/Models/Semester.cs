using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockProject.Models
{
    public class Semester
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        
        
        public virtual List<Schedule> Schedules { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}