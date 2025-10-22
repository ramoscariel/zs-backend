using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Repositories
{
    public class SqlClientRepository : IClientRepository
    {
        private readonly ZsDbContext dbContext;
        public SqlClientRepository(ZsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Client>> GetAllAsync()
        {
            return await dbContext.Clients.ToListAsync();
        }
        
        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await dbContext.Clients.FindAsync(id);
        }

        public async Task<Client> AddAsync(Client client)
        {
            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            return client;
        }
        public async Task<Client?> UpdateAsync(Guid id, Client client)
        {
            var existingClient = await dbContext.Clients.FindAsync(id);
            if (existingClient == null) 
            {
                return null;
            }

            // Update Properties
            existingClient.NationalId = client.NationalId;
            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.Address = client.Address;
            existingClient.Number = client.Number;

            await dbContext.SaveChangesAsync();
            return existingClient;
        }

        public async Task<Client?> DeleteAsync(Guid id)
        {
            var existingClient = await dbContext.Clients.FindAsync(id);
            if (existingClient == null)
            {
                return null;
            }

            dbContext.Clients.Remove(existingClient);
            await dbContext.SaveChangesAsync();

            return existingClient;
        }        
    }
}
