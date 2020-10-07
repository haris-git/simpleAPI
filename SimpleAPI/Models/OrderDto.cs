using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public bool IsPaid { get; set; } = false;

    }
}
