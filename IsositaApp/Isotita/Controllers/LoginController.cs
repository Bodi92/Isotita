using Factory;
using Isotita.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Isotita.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private CompanyContainer companyContanier;
        private int companyId;
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this.companyContanier = new CompanyContainer(
                CompanyDalFactory.CreateCompanyDal(), 
                JobDalFactory.CreateCompanyJobDal(),
                JobDalFactory.CreateJobDal()
                );
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View();
            this.companyId = this.companyContanier.GetCompanyId(loginViewModel.Email, loginViewModel.Password);
            if (this.companyId > 0)
            {
                this.SetCurrentCompanyId(this.companyId);
                return RedirectToAction("Index", "Company");
            }
            TempData["WrongUser"] = "Het ingevoerde e-mailadres of watchwoord zijn onjuist.";
            return RedirectToAction("Index");
        }
        private void SetCurrentCompanyId(int companyId)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("companyId", companyId.ToString(), option);
        }
        public IActionResult Logout()
        {
            if (this._httpContextAccessor.HttpContext.Request.Cookies["companyId"].ToString() != null)
            {
                this.RemoveCurrentCompanyId();
                return RedirectToAction("Index", "Home");
            }
            return StatusCode(403);
        }
        private void RemoveCurrentCompanyId()
        {
            Response.Cookies.Delete("companyId");
        }

    }
}
