using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication4mvcAD.Models;

namespace WebApplication4mvcAD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            var dat = User.Identity;
            ViewBag.UserName = User.Identity.Name;
            GetAutoUserName();
            return View();
        }


        public List<object> GetAutoUserName()
        {
            List<UserPrincipal> pri = new List<UserPrincipal>();
            List<object> users = new List<object>();

            using (PrincipalContext principle_context = new PrincipalContext(ContextType.Domain))
            {
                GroupPrincipal SearchGroup = GroupPrincipal.FindByIdentity(principle_context, "@Home");

                pri = SearchGroup.GetMembers(true).Cast<UserPrincipal>().ToList();

                foreach (UserPrincipal item in pri)
                {
                    var data = item;
                }

            

            }
           



            return users;

        }




        [HttpPost]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties()
            { RedirectUri = "/Home/SignOutSuccess" },
        AzureADDefaults.AuthenticationScheme,
        AzureADDefaults.CookieScheme,
        AzureADDefaults.OpenIdScheme);
        }

        public IActionResult SignOutSuccess()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
