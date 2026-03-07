using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccounViewModels;
using GymManagementDAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
           _accountService = accountService;
           _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _accountService.ValidateUser(model);
            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

             var result=await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if(result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Not Allowed");

            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);


        }

        public async Task <IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
