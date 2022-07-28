using Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfacaes
{
    public interface ICompanyDal
    {
        void AddCompany(List<CompanyDTO> companyDTOs);
        CompanyDTO GetCompanyById(int companyId);
        bool CheckIfCompanyExist(string email);
        int GetCompanyId(string email, string password);
    }
}
