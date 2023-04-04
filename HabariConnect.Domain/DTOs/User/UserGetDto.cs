namespace HabariConnect.Domain.DTOs.User
{
    public class UserGetDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public bool TermsAgreed { get; set; }
        public bool IsActive { get; set; }
    }
}
