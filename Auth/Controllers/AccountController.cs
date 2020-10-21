using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Auth.Models;

namespace Auth.Areas.Identity.Account
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserMgr { get; }
        private SignInManager<AppUser> SignInMgr { get; }

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login()
        {
            var result = await SignInMgr.PasswordSignInAsync("TestUser", "Admin123!", false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "result is:" + result.ToString();
            }
            return View();
        }
        public async Task<IActionResult> Register()
        {
            try
            {
                ViewBag.Message = "User already register";
                AppUser user = await UserMgr.FindByNameAsync("TestUser");
                if (user == null)
                {
                    user = new AppUser();
                    user.UserName = "admin";
                    user.Email = "admin@gmail.com";
                    user.FirstName = "Nguyen";
                    user.LastName = "Son";

                    IdentityResult result = await UserMgr.CreateAsync(user, "Admin123!");
                    ViewBag.Message = "User was created";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}

