using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name can only contain alphabetic characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Surname can only contain alphabetic characters")]
        public string Surname { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
