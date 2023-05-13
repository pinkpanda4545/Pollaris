using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;
using System.Dynamic;
using System.Text.Json;

namespace Pollaris.Controllers
{
    public class DashboardController : Controller
    {
        // UserDashboard function retrieves user-related information and displays a user dashboard view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // Returns: IActionResult representing the user dashboard view
        [HttpGet]
        [Route("/Dashboard/UserDashboard")]
        public IActionResult UserDashboard(int userId)
        {
            RoomManager rM = new RoomManager();
            UserManager uM = new UserManager();
            UserInfo user = uM.GetUserFromId(userId);
            List<RoomInfo> rooms = rM.GetRooms(userId);
            DashboardInfo model = new DashboardInfo(userId, rooms, user.ProfilePhoto); 
            return View(model);
        }

        // RoomDashboard function retrieves room-related information and displays a room dashboard view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // Returns: IActionResult representing the room dashboard view
        [HttpGet]
        [Route("/Dashboard/RoomDashboard")]
        public IActionResult RoomDashboard(int userId, int roomId)
        {
            //Need to put RoomName and UserType in the model!!
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager(); 
            List<SetInfo> sets = sM.GetSets(roomId);
            RoomInfo room = rM.GetRoomFromId(userId, roomId);
            room.Sets = sets;
            RoomDashboardInfo model = new RoomDashboardInfo(userId, room); 
            return View(model);
        }
    }
}
