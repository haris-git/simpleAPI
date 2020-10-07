using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class ClientForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Company { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
