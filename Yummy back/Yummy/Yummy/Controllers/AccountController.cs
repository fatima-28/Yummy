using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yummy.Models;
using Yummy.ViewModels.AccountVM;

namespace Yummy.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager { get;  }
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);

            }
            AppUser newUser = new AppUser
            {
                FullName = user.FullName,
                UserName="user01",
                Email = user.Email
            };
           
            var identityresult = await _userManager.CreateAsync(newUser,user.Password);
            if (!identityresult.Succeeded)
            {
                foreach (var error in identityresult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }
            return View();
        }
    }
}
