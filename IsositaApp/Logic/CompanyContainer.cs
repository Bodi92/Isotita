using Logic.Dtos;
using Logic.Interfacaes;
using Logic.PresentationLogicDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CompanyContainer
    {
        private List<Company> companies = new List<Company>();
        public IEnumerable<Company> Companies => companies;
        private readonly ICompanyDal _companyDal;
        private readonly ICompanyJobDal _companyJobDal;
        private readonly IJobDal _jobDal;
        public CompanyContainer(ICompanyDal companyDal, ICompanyJobDal companyJobDal, IJobDal jobDal)
        {
            this._companyDal    = companyDal;
            this._companyJobDal = companyJobDal;
            this._jobDal        = jobDal;
        }
        public Company GetById(int companyId)
        {
            CompanyDTO companyDTO = _companyDal.GetCompanyById(companyId);
            return this.DTOToCompany(companyDTO);
        }
        public void AddCompany(CompanyPresLogicDTO companyPresLogicDTO)
        {
            Company company = PresLogicDTOToCompany(companyPresLogicDTO);
            this.companies.Add(company);
            this._companyDal.AddCompany(this.CompaniesToDTOs());
        }
        public bool CheckIfExist(string email)
        {
            return this._companyDal.CheckIfCompanyExist(email);
        }
        public int GetCompanyId(string email, string password)
        {
            return this._companyDal.GetCompanyId(email, password);
        }
        private List<CompanyDTO> CompaniesToDTOs()
        {
            List<CompanyDTO> companyDTOs = new List<CompanyDTO>();
            foreach(Company company in this.companies)
            {
                companyDTOs.Add(new CompanyDTO
                {
                    Id           = company.Id,
                    Name         = company.Name,
                    Email        = company.Email,
                    Password     = company.Password,
                    MobileNumber = company.MobileNumber,
                    Address      = company.Address,
                    City         = company.City,
                    Zipcode      = company.Zipcode
                });
            }
            return companyDTOs;
        }
        private Company PresLogicDTOToCompany(CompanyPresLogicDTO companyPresLogicDTO)
        {
           return new Company(
               this._companyJobDal, 
               this._jobDal, 
               companyPresLogicDTO.Id,
               companyPresLogicDTO.Name,
               companyPresLogicDTO.Email,
               companyPresLogicDTO.Password,
               companyPresLogicDTO.MobileNumber,
               companyPresLogicDTO.Address,
               companyPresLogicDTO.City, 
               companyPresLogicDTO.Zipcode
               );
        }

        private Company DTOToCompany(CompanyDTO companyDTO)
        {
            return new Company(
               this._companyJobDal,
               this._jobDal, 
               companyDTO.Id, 
               companyDTO.Name,
               companyDTO.Email,
               companyDTO.Password,
               companyDTO.MobileNumber,
               companyDTO.Address,
               companyDTO.City,
               companyDTO.Zipcode
               );
        }
    }
}
