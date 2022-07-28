using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dtos
{
    public class AppliedDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CV { get; set; }
        public string MotivationLetter { get; set; }
        public bool Status { get; set; }
        public bool Treated { get; set; }
        public JobDTO Job_DTO { get;  set; }
    }
}
