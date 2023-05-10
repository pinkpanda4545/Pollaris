using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult JoinRoomSubmit(int userId, string roomCode)
        {
            RoomManager rM = new RoomManager();
            int roomId = rM.ValidateRoomCode(roomCode); 
            if (roomId == 0) return JoinRoomInvalid(userId, false);
            bool result = rM.PutUserInRoom(userId, roomId);
            if (!result) return JoinRoomInvalid(userId, false);
            return Redirect("/Dashboard/UserDashboard?userId=" + userId); 
        }

        public IActionResult CreateRoomSubmit(int userId, string roomName)
        {
            RoomManager rM = new RoomManager();
            UserManager uM = new UserManager();
            string userName = uM.GetUserNameFromId(userId); 
            bool result = rM.CreateRoom(userId, userName, roomName);
            if (!result) return CreateRoomInvalid(userId, false);
            return Redirect("/Dashboard/UserDashboard?userId=" + userId);
        }

        public IActionResult CreateRoom(int userId)
        {
            UserId id = new UserId(userId);
            return View(id);
        }
        private IActionResult CreateRoomInvalid(int userId, bool valid)
        {
            UserId id = new UserId(userId, valid);
            return View("CreateRoom", id);
        }

        public IActionResult JoinRoom(int userId)
        {
            UserId id = new UserId(userId); 
            return View(id);
        }

        private IActionResult JoinRoomInvalid(int userId, bool valid)
        {
            UserId id = new UserId(userId, valid);
            return View("JoinRoom", id);
        }
    }
}
