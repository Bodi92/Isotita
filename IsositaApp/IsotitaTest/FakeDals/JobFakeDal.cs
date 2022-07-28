using Logic.Dtos;
using Logic.Interfacaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsotitaTest.FakeDals
{
    public class JobFakeDal : IJobDal, ICompanyJobDal, IJobContainerDal
    {
        private List<JobDTO> jobDTOList = new List<JobDTO>();
        public JobDTO JobDTO;
        public List<JobDTO> GetJobDTOs()
        {
            return this.jobDTOList;
        }

        public void AddJob(List<JobDTO> jobDTOs)
        {
            foreach (JobDTO jobDTO in jobDTOs)
            {
                this.JobDTO = new JobDTO()
                {
                    Company_DTO = new CompanyDTO()
                    {
                        Id = jobDTO.Company_DTO.Id
                    },
                    Id           = jobDTO.Id,
                    Title        = jobDTO.Title,
                    Description  = jobDTO.Description,
                    Requirements = jobDTO.Requirements,
                    Experience   = jobDTO.Experience,
                };
                jobDTOList.Add(this.JobDTO);
            }
        }

        public void Delete(int jobId)
        {
            jobDTOList.RemoveAll(j => j.Id == jobId);
        }

        public void Edit(JobDTO jobDTO)
        {
            this.JobDTO = new JobDTO()
            {
                Id           = 2,
                Title        = "OldTitle",
                Description  = "OldDescription",
                Requirements = "OldRequierments",
                Experience   = "OldExperience",
            };
            this.JobDTO.Id           = jobDTO.Id;
            this.JobDTO.Title        = jobDTO.Title;
            this.JobDTO.Description  = jobDTO.Description;
            this.JobDTO.Requirements = jobDTO.Requirements;
            this.JobDTO.Experience   = jobDTO.Experience;
        }

        public List<JobDTO> GetAllJobs()
        {
            jobDTOList.Add(new JobDTO()
            {
                Company_DTO = new CompanyDTO()
                {
                    Id = 1
                },
                Id = 1,
                Title = "Software Developer (C#)",
                Description = "Programmaing using C# to bulid hotels systems",
                Requirements = "Using Git, Using Azure",
                Experience = "3 years of experince with C# & ASP.NET Framework"
            });
            jobDTOList.Add(new JobDTO()
            {
                Company_DTO = new CompanyDTO()
                {
                    Id = 2
                },
                Id = 2,
                Title = "Software Developer (C#)",
                Description = "Programmaing using C# to bulid hotels systems",
                Requirements = "Using Git, Using Azure",
                Experience = "3 years of experince with C# & ASP.NET Framework"
            });
            jobDTOList.Add(new JobDTO()
            {
                Company_DTO = new CompanyDTO()
                {
                    Id = 3
                },
                Id = 3,
                Title = "Software Developer (C#)",
                Description = "Programmaing using C# to bulid hotels systems",
                Requirements = "Using Git, Using Azure",
                Experience = "3 years of experince with C# & ASP.NET Framework"
            });
            jobDTOList.Add(new JobDTO()
            {
                Company_DTO = new CompanyDTO()
                {
                    Id = 2
                },
                Id = 4,
                Title = "Software Developer (Laravel)",
                Description = "Programmaing using Laravel to bulid invoices systems",
                Requirements = "Using Git, Using Composer",
                Experience = "3 years of experince with Laravel & PHPUnit"
            });
            jobDTOList.Add(new JobDTO()
            {
                Company_DTO = new CompanyDTO()
                {
                    Id = 1
                },
                Id = 5,
                Title = "Software Developer (Laravel)",
                Description = "Programmaing using Laravel to bulid invoices systems",
                Requirements = "Using Git, Using Composer",
                Experience = "3 years of experince with Laravel & PHPUnit"
            });
            return this.jobDTOList;
        }

        public JobDTO GetJobById(int jobId)
        {
            JobDTO jobDTO = null;   
            switch (jobId)
            {
                case 1:
                    jobDTO = new JobDTO()
                    {
                        Company_DTO = new CompanyDTO()
                        {
                            Id = 1
                        },
                        Id = 1,
                        Title = "Software Developer (C#)",
                        Description = "Programmaing using C# to bulid hotels systems",
                        Requirements = "Using Git, Using Azure",
                        Experience = "3 years of experince with C# & ASP.NET Framework"
                    };
                    jobDTOList.Add(jobDTO);
                    break;
                case 2:
                    jobDTO = new JobDTO()
                    {
                        Company_DTO = new CompanyDTO()
                        {
                            Id = 2
                        },
                        Id = 2,
                        Title = "Software Developer (C#)",
                        Description = "Programmaing using C# to bulid hotels systems",
                        Requirements = "Using Git, Using Azure",
                        Experience = "3 years of experince with C# & ASP.NET Framework"
                    };
                    jobDTOList.Add(jobDTO);
                    break;
            }
            if (jobDTO == null)
            {
                return new JobDTO();
            }
            return jobDTO;
        }

        public List<JobDTO> GetJobs(int companyId)
        {
            switch (companyId)
            {
                case 1:
                    jobDTOList.Add(new JobDTO()
                    {
                        Company_DTO = new CompanyDTO()
                        {
                            Id = companyId
                        },
                        Id           = 1,
                        Title        = "Software Developer (C#)",
                        Description  = "Programmaing using C# to bulid hotels systems",
                        Requirements = "Using Git, Using Azure",
                        Experience   = "3 years of experince with C# & ASP.NET Framework"
                    });
                    jobDTOList.Add(new JobDTO()
                    {
                        Company_DTO = new CompanyDTO()
                        {
                            Id = companyId
                        },
                        Id = 1,
                        Title = "Software Developer (Laravel)",
                        Description = "Programmaing using Laravel to bulid invoices systems",
                        Requirements = "Using Git, Using Composer",
                        Experience = "3 years of experince with Laravel & PHPUnit"
                    });
                    break;
            }
            return this.jobDTOList;
        }
    }
}
