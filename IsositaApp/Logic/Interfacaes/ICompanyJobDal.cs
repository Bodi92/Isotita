using Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfacaes
{
    public interface ICompanyJobDal
    {
        void AddJob(List<JobDTO> jobDTOs);
        List<JobDTO> GetJobs(int companyId);
    }
}
