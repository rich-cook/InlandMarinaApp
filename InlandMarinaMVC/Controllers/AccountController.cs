using InlandMarinaData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InlandMarinaMVC.Controllers
{
    public class AccountController : Controller
    {

        //Route: /Account/Login
        public IActionResult Login(string returnUrl = "")
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            
            return View();//collect login name, password and post it
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer customer) // from the Login form
        {
            Customer usr = CustomerManager.Authenticate(customer.Username, customer.Password);
            if (usr == null) // authentication failed
            {
                return View(); // stay on the login page
            }
           
      
            //if usr is an owner, get owner id and store in Session object
            if (usr != null)
          
            HttpContext.Session.SetInt32("CurrentCustomer", (int)usr.ID);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usr.FirstName),
                //new Claim(ClaimTypes.Username, usr.Username),
                new Claim("FirstName", usr.FirstName),
                new Claim("LastName", usr.LastName)

                //

            };

            //}
            // user != null -- authentication passed
            

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies"); // cookies authetication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            if (String.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
                return RedirectToAction("List", "Lease");
            else
                return Redirect(TempData["ReturnUrl"].ToString());
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
