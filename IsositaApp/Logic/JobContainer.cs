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
    public class JobContainer
    {
        private IJobContainerDal _jobContanierDal;
        private IJobDal _jobDal;
        private IJobAppliedDal _jobAppliedDal;
        private IAppliedDal _appliedDal;
        private List<Job> Jobs;

        public JobContainer(IJobContainerDal jobContanierDal, IJobDal jobDal, IJobAppliedDal jobAppliedDal, IAppliedDal appliedDal)
        {
            this._jobContanierDal = jobContanierDal;
            this._jobDal = jobDal;
            this._jobAppliedDal = jobAppliedDal;
            this._appliedDal = appliedDal;
        }

        public List<Job> GetJobs()
        {
            List<JobDTO> jobDTOs = this._jobContanierDal.GetAllJobs();
            this.Jobs = new List<Job>();
            foreach (var jobDTO in jobDTOs)
            {
                this.Jobs.Add(
                    new Job(
                        this._jobDal,
                        this._jobAppliedDal,
                        this._appliedDal,
                        this.DTOtoCompany(jobDTO.Company_DTO),
                        jobDTO.Id,
                        jobDTO.Title,
                        jobDTO.Description,
                        jobDTO.Requirements,
                        jobDTO.Experience
                        )
                    );
            }
            return this.Jobs;
        }
        public Job GetById(int jobId)
        {
            JobDTO jobDTO = this._jobContanierDal.GetJobById(jobId);
            Job job = new Job(
                this._jobDal,
                this._jobAppliedDal,
                this._appliedDal,
                this.DTOtoCompany(jobDTO.Company_DTO),
                jobDTO.Id,
                jobDTO.Title,
                jobDTO.Description,
                jobDTO.Requirements,
                jobDTO.Experience
            );
            return job;
        }

        private Company DTOtoCompany(CompanyDTO companyDTO)
        {
            if (companyDTO != null)
            {
                return new Company(companyDTO.Id, companyDTO.Name);
            }
            return new Company(0, "");
        }
    }
}
