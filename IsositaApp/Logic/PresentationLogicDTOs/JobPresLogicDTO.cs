using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.PresentationLogicDTOs
{
    public class JobPresLogicDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Experience { get; set; }
        public List<AppliedPresLogicDTO> Applieds { get; set; }
    }
}
