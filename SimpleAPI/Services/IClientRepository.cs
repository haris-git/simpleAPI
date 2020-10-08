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
        void CreateClient(Client client);
        void AddOrderForClient(int clientId, Order orderToBeCreated);
        void DeleteClient(Client client);
        bool Save();
        bool ClientExists(int clientId);
    }
}
