using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;

namespace HabariConnect.Domain.Interfaces
{
    public interface IUserAuthorization
    {
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<User> GetUserByHandleAsync(string Handle);
        public Task<User> AddNewUserAsync(UserCreateDto Dto);
        public Task<bool> VerifyPasswordAsync(string Password, byte[] PasswordHash, byte[] PasswordSalt);
        public Task<string> CreateJwtToken();
    }
}
