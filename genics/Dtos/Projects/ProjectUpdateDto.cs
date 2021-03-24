using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Dtos.Projects
{
    public class ProjectUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(450)]
        public string LeadId { get; set; }
        [Required]
        [MaxLength(100)]
        public string LeadFullName { get; set; }
    }
}
