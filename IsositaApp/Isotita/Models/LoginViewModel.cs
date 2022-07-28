using System.ComponentModel.DataAnnotations;

namespace Isotita.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
