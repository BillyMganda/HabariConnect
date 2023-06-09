﻿using HabariConnect.Domain.DTOs.User;
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
        public Task<bool> UpdateForgotPasswordTokenAsync(string Email);
        public Task SendForgotPasswordEmailAsync(string recipient, string subject, string body);
        public Task<User> GetUserByTokenAsync(string Token);
        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
        public Task<User> ResetPasswordAsync(UserResetPasswordDto dto, byte[] hash, byte[] salt);
    }
}
