using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers; 
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class MembersController : Controller
    {

        // Will take in information to edit profile and saves in database
        // Return true if successful, else false
        public IActionResult EditProfile(int userId)
        {
            UserId id = new UserId(userId);
            return View(id);
        }
        public IActionResult MemberList(int userId, int roomId)
        {
            //1. Using the roomId, search the SQL table for all the connections
            //2. Take all the userIds and get all user info from the SQL (2nd call)
            //  -- You'll need the photo, name, and id

            //Note: you probably don't need userId for this at all since it's the same id as the room's instructor

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
