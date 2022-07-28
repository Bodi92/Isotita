using System.ComponentModel.DataAnnotations;

namespace Isotita.Models
{
    public class AppliedStatusViewModel
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "CV")]
        public string CV { get; set; }

        [Display(Name = "Motivatiebrief")]
        public string MotivationLetter { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        [Display(Name = "Behandeld")]
        public bool Treated { get; set; }

    }
}
