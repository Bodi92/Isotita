using Factory;
using Isotita.Models;
using Logic;
using Logic.PresentationLogicDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Isotita.Controllers
{
    public class CompanyController : Controller
    {
        private int companyId;
        private CompanyContainer companyContainer;
        IHttpContextAccessor _httpContextAccessor;
        public CompanyController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            if (this._httpContextAccessor.HttpContext.Request.Cookies["companyId"] != null)
            {
                this.companyId = Convert.ToInt32(this._httpContextAccessor.HttpContext.Request.Cookies["companyId"]);
                this.companyContainer = new CompanyContainer(
                    CompanyDalFactory.CreateCompanyDal(), 
                    JobDalFactory.CreateCompanyJobDal(), 
                    JobDalFactory.CreateJobDal()
                    );
            }
        }
        public IActionResult Index()
        {
            if (this.companyId > 0)
            {
                Company company = this.companyContainer.GetById(this.companyId);
                List<Job> jobs = company.GetJobs();
                List<JobViewModel> jobViewModels = new List<JobViewModel>();
                for (int i = 0; i < jobs.Count(); i++)
                {
                    jobViewModels.Add(new JobViewModel
                    {
                        Id          = jobs[i].Id,
                        Title       = jobs[i].Title,
                        Description = jobs[i].Description,
                    });
                }
                return View(jobViewModels);
            }
            return StatusCode(403);
        }
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(JobViewModel jobViewModel)
        {
            Company company = this.companyContainer.GetById(this.companyId);
            if (this.companyId > 0)
            {
                try
                {
                    if (!ModelState.IsValid) return View();
                    JobPresLogicDTO jobPresLogicDTO = new JobPresLogicDTO()
                    {
                        Title        = jobViewModel.Title,
                        Description  = jobViewModel.Description,
                        Requirements = jobViewModel.Requirements,
                        Experience   = jobViewModel.Experience
                    };
                    company.AddJob(jobPresLogicDTO);
                    TempData["JobAdded"] = "Vacature is succesvol aangemaakt!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return StatusCode(403);
        }
    }
}
