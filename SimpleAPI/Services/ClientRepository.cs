using Microsoft.EntityFrameworkCore;
using SimpleAPI.Context;
using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public class ClientRepository : IClientRepository
    {
        private SimpleApiContext _context;

        public ClientRepository(SimpleApiContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
        }

        public Client GetClient(int clientId, bool includeOrdersOfClient = false)
        {
            if (includeOrdersOfClient)
            {
                return _context.Clients.Include(c => c.Orders).Where(c => c.Id == clientId).FirstOrDefault();
            }

            return _context.Clients.Where(c => c.Id == clientId).FirstOrDefault();
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.OrderBy(c => c.Id).ToList();
        }

        public void CreateClient(Client client)
        {
            _context.Add(client);
        }

        public void AddOrderForClient(int clientId, Order orderToBeCreated)
        {
            var client = GetClient(clientId, false);
            client.Orders.Add(orderToBeCreated);
        }

        public void DeleteClient(Client client)
        {
            _context.Remove(client);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >=0);
        }

        public bool ClientExists(int clientId)
        {
            return _context.Clients.Any(c => c.Id == clientId);
        }
    }
}
