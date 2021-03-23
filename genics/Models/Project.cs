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
         public ApplicationUser Lead { get; set; }
         public DateTime CreatedAt { get; set; } = DateTime.Now;
         public List<ApplicationUser> Members { get; set; }
    }

}
