using API.Data.Models;
using System.Collections.Generic;

namespace API.Repository.IRepository
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        Client GetClient(int clientId);
        bool ClientExists(int clientId);
        bool ClientExists(string email);
        bool CreateClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(Client client);
        bool Save();
    }
}
