using System;
using System.ComponentModel.DataAnnotations;

namespace genics.Models
{
    public class Project 
    {       
         public int  Id { get; set; }
         [Required]
         public string Name { get; set; }
         [Required]
         public string Lead { get; set; }

    }

}
