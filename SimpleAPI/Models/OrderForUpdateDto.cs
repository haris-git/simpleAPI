using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class OrderForUpdateDto
    {
        public string Code { get; set; }

        [Required]
        public string Status { get; set; }

        public bool IsPaid { get; set; } = false;

        public int ClientId { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
