using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using HabariConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HabariConnect.Infrastructure.Repositories
{
    public class UserAuthorization : IUserAuthorization
    {
        private readonly HabariConnectDbContext _dbContext;
        public UserAuthorization(HabariConnectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<User> GetUserByHandleAsync(string Handle)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Handle == Handle);
        }

        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }
        public async Task AddNewUserAsync(UserCreateDto Dto)
        {
            CreatePasswordHash(Dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var NewUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = Dto.FirstName,
                LastName = Dto.LastName,
                Email = Dto.Email,
                Handle = Dto.Handle,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedOn = DateTime.Now,
                LastModified = DateTime.Now,
                ForgotPasswordToken = "",
                TermsAgreed = Dto.TermsAgreed,
                IsActive = true
            };
            await _dbContext.Users.AddAsync(NewUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SendUserWelcomeEmailAsync(string Email)
        {

        }
    }
}
