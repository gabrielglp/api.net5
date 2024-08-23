using System.ComponentModel.DataAnnotations;

namespace api.net5.Models.LoginModel
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Email or User Name is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

