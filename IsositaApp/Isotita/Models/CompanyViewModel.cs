using System.ComponentModel.DataAnnotations;

namespace Isotita.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Bedrijfsnaam")]
        [MaxLength(30, ErrorMessage = "Naam mag maximum 30 letters/getallen zijn.")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30, ErrorMessage = "Email mag maximum 30 letters/getallen zijn.  ")]
        public string Email { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [MinLength(8, ErrorMessage = "Wachtwoord moet minimaal 8 letters/getallen zijn.")]
        [MaxLength(25, ErrorMessage = "Wachtwoord mag maximum 25 letters/getallen zijn.")]
        public string Password { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Telefoonnummer moet minimaal 10 getallen zijn.")]
        [MaxLength(14, ErrorMessage = "Mobielnummer mag maximum 14 nummers zijn.")]
        public string MobileNumber { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [MaxLength(60, ErrorMessage = "Adres mag maximum 60 letters/getallen zijn.")]
        public string Address { get; set; }

        [Display(Name = "Stad")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [MaxLength(28, ErrorMessage = "Stad mag maximum 28 letters zijn.")]
        public string City { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [MinLength(6, ErrorMessage = "Postcode moet minimaal 6 characters zijn.")]
        [MaxLength(7, ErrorMessage = "Postcode moet maximaal 7 characters zijn.")]
        public string Zipcode { get; set; }
    }
}
