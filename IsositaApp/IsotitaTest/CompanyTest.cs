using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsotitaTest.FakeDals;
using Logic;
using Logic.PresentationLogicDTOs;
using Logic.Dtos;
using Xunit;

namespace IsotitaTest
{
    public class CompanyTest
    {
        private CompanyFakeDal companyFakeDal     = new CompanyFakeDal();
        private JobFakeDal companyJobFakeDal      = new JobFakeDal();
        private JobFakeDal jobFakeDal             = new JobFakeDal();

        [Fact]
        public void AddJobTest()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            Company company = companyContainer.GetById(2);
            var jobPresLogicDTO = new JobPresLogicDTO()
            {
                Id            = 1,
                Title         = "Software Developer (C#)",
                Description   = "Programmaing using C# to bulid hotels systems",
                Requirements  = "Using Git, Using Azure",
                Experience    = "3 years of experince with C# & ASP.NET Framework"
            };
            //Act
            company.AddJob(jobPresLogicDTO);
            //Assert
            foreach (JobDTO jobDTO in this.companyJobFakeDal.GetJobDTOs().Where(j => j.Id == 1))
            {
                Assert.Equal(1, jobDTO.Id);
                Assert.Equal(company.Id, jobDTO.Company_DTO.Id);
                Assert.Equal("Software Developer (C#)", jobDTO.Title);
                Assert.Equal("Programmaing using C# to bulid hotels systems", jobDTO.Description);
                Assert.Equal("Using Git, Using Azure", jobDTO.Requirements);
                Assert.Equal("3 years of experince with C# & ASP.NET Framework", jobDTO.Experience);
            }
        }
        [Fact]
        public void GetJobsByCompanyId1Test()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            Company company = companyContainer.GetById(1);
            //Act
            List<Job> jobs = company.GetJobs();
            //Assert
            Assert.NotNull(jobs);
            Assert.Equal(2, jobs.Count);
            for(int i = 0; i < jobs.Count; i++)
            {
                Assert.Equal(jobs[i].Id, companyJobFakeDal.GetJobDTOs()[i].Id);
                Assert.Equal(jobs[i].Company.Id, companyJobFakeDal.GetJobDTOs()[i].Company_DTO.Id);
                Assert.Equal(jobs[i].Title, companyJobFakeDal.GetJobDTOs()[i].Title);
                Assert.Equal(jobs[i].Description, companyJobFakeDal.GetJobDTOs()[i].Description);
                Assert.Equal(jobs[i].Requirements, companyJobFakeDal.GetJobDTOs()[i].Requirements);
                Assert.Equal(jobs[i].Experience, companyJobFakeDal.GetJobDTOs()[i].Experience);
            }
        }
        [Fact]
        public void GetJobsByCompanyId2Test()
        {
            //Arrange
            CompanyContainer companyContainer = new CompanyContainer(this.companyFakeDal, this.companyJobFakeDal, this.jobFakeDal);
            Company company = companyContainer.GetById(2);
            //Act
            List<Job> jobs = company.GetJobs();
            //Assert
            Assert.Empty(jobs);
        }
    }
}
