using System.ComponentModel.DataAnnotations;

namespace MockProject.Models
{
    public class Grade
    {
        [Key] 
        public int Id { get; set; }

        public int TranscriptId { get; set; }
        public int SubjectId { get; set; }
        public double Mark { get; set; }
        
        public virtual Transcript Transcript { get; set; }
        public virtual Subject Subject { get; set; }
    }
}