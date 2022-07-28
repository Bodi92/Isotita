using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Logic;
using Logic.Dtos;
using Logic.PresentationLogicDTOs;
using IsotitaTest.FakeDals;

namespace IsotitaTest
{
    public class CompanyContainerTest
    {

        private CompanyFakeDal companyFakeDal       = new CompanyFakeDal();
        private JobFakeDal companyJobFakeDal        = new JobFakeDal();
        private JobFakeDal jobFakeDal               = new JobFakeDal();

        [Fact]
        public void AddCompanyTest()
        {
            //Arrange
            var companyPresLogicDTO = new CompanyPresLogicDTO()
            {
                Id           = 1,
                Name         = "Thrinix",
                Email        = "info@thrinix.com",
                Password     = "12345678",
                MobileNumber = "0628790290",
                Address      = "Zwanenveld",
                City         = "Nijmegen",
                Zipcode      = "6538WT"
            };
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            //Act
            companyContainer.AddCompany(companyPresLogicDTO);
            //Assert
            Assert.Equal(1, this.companyFakeDal.companyDTO.Id);
            Assert.Equal("Thrinix", this.companyFakeDal.companyDTO.Name);
            Assert.Equal("info@thrinix.com", this.companyFakeDal.companyDTO.Email);
            Assert.Equal("12345678", this.companyFakeDal.companyDTO.Password);
            Assert.Equal("0628790290", this.companyFakeDal.companyDTO.MobileNumber);
            Assert.Equal("Zwanenveld", this.companyFakeDal.companyDTO.Address);
            Assert.Equal("Nijmegen", this.companyFakeDal.companyDTO.City);
            Assert.Equal("6538WT", this.companyFakeDal.companyDTO.Zipcode);
            Assert.Equal(1, this.companyFakeDal.GetCompanies().Count());
        }
        [Fact]
        public void CheckIfCompanyExistByRegisteredEmailTest()
        {
            //Arrange
            string email = "info@thrinix.com";
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            this.AddCompanyTest();
            //Act
            bool result = companyContainer.CheckIfExist(email);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void CheckIfCompanyExistByNotRegisteredEmailTest()
        {
            //Arrange
            string email = "info@ippon.com";
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            //Act
            bool result = companyContainer.CheckIfExist(email);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void GetCompanyById1Test()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            //Act
            Company company = companyContainer.GetById(1);
            //Assert
            Assert.NotNull(company);
            Assert.Equal(1, company.Id);
            Assert.Equal("Thrinix", company.Name);
            Assert.Equal("info@thrinix.com", company.Email);
            Assert.Equal("12345678", company.Password);
            Assert.Equal("0628790290", company.MobileNumber);
            Assert.Equal("Zwanenveld", company.Address);
            Assert.Equal("Nijmegen", company.City);
            Assert.Equal("6538WT", company.Zipcode);
        }
        [Fact]
        public void GetCompanyBy0IdTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            //Act
            Action NewCompany = () => companyContainer.GetById(0);
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(NewCompany);
            Assert.Equal("Er is geen bedrijf met dat aangegeven Id", exception.Message);
        }
        [Fact]
        public void GetCompanyByNotExistIdTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            //Act
            Action NewCompany = () => companyContainer.GetById(50);
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(NewCompany);
            Assert.Equal("Er is geen bedrijf met dat aangegeven Id", exception.Message);
        }
        [Fact]
        public void GetCompanyIdWithCurrentDataTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            this.AddCompanyTest();
            //Act
            int companyId = companyContainer.GetCompanyId("info@thrinix.com", "12345678");
            //Assert
            Assert.NotEqual(0, companyId);
            Assert.Equal(companyId, companyFakeDal.companyDTO.Id);
            
        }
        [Fact]
        public void GetCompanyIdByWrongEmailTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            this.AddCompanyTest();
            //Act
            Action GetCompanyId = () => companyContainer.GetCompanyId("info@thrinix11.com", "12345678");
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(GetCompanyId);
            Assert.Equal("Wrong Login Data", exception.Message);
        }
        [Fact]
        public void GetCompanyIdByWrongPasswordTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            this.AddCompanyTest();
            //Act
            Action GetCompanyId = () => companyContainer.GetCompanyId("info@thrinix.com", "1234567899");
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(GetCompanyId);
            Assert.Equal("Wrong Login Data", exception.Message);
        }
    }
}
