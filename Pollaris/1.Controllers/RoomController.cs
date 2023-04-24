using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult JoinRoomSubmit(int userId, string roomCode)

        {
            //go make a connection between user and room in the sql
            //return true if connection successfully made. 
            return Redirect(Url.Action("UserDashboard", "Dashboard") + "?userId=" + userId); 
        }

        public IActionResult CreateRoomSubmit(int userId, string roomName, string roomCode)

        {
            //go make the room in the sql
            //make the userId the instructor

            return Redirect(Url.Action("UserDashboard", "Dashboard") + "?userId=" + userId);
        }

        public IActionResult CreateRoom(int userId)
        {
            UserId id = new UserId(userId); 
            return View(id);
        }

        public IActionResult JoinRoom(int userId)
        {
            UserId id = new UserId(userId); 
            return View(id);
        }
    }
}
