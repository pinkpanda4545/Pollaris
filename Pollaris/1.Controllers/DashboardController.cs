using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;
using System.Dynamic;
using System.Text.Json;

namespace Pollaris.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("/Dashboard/UserDashboard")]
        public IActionResult UserDashboard(int userId)
        {
            RoomManager rM = new RoomManager(); 
            List<RoomInfo> rooms = rM.GetRooms(userId);

            DashboardInfo model = new DashboardInfo(userId, rooms); 
            return View(model);
        }

        [HttpGet]
        [Route("/Dashboard/RoomDashboard")]
        public IActionResult RoomDashboard(int userId, int roomId)
        {
            //Need to put RoomName and UserType in the model!!
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager(); 
            List<SetInfo> sets = sM.GetSets(roomId);
            RoomInfo room = rM.GetRoomFromId(roomId);
            room.Sets = sets;

            RoomDashboardInfo model = new RoomDashboardInfo(userId, room); 
            return View(model);
        }
    }
}
