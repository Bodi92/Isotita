using System.ComponentModel.DataAnnotations;

namespace Isotita.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Titel")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        public string Title { get; set; }

        [Display(Name = "Beschrijving")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
       // [MinLength(50, ErrorMessage ="Beschrijving moet minimaal 50 karakters bevatten.")]
        public string Description { get; set; }

        [Display(Name = "Eisen")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        // [MinLength(50, ErrorMessage ="Eisen moet minimaal 50 karakters bevatten.")]
        public string Requirements { get; set; }

        [Display(Name = "Ervaringen")]
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        // [MinLength(50, ErrorMessage ="Ervaringen moet minimaal 50 karakters bevatten.")]
        public string Experience { get; set; }

        public List<AppliedStatusViewModel>? appliedStatusViewModels { get; set; }
        public CompanyViewModel? companyViewModel { get; set; }
    }
}
