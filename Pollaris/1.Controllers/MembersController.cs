using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers; 
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class MembersController : Controller
    {
        [HttpPost]
        [Route("/Members/SaveProfileInformation")]
        public IActionResult SaveProfileInformation(int userId, string firstName, string lastName)
        {
            //Save the first and last name and photo (if changed) to the database. 
            UserManager uM = new UserManager();
            bool result = uM.SaveProfileInfo(userId,firstName, lastName); 
            return new JsonResult(result); 
        }

        [HttpPost]
        [Route("/Members/ValidateAndSavePassword")]
        public IActionResult ValidateAndSavePassword(int userId, string oldPassword, string newPassword, string newPassword2)
        {
            //Validate that the password matches the user id
            //Validate that the newPassword and newPassword2 are the same
            //Save it to the database
            UserManager uM = new UserManager();
            bool result = uM.ChangePassword(userId, oldPassword, newPassword, newPassword2);
            return new JsonResult(true);
        }

        public IActionResult EditProfile(int userId)
        {
            UserId id = new UserId(userId);
            return View(id);
        }
        public IActionResult MemberList(int userId, int roomId)
        {
            UserManager uM = new UserManager();
            List<UserInfo> userList = uM.GetUsersInRoom(roomId);
            MemberListInfo model = new MemberListInfo(userId, roomId, userList);
            return View(model);
        }

        public IActionResult MemberPermissions(int userId, int roomId, int memberId)
        {
            UserManager uM = new UserManager();
            UserInfo user = uM.GetUserFromId(memberId);
            MemberPermissionsInfo model = new MemberPermissionsInfo(userId, roomId, user);
            return View(model);
        }
    }
}
