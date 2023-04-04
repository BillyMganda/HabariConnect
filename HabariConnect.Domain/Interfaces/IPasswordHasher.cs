namespace HabariConnect.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        public (byte[], byte[]) HashPassword(string password);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
