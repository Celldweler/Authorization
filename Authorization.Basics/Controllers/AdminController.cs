using Authorization.Basics.Data;
using Authorization.Basics.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Basics.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Manager()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _userManager.FindByNameAsync(loginVm.UserName);
            //var user = await _ctx.Users
            //    .SingleOrDefaultAsync(x => x.UserName == loginVm.UserName && x.Password == loginVm.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(loginVm);
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, loginVm.UserName),
            //};
            //var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect(loginVm.ReturnUrl);
        }

        public async Task<IActionResult> LogOffAsync(string returnUrl)
        {
            await HttpContext.SignOutAsync("Cookie");

            return Redirect("/Home/Index");
        }
    }
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
