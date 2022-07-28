using System.ComponentModel.DataAnnotations;
namespace Isotita.Models
{
    public class AppliedViewModel
    {
        public int JobId { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30, ErrorMessage = "Postcode moet maximaal 30 characters zijn.")]
        public string Email { get; set; }

        [Display(Name = "CV")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public IFormFile CV { get; set; }

        [Display(Name = "Motivatiebrief")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public IFormFile MotivationLetter { get; set; }
    }
}