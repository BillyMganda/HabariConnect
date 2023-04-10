using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;

namespace HabariConnect.Domain.Interfaces
{
    public interface IUserAuthorization
    {
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<User> GetUserByHandleAsync(string Handle);
        public Task AddNewUserAsync(UserCreateDto Dto);
        public Task SendUserWelcomeEmailAsync(string recipient, string subject, string body);
        public bool VerifyPasswordAsync(string Password, byte[] PasswordHash, byte[] PasswordSalt);
        public string CreateJwtToken(User user);
        public Task UpdateUserAsync(UserModifyDto user);
        public Task<string> CreateForgotPasswordToken();
        public Task<string> UpdateForgotPasswordTokenAsync(string Email, string Token); //replace variables with dto
        public Task<bool> SendForgotPasswordEmailAsync(string Email, string Token);
        public Task<User> GetUserByTokenAsync(string Token);
        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
        public Task ResetPasswordAsync(); //create reset password dto
    }
}
