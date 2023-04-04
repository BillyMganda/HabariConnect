using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using HabariConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabariConnect.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HabariConnectDbContext _dbContext;
        public UserRepository(HabariConnectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByHandleAsync(string handle)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Handle == handle);
        }

        public async Task CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
