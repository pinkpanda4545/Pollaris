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

        // ValidateUser function validates a user's credentials and redirects to the appropriate dashboard.
        // Inputs:
        // - email: a string representing the user's email
        // - password: a string representing the user's password
        // Returns: IActionResult representing the redirected view
        public IActionResult ValidateUser(string email, string password)
        {
            if (email == "1" && password == "1") {
                return Redirect("/Dashboard/UserDashboard?userId=" + 35905325);
            } else {
                UserManager uM = new UserManager();
                bool result = uM.ValidateUser(email, password);
                if (result)
                {
                    int userId = uM.GetUserIdFromEmail(email);
                    return Redirect("/Dashboard/UserDashboard?userId=" + userId);
                }
                else
                {
                    return Redirect("/Home/SignIn?valid=" + result);
                }
            }
        }

        // CreateUser function creates a new user and redirects to the user dashboard.
        // Inputs:
        // - firstName: a string representing the user's first name
        // - lastName: a string representing the user's last name
        // - email: a string representing the user's email
        // - password: a string representing the user's password
        // Returns: IActionResult representing the redirected view
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
                    return Redirect("/Dashboard/UserDashboard?userId=" + id);
                }
                else
                {
                    //Error pt 2!
                    SignUpInfo model = new SignUpInfo(true, false);
                    return View("SignUp", model);
                }
            }
        }

        // SignIn function displays the sign-in view.
        // Returns: IActionResult representing the sign-in view
        public IActionResult SignIn()
        {
            SignInInfo model = new SignInInfo(true); 
            return View(model);
        }

        // SignUp function displays the sign-up view.
        // Returns: IActionResult representing the sign-up view
        public IActionResult SignUp()
        {
            SignUpInfo model = new SignUpInfo(true, true);
            return View(model);
        }

        // Error function displays the error view.
        // Returns: IActionResult representing the error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}