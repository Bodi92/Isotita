using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfacaes;
using Dal;

namespace Factory
{
    public class AppliedDalFactory
    {
        public static IAppliedDal CreateAppliedDal()
        {
           return new AppliedDal();
        }
        public static IJobAppliedDal CreateJobAppliedDal()
        {
            return new AppliedDal();
        }
    }
}
