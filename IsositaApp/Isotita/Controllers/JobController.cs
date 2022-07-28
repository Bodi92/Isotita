using Microsoft.AspNetCore.Mvc;
using Logic;
using Factory;
using Isotita.Models;
using Logic.PresentationLogicDTOs;

namespace Isotita.Controllers
{
    public class JobController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostEnvironment;
        private JobContainer jobContainer;
        private int companyId;
        public JobController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            if (this._httpContextAccessor.HttpContext.Request.Cookies["companyId"] != null)
            {
                this.companyId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Request.Cookies["companyId"]);
                this.jobContainer = new JobContainer(
                    JobDalFactory.CreateJobContanierDal(),
                    JobDalFactory.CreateJobDal(), 
                    AppliedDalFactory.CreateJobAppliedDal(),
                    AppliedDalFactory.CreateAppliedDal()
                    );
            }
        }
        // GET: JobController/Route/5
        [Route("/Job/Applieds")]
        public ActionResult GetAppliedsPerJob(int jobId)
        {
            if (this.companyId == 0)
            {
                return StatusCode(403);
            }
            Job job = this.jobContainer.GetById(jobId);
            List<Applied> appliedsPerJob = job.GetApplieds();
            JobViewModel jobViewModel = new JobViewModel()
            {
                Id                      = job.Id,
                Title                   = job.Title,
                Description             = job.Description,
                Requirements            = job.Requirements,
                Experience              = job.Experience,
            };
            jobViewModel.appliedStatusViewModels  = new List<AppliedStatusViewModel>();
            foreach(var applied in appliedsPerJob)
            {
                jobViewModel.appliedStatusViewModels.Add(new AppliedStatusViewModel
                {
                    Email            = applied.Email,
                    CV               = applied.CV,
                    MotivationLetter = applied.MotivationLetter,
                    Status           = applied.Status,
                    Treated          = applied.Treated
                });
            }
            return View(jobViewModel);
        }

        // GET: JobController/Edit/5
        public ActionResult Edit(int jobId)
        {
            if (this.companyId == 0)
            {
                return StatusCode(403);
            }
            Job job = this.jobContainer.GetById(jobId);
            JobViewModel jobViewModel = new JobViewModel()
            {
                Id           = job.Id,
                Title        = job.Title,
                Description  = job.Description,
                Requirements = job.Requirements,
                Experience   = job.Experience
            };
            return View(jobViewModel);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobViewModel jobViewModel)
        {
            if (this.companyId == 0)
            {
                return StatusCode(403);
            }
            Job job = this.jobContainer.GetById(jobViewModel.Id);
            JobPresLogicDTO jobPresLogicDTO = new JobPresLogicDTO()
            {
                Id           = jobViewModel.Id,
                Title        = jobViewModel.Title,
                Description  = jobViewModel.Description,
                Requirements = jobViewModel.Requirements,
                Experience   = jobViewModel.Experience
            };
            try
            {
                job.Edit(jobPresLogicDTO);
                TempData["JobUpdated"] = "Vacature is succesvol aangepast";
                return RedirectToAction("Index", "Company");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        // Delete: JobController/Delete/5
        public ActionResult Delete(int jobId)
        {
            if (this.companyId == 0)
            {
                return StatusCode(403);
            }
            try
            {
                Job job = this.jobContainer.GetById(jobId);
                job.Delete();
                TempData["JobDeleted"] = "Vacature is succevol verwijderd";
                return RedirectToAction("Index", "Company");
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }
        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this._hostEnvironment.WebRootPath, "uploadedFiles/") + fileName;
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

    }
}
