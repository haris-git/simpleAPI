using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(int clientId, bool includeOrdersOfClient);
        bool ClientExists(int clientId);
        void AddOrderForClient(int clientId, Order orderToBeCreated);
        bool Save();
    }
}
