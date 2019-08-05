using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockProject.Models
{
    
    public class User
    {
        
        [Key] 
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Code { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public bool IsActive { get; set; }
        //public bool? IsGraduated { get; set; }
        
        public int? FacultyId { get; set; }
        public int? RoleId { get; set; }
        
        public virtual Faculty Faculty { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}