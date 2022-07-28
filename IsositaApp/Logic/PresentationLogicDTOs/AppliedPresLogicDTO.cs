using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.PresentationLogicDTOs
{
    public class AppliedPresLogicDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Email { get; set; }
        public string CV { get; set; }
        public string MotivationLetter { get; set; }
        public bool Status { get; set; }
        public bool Treated { get; set; }
    }
}
