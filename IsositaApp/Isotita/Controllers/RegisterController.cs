using Factory;
using Isotita.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Logic.PresentationLogicDTOs;

namespace Isotita.Controllers
{
    public class RegisterController : Controller
    {
        private CompanyContainer companyContanier;

        public RegisterController()
        {
            this.companyContanier = new CompanyContainer(
                CompanyDalFactory.CreateCompanyDal(), 
                JobDalFactory.CreateCompanyJobDal(), 
                JobDalFactory.CreateJobDal()
                );
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View();

            if (CheckIfCompanyExist(registerViewModel.Email))
            {
                TempData["ErrorMessage"] = "Email bestaat al";
                return RedirectToAction("Index");
            }
            try
            {
                this.RegisterCompany(registerViewModel);
                TempData["Registered"] = "Uw registratie is voltooid";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        private void RegisterCompany(RegisterViewModel registerViewModel)
        {
            var companyPresLogicDTO = new CompanyPresLogicDTO()
            {
                Name         = registerViewModel.Name,
                Email        = registerViewModel.Email,
                Password     = registerViewModel.Password,
                MobileNumber = registerViewModel.MobileNumber,
                Address      = registerViewModel.Address,
                City         = registerViewModel.City,
                Zipcode      = registerViewModel.Zipcode
            };
            this.companyContanier.AddCompany(companyPresLogicDTO);
        }
        private bool CheckIfCompanyExist(string email)
        {
            return companyContanier.CheckIfExist(email);
        }
    }
}
