using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(64, MinimumLength = 5, ErrorMessage = "Minimum username length is 5 symbols!")]
        public string username { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Minimum password length is 8 symbols!")]
        public string password { get; set; }
    }
}