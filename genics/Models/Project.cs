using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace genics.Models
{
    public class Project 
    {       
         public int  Id { get; set; }
         [Required]
         [MaxLength(50)]
         public string Name { get; set; }
         [Required]
         [MaxLength(450)]
         public string LeadId { get; set; }
         [Required]
         [MaxLength(100)]
         public string LeadFullName { get; set; }
         public DateTime CreatedAt { get; set; } = DateTime.Now;
         public IList<ApplicationUser> Users { get; set; }
    }

}
