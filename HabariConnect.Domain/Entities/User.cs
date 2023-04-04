namespace HabariConnect.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public string ForgotPasswordToken { get; set; } = string.Empty;
        public bool TermsAgreed { get; set; }
    }
}
