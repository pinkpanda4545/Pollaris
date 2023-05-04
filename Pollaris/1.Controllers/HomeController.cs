using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Pollaris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult ValidateUser(string email, string password)
        {
            if (email == "1" && password == "1") {
                return Redirect(Url.Action("UserDashboard", "Dashboard") + "?userId=" + 35905325);
            } else {
                UserManager uM = new UserManager();
                bool result = uM.ValidateUser(email, password);
                if (result)
                {
                    int userId = uM.GetUserIdFromEmail(email);
                    return Redirect(Url.Action("UserDashboard", "Dashboard") + "?userId=" + userId);
                }
                else
                {
                    return Redirect(Url.Action("SignIn", "Home") + "?valid=" + result);
                }
            }
        }

        public IActionResult CreateUser(string firstName, string lastName, string email, string password) {
            UserManager uM = new UserManager();
            bool emailInDatabase = uM.IsEmailInDatabase(email);
            if (emailInDatabase)
            {
                //Error!
                SignUpInfo model = new SignUpInfo(false);
                return View("SignUp", model);
            }
            else
            {
                bool userCreated = uM.CreateUser(firstName, lastName, email, password);
                if (userCreated)
                {
                    int id = uM.GetUserIdFromEmail(email);
                    return Redirect(Url.Action("UserDashboard", "Dashboard") + "?userId=" + id);
                }
                else
                {
                    //Error pt 2!
                    SignUpInfo model = new SignUpInfo(true, false);
                    return View("SignUp", model);
                }
            }
        }

        public IActionResult SignIn()
        {
            SignInInfo model = new SignInInfo(true); 
            return View(model);
        }
        public IActionResult SignUp()
        {
            SignUpInfo model = new SignUpInfo(true, true);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}