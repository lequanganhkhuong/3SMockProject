using System.ComponentModel.DataAnnotations;

namespace MockProject.Models
{
    public class Transcript
    {
        [Key] 
        public int Id { get; set; }

        public int? ScheduleId { get; set; }
        //Student
        public int? UserId { get; set; }
        public double? Mark { get; set; }
        public bool? IsPassed { get; set; }
        public bool IsActive { get; set; }
        
        
      
        
        public virtual Schedule Schedule { get; set; }
       public virtual User User { get; set; }
    }
}