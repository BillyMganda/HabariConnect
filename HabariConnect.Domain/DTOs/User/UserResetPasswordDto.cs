using System.ComponentModel.DataAnnotations;

namespace HabariConnect.Domain.DTOs.User
{
    public class UserResetPasswordDto
    {
        [Required]
        public string token { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string new_password { get; set; } = string.Empty;
        [Required, MinLength(6), Compare("new_password")]
        public string confirm_new_password { get; set; } = string.Empty;
    }
}
