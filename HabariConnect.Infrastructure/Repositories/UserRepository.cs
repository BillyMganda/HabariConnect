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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByHandleAsync(string handle)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Handle == handle);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> DisableUserAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> EnableUserAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            user.IsActive = true;
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
