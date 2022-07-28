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
    public class JobTest
    {
        JobFakeDal jobContainerFakeDal       = new JobFakeDal();
        JobFakeDal jobFakeDal                = new JobFakeDal();
        AppliedFakeDal jobAppliedFakeDal     = new AppliedFakeDal();
        AppliedFakeDal appliedFakeDal        = new AppliedFakeDal();

        [Fact]
        public void EditJobTest()
        {
            //Arrange
            JobContainer jobContainer = new JobContainer(this.jobContainerFakeDal, this.jobFakeDal, this.jobAppliedFakeDal, this.appliedFakeDal);
            Job job = jobContainer.GetById(2);
            var jobPresLogicDTO = new JobPresLogicDTO()
            {
                Id           = job.Id,
                Title        = "Software Developer (C#)",
                Description  = "Programmaing using C# to bulid hospitals systems",
                Requirements = "Using Git, Using Azure, Using Visual Studio",
                Experience   = "5 years of experince with C# & ASP.NET Framework"
            };
            //Act
            job.Edit(jobPresLogicDTO);
            //Assert
            Assert.Equal(job.Id, this.jobFakeDal.JobDTO.Id);
            Assert.Equal(jobPresLogicDTO.Title, this.jobFakeDal.JobDTO.Title);
            Assert.Equal(jobPresLogicDTO.Description, this.jobFakeDal.JobDTO.Description);
            Assert.Equal(jobPresLogicDTO.Requirements, this.jobFakeDal.JobDTO.Requirements);
            Assert.Equal(jobPresLogicDTO.Experience, this.jobFakeDal.JobDTO.Experience);
        }
        [Fact]
        public void DeleteJobById1Test()
        {
            //Arrange
            JobContainer jobContainer = new JobContainer(this.jobContainerFakeDal, this.jobFakeDal, this.jobAppliedFakeDal, this.appliedFakeDal);
            CompanyTest companyTest = new CompanyTest();
            companyTest.AddJobTest();
            Job job = jobContainer.GetById(1);
            //Act
            job.Delete();
            //Assert
            Assert.True(this.jobFakeDal.GetJobDTOs().Count() == 0);
            Assert.True(this.jobFakeDal.GetJobDTOs().Where(j => j.Id == 1).Count() == 0);
        }
    }
}
