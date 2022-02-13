using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Models;
using PizzaREAL.ModelsIdentity;
using PizzaREAL.Services;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
        }

        [Authorize]
        public IActionResult GetUsers()
        {
            return View(_accountService.GetAccountViewModel());
        }

        [Authorize]
        public async Task<IActionResult> UpdateToPremium(string id)
        {
            if (await _accountService.UpdateRoleAsync(id, "Premium"))
            {
                ViewBag.Account = "Kontot uppdaterades";
            }

            else
            {
                ViewBag.Account = "Något gick fel var god försök igen";
            }
            
            return View("GetUsers", _accountService.GetAccountViewModel());
        }

        [Authorize]
        public async Task<IActionResult> UpdateToStandard(string id)
        {
            if (await _accountService.UpdateRoleAsync(id, "Standard"))
            {
                ViewBag.Account = "Kontot uppdaterades";
            }

            else
            {
                ViewBag.Account = "Något gick fel var god försök igen";
            }

            return View("GetUsers", _accountService.GetAccountViewModel());
        }
        [Authorize]
        public async Task<IActionResult> UpdateToAdmin(string id)
        {
            if (await _accountService.UpdateRoleAsync(id, "Admin"))
            {
                ViewBag.Account = "Kontot uppdaterades";
            }

            else
            {
                ViewBag.Account = "Något gick fel var god försök igen";
            }

            return View("GetUsers", _accountService.GetAccountViewModel());
        }


        public IActionResult LogIn()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(IndexViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserLogin.Username, model.UserLogin.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Felaktig inloggning, försök igen";
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationRequest model)
        {
            if (ModelState.IsValid)
            {

                if (await _accountService.CreateAsync(model))
                {
                    ViewBag.Message = "Registreringen lyckades";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Message = "Något gick fel med registreringen";
                }
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Update()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var request = _accountService.GetUserUpdateRequest(user);

            return View(request);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (await _accountService.UpdateAsync(request, user))
                {
                    ViewBag.Message = "Ändringarna sparades!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = "Något gick fel, var god försök igen!";
                }
            }
            return View(request);
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (await _accountService.DeleteAsync(user))
            {
                await _signInManager.SignOutAsync();
                ModelState.Clear();
                ViewBag.Message = "Kontot raderat!";
                return View("../Home/Index");
            }

            ViewBag.Message = "Något gick fel!";
            return View();
        }
    }
}
