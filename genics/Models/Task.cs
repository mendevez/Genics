using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public Project Project{ get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public ApplicationUser AssignedTo { get; set; }

    }
}
