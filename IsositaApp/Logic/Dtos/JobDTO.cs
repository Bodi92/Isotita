using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Dtos
{
    public class JobDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Experience { get; set; }
        public CompanyDTO Company_DTO { get; set; }
        public List<AppliedDTO> Applieds { get; set; }
        public int AppliedsCount { get; set;}
    }
}
