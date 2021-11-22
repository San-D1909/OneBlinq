using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
