﻿using Microsoft.AspNetCore.Mvc;
using SuperShop.Helpers;
using SuperShop.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded) //se o login for bem sucedido
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl")) //se houver ReturnUrl no Request
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First()); //ele envia para o primeiro return
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
