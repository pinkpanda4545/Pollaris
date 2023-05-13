using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers; 
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class MembersController : Controller
    {
        // SaveProfileInformation function saves the updated profile information for a user.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - firstName: a string representing the user's updated first name
        // - lastName: a string representing the user's updated last name
        // Returns: IActionResult representing a JSON result indicating the success or failure of saving the profile information
        [HttpPost]
        [Route("/Members/SaveProfileInformation")]
        public IActionResult SaveProfileInformation(int userId, string firstName, string lastName)
        {
            UserManager uM = new UserManager();
            bool result = uM.SaveProfileInfo(userId,firstName, lastName); 
            return new JsonResult(result); 
        }

        // ValidateAndSavePassword function validates the user's old password, changes it to the new password if valid, and saves it.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - oldPassword: a string representing the user's old password
        // - newPassword: a string representing the user's new password
        // - newPassword2: a string representing the confirmation of the new password
        // Returns: IActionResult representing a JSON result indicating the success or failure of the password change
        [HttpPost]
        [Route("/Members/ValidateAndSavePassword")]
        public IActionResult ValidateAndSavePassword(int userId, string oldPassword, string newPassword, string newPassword2)
        {
            UserManager uM = new UserManager();
            bool result = uM.ChangePassword(userId, oldPassword, newPassword, newPassword2);
            return new JsonResult(userId);
        }

        // ChangeProfilePhoto function changes the profile photo for a user.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - src: a string representing the source of the new profile photo
        // Returns: void
        public void ChangeProfilePhoto(int userId, string src)
        {
            UserManager uM = new UserManager();
            uM.ChangeProfilePhoto(userId, src);
        }

        // EditProfile function displays the edit profile view for a user.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // Returns: IActionResult representing the edit profile view
        [HttpGet]
        [Route("/Members/EditProfile")]
        public IActionResult EditProfile(int userId)
        {
            UserId id = new UserId(userId);
            UserManager uM = new UserManager();
            UserInfo? user = uM.GetUserFromId(userId);
            if (user.ProfilePhoto != null && user.ProfilePhoto != "")
            {
                id.Photo = user.ProfilePhoto;
            }
            return View(id);
        }

        // MemberList function retrieves a list of members in a room and displays the member list view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // Returns: IActionResult representing the member list view
        public IActionResult MemberList(int userId, int roomId)
        {
            UserManager uM = new UserManager();
            List<UserInfo> userList = uM.GetUsersInRoom(roomId);
            MemberListInfo model = new MemberListInfo(userId, roomId, userList);
            return View(model);
        }

        // MemberPermissions function retrieves the permissions of a member in a room and displays the member permissions view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - memberId: an integer representing the ID of the member
        // Returns: IActionResult representing the member permissions view
        public IActionResult MemberPermissions(int userId, int roomId, int memberId)
        {
            UserManager uM = new UserManager();
            RoomManager rM = new RoomManager();
            UserInfo user = uM.GetUserFromId(memberId);
            string role = rM.GetRole(user.Id, roomId); 
            MemberPermissionsInfo model = new MemberPermissionsInfo(userId, roomId, user, role);
            return View(model);
        }
    }
}
