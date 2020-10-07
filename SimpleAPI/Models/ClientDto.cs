using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public int NumberOfOrders 
        { 
            get {
                return Orders.Count;    
            } 
        }

        public ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
