using Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfacaes
{
    public interface IJobDal
    {
        void Edit(JobDTO jobDTO);
        void Delete(int jobId);
    }
}
