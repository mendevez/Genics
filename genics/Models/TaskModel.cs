using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required]
        public Project Project{ get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public TaskStatus Status { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Required]
        public ApplicationUser Reporter { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
