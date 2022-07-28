using Isotita.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Factory;
using Logic.PresentationLogicDTOs;

namespace Isotita.Controllers
{
    public class AppliedController : Controller
    {
        private JobContainer jobContainer;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AppliedController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            this.jobContainer = new JobContainer(
                JobDalFactory.CreateJobContanierDal(), 
                JobDalFactory.CreateJobDal(), 
                AppliedDalFactory.CreateJobAppliedDal(), 
                AppliedDalFactory.CreateAppliedDal()
                );
        }
        // GET: AppliedController
        public async Task<IActionResult> Index()
        {
            List<JobViewModel> jobsViewModel = new List<JobViewModel>();
            List<Job> jobs = this.jobContainer.GetJobs();
            foreach (var job in jobs)
            {
                jobsViewModel.Add(
                new JobViewModel()
                {
                    companyViewModel = new CompanyViewModel()
                    {
                       Id   = job.Company.Id,
                       Name = job.Company.Name
                    },
                    Id          = job.Id,
                    Title       = job.Title,
                    Description = job.Description
                });
            }
            return View(jobsViewModel);
        }

        // GET: AppliedController/Details/5
        public async Task<IActionResult> Details(int jobId)
        {
            Job job  = this.jobContainer.GetById(jobId);
            JobViewModel jobViewModel = new JobViewModel()
            {
                companyViewModel = new CompanyViewModel()
                {
                    Id   = job.Company.Id,
                    Name = job.Company.Name
                },
                Id           = job.Id,
                Title        = job.Title,
                Description  = job.Description,
                Requirements = job.Requirements,
                Experience   = job.Experience
            };
            return View(jobViewModel);
        }

        // GET: AppliedController/Create
        public ActionResult ApplyOnJob(int jobId)
        {
            ViewData["jobId"] = jobId;
            return View();
        }

        // POST: AppliedController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyOnJob(AppliedViewModel appliedViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appliedViewModel);
            }
            Job job = jobContainer.GetById(appliedViewModel.JobId);
            if (job.CheckIfApplied(appliedViewModel.Email))
            {
                TempData["AppliedExist"] = "U heeft eerder gesolliciteerd op deze vacature!";
                return RedirectToAction("ApplyOnJob", new { jobId = appliedViewModel.JobId });
            }
            AppliedPresLogicDTO appliedPresLogicDTO = new AppliedPresLogicDTO()
            {
                Email            = appliedViewModel.Email,
                CV               = SaveUploadedFiles(appliedViewModel.CV),
                MotivationLetter = SaveUploadedFiles(appliedViewModel.MotivationLetter),
                Status           = false,
                Treated          = false,
            };
            job.Apply(appliedPresLogicDTO);
            TempData["AppliedSuccessvol"] = "U anonieme sollicitatie is verzonden!";
            return RedirectToAction(nameof(Index));
        }
        public ActionResult ChangeStatus(bool status, int jobId, string email)
        {
            try
            {
                Job job = jobContainer.GetById(jobId);
                Applied applied = job.GetAppliedByJobIdAndEmail(jobId, email);
                applied.ChangeStatus(status, email);
                TempData["success_message"] = "Sollicitatie is behandeld";
                return RedirectToAction("GetAppliedsPerJob", "Job", new
                {
                    jobId = jobId,
                });
            }
            catch
            {
                TempData["error_message"] = "Er is iets misgegeaan, probeer het later opnieuw";
                return RedirectToAction("GetAppliedsPerJob", "Job", new
                {
                    jobId = jobId,
                });
            }
        }
        private string SaveUploadedFiles(IFormFile file)
        {
            string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploadedFiles");
            string uniqeFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadFolder, uniqeFileName);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Close();
            return uniqeFileName;
        }
    }
}
