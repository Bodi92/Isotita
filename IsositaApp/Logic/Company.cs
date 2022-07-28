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
    public class Company
    {
        private int id;
        private string name;
        private string email;
        private string password;
        private string mobile_number;
        private string address;
        private string city;
        private string zipcode;
        private List<Job> jobs = new List<Job>();
        public IEnumerable<Job> Jobs => jobs;
        private ICompanyJobDal _companyJobDal;
        private IJobDal _jobDal;
        public int Id { get { return id; } private set { id = value; } }
        public string Name { get { return name; } private set { name = value; } }
        public string Email { get { return email; } private set { email = value; } }
        public string Password { get { return password; } private set { password = value; } }
        public string MobileNumber { get { return mobile_number; } private set { mobile_number = value; } }
        public string Address { get { return address; } private set { address = value; } }
        public string City { get { return city; } private set { city = value; } }
        public string Zipcode { get { return zipcode; } private set { zipcode = value; } }
        public Company(ICompanyJobDal companyJobDal, IJobDal jobDal, int id, string name, string email, string password, string mobileNumber, string address, string city, string zipcode)
        {
            this._companyJobDal  = companyJobDal;
            this._jobDal         = jobDal;
            this.Id              = id;
            this.Name            = name;
            this.Email           = email;
            this.Password        = password;
            this.MobileNumber    = mobileNumber;
            this.Address         = address;
            this.City            = city;
            this.Zipcode         = zipcode;
        }
        public Company(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public void AddJob(JobPresLogicDTO jobPresLogicDTO)
        {
            Job job = this.DTOToJob(jobPresLogicDTO);
            this.jobs.Add(job);
            _companyJobDal.AddJob(JobsToDTOs());
        }

        public List<Job> GetJobs()
        {
            var companyDTO = new CompanyDTO()
            {
                jobs = _companyJobDal.GetJobs(this.Id)
            };
            foreach (var jobDTO in companyDTO.jobs)
            {
                this.jobs.Add(new Job(
                    this._jobDal,
                    this,
                    jobDTO.Id,
                    jobDTO.Title,
                    jobDTO.Description,
                    jobDTO.Requirements,
                    jobDTO.Experience
                    ));
            }
            return this.jobs;
        }

        private List<JobDTO> JobsToDTOs()
        {
            List<JobDTO> jobDTOs = new List<JobDTO>();
            foreach(Job job in this.Jobs)
            {
                jobDTOs.Add(new JobDTO
                {
                    Id           = job.Id,
                    Company_DTO  = this.CompanyToDTO(),
                    Title        = job.Title,
                    Description  = job.Description,
                    Requirements = job.Requirements,
                    Experience   = job.Experience
                });
            }
            return jobDTOs;
        }

        private CompanyDTO CompanyToDTO()
        {
            return new CompanyDTO
            {
                Id           = this.Id,
                Name         = this.Name,
                Email        = this.Email,
                Password     = this.Password,
                MobileNumber = this.MobileNumber,
                Address      = this.Address,
                City         = this.City,
                Zipcode      = this.Zipcode
            };
        }

        private Job DTOToJob(JobPresLogicDTO jobPresLogicDTO)
        {
            return new Job(
                this._jobDal,
                this,
                jobPresLogicDTO.Id,
                jobPresLogicDTO.Title,
                jobPresLogicDTO.Description,
                jobPresLogicDTO.Requirements,
                jobPresLogicDTO.Experience
                );
        }
    }
}
