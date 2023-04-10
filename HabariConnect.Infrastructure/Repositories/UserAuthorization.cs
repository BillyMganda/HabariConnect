using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using HabariConnect.Infrastructure.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
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

        public string CreateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Handle),
                new Claim(ClaimTypes.Email, user.Email)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task UpdateUserAsync(UserModifyDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
            if(user != null)
            {
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
            }
        }

        public Task<string> CreateForgotPasswordToken()
        {
            const int tokenLength = 32;

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var tokenBytes = new byte[tokenLength];
            randomNumberGenerator.GetBytes(tokenBytes);

            var token = Convert.ToBase64String(tokenBytes);
            return Task.FromResult(token);
        }

        public async Task<bool> UpdateForgotPasswordTokenAsync(string Email)
        {
            var token = CreateForgotPasswordToken();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if(user != null)
            {
                user.ForgotPasswordToken = await token;
                return true;
            }
            return false;
        }

        public async Task SendForgotPasswordEmailAsync(string recipient, string subject, string body)
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

        public async Task<User> GetUserByTokenAsync(string Token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ForgotPasswordToken == Token);
            return user!;
        }

        public Task ResetPasswordAsync()
        {

        }
    }
}
