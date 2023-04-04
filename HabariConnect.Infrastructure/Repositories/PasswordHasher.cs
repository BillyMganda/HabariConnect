using HabariConnect.Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HabariConnect.Infrastructure.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        public (byte[], byte[]) HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32);

            return (hash, salt);
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            if (passwordSalt == null)
            {
                throw new ArgumentNullException(nameof(passwordSalt));
            }

            byte[] hashToCheck = KeyDerivation.Pbkdf2(
                password: password,
                salt: passwordSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32);

            if (hashToCheck.Length != passwordHash.Length)
            {
                return false;
            }

            for (int i = 0; i < hashToCheck.Length; i++)
            {
                if (hashToCheck[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
