using Logic.Dtos;
using Logic.Interfacaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsotitaTest.FakeDals
{
    public class AppliedFakeDal : IJobAppliedDal, IAppliedDal
    {
        public void ApplyOnJob(List<AppliedDTO> appliedDTOs)
        {
            throw new NotImplementedException();
        }

        public void ChangeApplicantAppliedStatus(AppliedDTO appliedDTO)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfAppliedOnJob(int jobId, string email)
        {
            throw new NotImplementedException();
        }

        public AppliedDTO GetAppliedByJobIdAndEmail(int jobId, string email)
        {
            throw new NotImplementedException();
        }

        public List<AppliedDTO> GetAppliedsPerJob(int jobId)
        {
            throw new NotImplementedException();
        }
    }
}
