using API.Data;
using API.Data.Models;
using API.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbAb6d2eProjectlabContext _dbContext;
        public ClientRepository(DbAb6d2eProjectlabContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public bool ClientExists(int clientId)
        {
            return _dbContext.Clients.Any(c => c.Id == clientId);
        }

        public bool ClientExists(string email)
        {
            return _dbContext.Clients.Any(c => c.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public bool CreateClient(Client client)
        {
            _dbContext.Clients.Add(client);
            return Save(); throw new System.NotImplementedException();
        }

        public bool DeleteClient(Client client)
        {
            _dbContext.Clients.Remove(client);
            return Save();
        }

        public Client GetClient(int clientId)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        }

        public ICollection<Client> GetClients()
        {
            return _dbContext.Clients.OrderBy(c => c.FirstName).ToList();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateClient(Client client)
        {
            _dbContext.Clients.Update(client);
            return Save();
        }
    }
}
