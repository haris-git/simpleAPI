using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class OrderForCreationDto
    {
        public string Code { get; set; }

        [Required]
        public string Status { get; set; }

        public bool IsPaid { get; set; } = false;
        
        public int ClientId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
