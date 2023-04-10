using System.ComponentModel.DataAnnotations;

namespace HabariConnect.Domain.DTOs.User
{
    public class UserModifyDto
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; } = string.Empty;        
        public string LastName { get; set; } = string.Empty;
        //public string NewPassword { get; set; } = string.Empty;
        //public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
