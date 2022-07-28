using Logic.Dtos;
using Logic.Interfacaes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Applied
    {
        private int id;
        private string email;
        private string cv;
        private string motivationLetter;
        private bool status;
        private bool treated;
        private IAppliedDal _appliedDal;

        public Applied(IAppliedDal appliedDal, Job job,  string email, string cv, string motivitionLetter, bool status, bool treated)
        {
            this._appliedDal      = appliedDal;
            this.Job              = job;
            this.Email            = email;
            this.CV               = cv;
            this.MotivationLetter = motivitionLetter;
            this.Status           = status;
            this.Treated          = treated;
        }

        public int Id { get { return id; } private set { id = value; } }
        public Job Job { get; private set; }
        public string Email { get { return email; } private set { email = value; } }
        public string CV { get { return cv; } private set { cv = value; } }
        public string MotivationLetter { get { return motivationLetter; } private set { motivationLetter = value; } }
        public bool Status { get { return status; } private set { status = value; } }
        public bool Treated { get { return treated; } private set { treated = value; } }

        public void ChangeStatus(bool status, string email)
        {
            AppliedDTO appliedDTO = new AppliedDTO()
            {
                Status  = status,
                Job_DTO = this.JobToDTO(),
                Email   = email
            };
            _appliedDal.ChangeApplicantAppliedStatus(appliedDTO);
        }

        private JobDTO JobToDTO()
        {
            return new JobDTO
            {
                Id           = this.Job.Id,
                Title        = this.Job.Title,
                Description  = this.Job.Description,
                Requirements = this.Job.Requirements,
                Experience   = this.Job.Experience,
            };
        }
    }
}
