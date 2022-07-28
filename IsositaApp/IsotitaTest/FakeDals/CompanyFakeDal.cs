using Logic.Dtos;
using Logic.Interfacaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsotitaTest.FakeDals
{
    public class CompanyFakeDal : ICompanyDal
    {
        private List<CompanyDTO> companyDTOList = new List<CompanyDTO>();
        public CompanyDTO companyDTO;

        public void AddCompany(List<CompanyDTO> companyDTOs)
        {
            foreach(var companyDTO in companyDTOs)
            {
                this.companyDTO = new CompanyDTO()
                {
                    Id = companyDTO.Id,
                    Name = companyDTO.Name,
                    Email = companyDTO.Email,
                    Password = companyDTO.Password,
                    MobileNumber = companyDTO.MobileNumber,
                    Address = companyDTO.Address,
                    City = companyDTO.City,
                    Zipcode = companyDTO.Zipcode
                };
                this.companyDTOList.Add(this.companyDTO);
            }
        }
        public bool CheckIfCompanyExist(string email)
        {
            this.companyDTO = companyDTOList.ToList().Where(e => e.Email == email).FirstOrDefault();
            if (companyDTO != null)
            {
                return true;
            }
            return false;
        }

        public CompanyDTO GetCompanyById(int companyId)
        {
            CompanyDTO companyDTO;
            switch (companyId)
            {
                case 0:
                    companyDTO = null;
                    throw new ArgumentException("Er is geen bedrijf met dat aangegeven Id");
                    break;
                case 1:
                    return new CompanyDTO()
                    {
                        Id = 1,
                        Name = "Thrinix",
                        Email = "info@thrinix.com",
                        Password = "12345678",
                        MobileNumber = "0628790290",
                        Address = "Zwanenveld",
                        City = "Nijmegen",
                        Zipcode = "6538WT"
                    };
                    break;
                case 2:
                    return new CompanyDTO()
                    {
                        Id = 3,
                        Name = "IPPON",
                        Email = "info@ippon.nl",
                        Password = "12345678",
                        MobileNumber = "0628790290",
                        Address = "Zwanenveld",
                        City = "Nijmegen",
                        Zipcode = "6538WT"
                    };
                    break;
                default:
                    companyDTO = this.companyDTOList.ToList().Where(e => e.Id == companyId).FirstOrDefault();
                    if (companyDTO == null)
                    {
                        throw new ArgumentException("Er is geen bedrijf met dat aangegeven Id");
                    }
                    break;
            }
            return companyDTO;
        }

        public int GetCompanyId(string email, string password)
        {
            CompanyDTO companyDTO = this.companyDTOList.ToList().Where(e => e.Email == email && e.Password == password).FirstOrDefault();
            if (companyDTO == null)
            {
                throw new ArgumentException("Wrong Login Data");
            }
            return companyDTO.Id;
        }

        public List<CompanyDTO> GetCompanies()
        {
            return this.companyDTOList;
        }
    }
}
