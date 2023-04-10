using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using HabariConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace HabariConnect.Infrastructure.Repositories
{
    public class UserAuthorization : IUserAuthorization
    {
        private readonly HabariConnectDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserAuthorization(HabariConnectDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
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

        public async Task SendUserWelcomeEmailAsync(string recipient, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            using (var client = new SmtpClient(smtpSettings["Server"], int.Parse(smtpSettings["Port"])))
            {
                client.EnableSsl = bool.Parse(smtpSettings["UseSsl"]);
                client.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);
                
                using (var message = new MailMessage(smtpSettings["Username"], recipient))
                {
                    message.Subject = subject;
                    message.Body = body;                                        
                    await client.SendMailAsync(message);
                }
            }
        }

        public bool VerifyPasswordAsync(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new ArgumentNullException(nameof(Password));
            }

            if (PasswordHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(PasswordHash));
            }

            if (PasswordSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(PasswordSalt));
            }

            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PasswordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
