using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IsotitaTest.FakeDals;
using Logic;
using Logic.PresentationLogicDTOs;
using Logic.Dtos;

namespace IsotitaTest
{
    public class JobContainerTest
    {
        JobFakeDal jobContainerFakeDal   = new JobFakeDal();
        JobFakeDal jobFakeDal            = new JobFakeDal();
        AppliedFakeDal jobAppliedFakeDal = new AppliedFakeDal();
        AppliedFakeDal appliedFakeDal    = new AppliedFakeDal();

        [Fact]
        public void GetJobsTest()
        {
            //Arrange 
            JobContainer jobContainer = new JobContainer(this.jobContainerFakeDal, this.jobFakeDal, this.jobAppliedFakeDal, this.appliedFakeDal);
            //Act
            List<Job> jobs = jobContainer.GetJobs();
            //Assert
            Assert.NotNull(jobs);
            for (int i = 0; i < jobs.Count; i ++)
            {
                Assert.Equal(jobs[i].Id, jobContainerFakeDal.GetJobDTOs()[i].Id);
                Assert.Equal(jobs[i].Company.Id, jobContainerFakeDal.GetJobDTOs()[i].Company_DTO.Id);
                Assert.Equal(jobs[i].Title, jobContainerFakeDal.GetJobDTOs()[i].Title);
                Assert.Equal(jobs[i].Description, jobContainerFakeDal.GetJobDTOs()[i].Description);
                Assert.Equal(jobs[i].Requirements, jobContainerFakeDal.GetJobDTOs()[i].Requirements);
                Assert.Equal(jobs[i].Experience, jobContainerFakeDal.GetJobDTOs()[i].Experience);
            }
        }
        [Fact]
        public void GetJobById3Test()
        {
            //Arrange 
            JobContainer jobContainer = new JobContainer(this.jobContainerFakeDal, this.jobFakeDal, this.jobAppliedFakeDal, this.appliedFakeDal);
            //Act
            Job job = jobContainer.GetById(3);
            //Assert
            Assert.Equal(0, job.Id);
            Assert.Equal(0, job.Company.Id);
            Assert.Null(job.Title);
            Assert.Null(job.Description);
            Assert.Null(job.Requirements);
            Assert.Null(job.Experience);
        }
        [Fact]
        public void GetJobById2Test()
        {
            //Arrange 
            JobContainer jobContainer = new JobContainer(this.jobContainerFakeDal, this.jobFakeDal, this.jobAppliedFakeDal, this.appliedFakeDal);
            //Act
            Job job = jobContainer.GetById(2);
            //Assert
            foreach (JobDTO jobDTO in this.jobContainerFakeDal.GetJobDTOs())
            {
                Assert.Equal(job.Id, jobDTO.Id);
                Assert.Equal(job.Company.Id, jobDTO.Company_DTO.Id);
                Assert.Equal(job.Title, jobDTO.Title);
                Assert.Equal(job.Description, jobDTO.Description);
                Assert.Equal(job.Requirements, jobDTO.Requirements);
                Assert.Equal(job.Experience, jobDTO.Experience);
            }
        }
    }
}
