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
    public class Job
    {
        private int id;
        private string title;
        private string description;
        private string requierments;
        private string experince;

        public int Id { get { return id; } private set { id = value; } }
        public Company Company { get; private set; }
        public string Title { get { return title; } private set { title = value; } }
        public string Description { get { return description; } private set { description = value; } }
        public string Requirements { get { return requierments; } private set { requierments = value; } }
        public string Experience { get { return experince; } private set { experince = value; } }
        private List<Applied> applieds = new List<Applied>();
        public IEnumerable<Applied> Applieds => applieds;
        private IJobDal _jobDal;
        private IJobAppliedDal _jobAppliedDal;
        private IAppliedDal _appliedDal;
        public Job(IJobDal jobDal, IJobAppliedDal jobAppliedDal, IAppliedDal appliedDal, Company company, int id,
         string title, string description, string requierments, string experince)
        {
            this._jobDal        = jobDal;
            this._jobAppliedDal = jobAppliedDal;
            this._appliedDal    = appliedDal;
            this.Id             = id;
            this.Company        = company;
            this.Title          = title;
            this.Description    = description;
            this.Requirements   = requierments;
            this.Experience     = experince;
        }
        public Job(IJobDal jobDal, Company company, int id,
         string title, string description, string requierments, string experince)
        {
            this._jobDal      = jobDal;
            this.Id           = id;
            this.Company      = company;
            this.Title        = title;
            this.Description  = description;
            this.Requirements = requierments;
            this.Experience   = experince;
        }

        public List<Applied> GetApplieds()
        {
            List<AppliedDTO> appliedDTOs = this._jobAppliedDal.GetAppliedsPerJob(this.Id);
            this.applieds = new List<Applied>();
            foreach (var appliedDTO in appliedDTOs)
            {
                this.applieds.Add(
                    new Applied(
                        this._appliedDal,
                        this,
                        appliedDTO.Email,
                        appliedDTO.CV,
                        appliedDTO.MotivationLetter,
                        appliedDTO.Status,
                        appliedDTO.Treated
                        )
                    );
            }
            return applieds;
        }

        public void Edit(JobPresLogicDTO jobPresLogicDTO)
        {
            this.Id           = jobPresLogicDTO.Id;
            this.Title        = jobPresLogicDTO.Title;
            this.Description  = jobPresLogicDTO.Description;
            this.Requirements = jobPresLogicDTO.Requirements;
            this.Experience   = jobPresLogicDTO.Experience;
            this._jobDal.Edit(this.JobToDTO());
        }

        public void Delete()
        {
            this._jobDal.Delete(this.Id);
        }

        public bool CheckIfApplied(string email)
        {
            return this._jobAppliedDal.CheckIfAppliedOnJob(this.Id, email);
        }

        public void Apply(AppliedPresLogicDTO appliedPresLogicDTO)
        {
            Applied applied = this.PresLogicDTOtoApplied(appliedPresLogicDTO);
            this.applieds.Add(applied);
            this._jobAppliedDal.ApplyOnJob(this.AppliedsToDTOs());
        }

        public Applied GetAppliedByJobIdAndEmail(int jobId, string email)
        {
            AppliedDTO appliedDTO = this._jobAppliedDal.GetAppliedByJobIdAndEmail(jobId, email);
            return this.DTOtoApplied(appliedDTO);
        }

        private JobDTO JobToDTO()
        {
            return new JobDTO
            {
                Id           = this.Id,
                Title        = this.Title,
                Description  = this.Description,
                Requirements = this.Requirements,
                Experience   = this.Experience
            };
        }

        private Applied DTOtoApplied(AppliedDTO appliedDTO)
        {
            return new Applied(
                this._appliedDal,
                this,
                appliedDTO.Email,
                appliedDTO.CV,
                appliedDTO.MotivationLetter,
                appliedDTO.Status, 
                appliedDTO.Treated
                );
        }
        private Applied PresLogicDTOtoApplied(AppliedPresLogicDTO appliedPresLogicDTO)
        {
            return new Applied(
                this._appliedDal,
                this,
                appliedPresLogicDTO.Email,
                appliedPresLogicDTO.CV,
                appliedPresLogicDTO.MotivationLetter,
                appliedPresLogicDTO.Status,
                appliedPresLogicDTO.Treated
                );
        }

        private AppliedDTO AppliedToDTO(Applied applied)
        {
            return new AppliedDTO
            {
                Job_DTO          = this.JobToDTO(),
                Email            = applied.Email,
                CV               = applied.CV,
                MotivationLetter = applied.MotivationLetter,
                Status           = applied.Status,
                Treated          = applied.Treated
            };
        }
        private List<AppliedDTO> AppliedsToDTOs()
        {
            List<AppliedDTO> appliedDTOs = new List<AppliedDTO>();
            foreach(Applied applied in this.applieds)
            {
                appliedDTOs.Add(new AppliedDTO()
                {
                    Job_DTO          = this.JobToDTO(),
                    Email            = applied.Email,
                    CV               = applied.CV,
                    MotivationLetter = applied.MotivationLetter,
                    Status           = applied.Status,
                    Treated          = applied.Treated
                });
            }
            return appliedDTOs;
        }
    }
}
