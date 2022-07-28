using Dal;
using Logic.Interfacaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class JobDalFactory
    {
        public static IJobDal CreateJobDal()
        {
            return new JobDal();
        }
        public static ICompanyJobDal CreateCompanyJobDal()
        {
            return new JobDal();
        }
        public static IJobContainerDal CreateJobContanierDal()
        {
            return new JobDal();
        }
    }
}
