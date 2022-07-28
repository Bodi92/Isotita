using Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfacaes
{
    public interface IJobAppliedDal
    {
        void ApplyOnJob(List<AppliedDTO> appliedDTOs);
        bool CheckIfAppliedOnJob(int jobId, string email);
        List<AppliedDTO> GetAppliedsPerJob(int jobId);
        AppliedDTO GetAppliedByJobIdAndEmail(int jobId, string email);
    }
}
