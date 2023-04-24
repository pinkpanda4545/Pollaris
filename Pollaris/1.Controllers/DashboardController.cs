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
            DashboardInfo dbModel = new DashboardInfo(userId, rooms); 
            return View(dbModel);
        }

        [HttpGet]
        [Route("/Dashboard/RoomDashboard")]
        public IActionResult RoomDashboard(int userId, int roomId, string roomName)
        {
            //ROOM NAME SHOULD NOT BE A PARAMETER. WHEN THE DB IS SETUP, IT SHOULD COME FROM THAT
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager(); 
            List<SetInfo> sets = sM.GetSets(roomId);
            RoomInfo room = rM.GetRoomFromId(roomId, roomName);
            room.Sets = sets;
            RoomDashboardInfo roomDashboardModel = new RoomDashboardInfo(userId, room); 

            return View(roomDashboardModel);
        }
    }
}
