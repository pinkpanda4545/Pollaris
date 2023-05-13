using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class RoomController : Controller
    {
        // JoinRoomSubmit function attempts to join a room using a room code.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomCode: a string representing the room code to join
        // Returns: IActionResult representing the user dashboard view if successful, or an invalid join room view if unsuccessful
        public IActionResult JoinRoomSubmit(int userId, string roomCode)
        {
            RoomManager rM = new RoomManager();
            int roomId = rM.ValidateRoomCode(roomCode); 
            if (roomId == 0) return JoinRoomInvalid(userId, false);
            bool result = rM.PutUserInRoom(userId, roomId);
            if (!result) return JoinRoomInvalid(userId, false);
            return Redirect("/Dashboard/UserDashboard?userId=" + userId); 
        }

        // CreateRoomSubmit function creates a new room.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomName: a string representing the name of the room to create
        // Returns: IActionResult representing the user dashboard view if successful, or an invalid create room view if unsuccessful
        public IActionResult CreateRoomSubmit(int userId, string roomName)
        {
            RoomManager rM = new RoomManager();
            UserManager uM = new UserManager();
            string userName = uM.GetUserNameFromId(userId); 
            bool result = rM.CreateRoom(userId, userName, roomName);
            if (!result) return CreateRoomInvalid(userId, false);
            return Redirect("/Dashboard/UserDashboard?userId=" + userId);
        }

        // CreateRoom function displays the create room view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // Returns: IActionResult representing the create room view
        public IActionResult CreateRoom(int userId)
        {
            UserId id = new UserId(userId);
            return View(id);
        }

        // CreateRoomInvalid function displays the create room view with an invalid message.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - valid: a boolean indicating whether the create room request was valid or not
        // Returns: IActionResult representing the create room view with an invalid message
        private IActionResult CreateRoomInvalid(int userId, bool valid)
        {
            UserId id = new UserId(userId, valid);
            return View("CreateRoom", id);
        }

        // JoinRoom function displays the join room view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // Returns: IActionResult representing the join room view
        public IActionResult JoinRoom(int userId)
        {
            UserId id = new UserId(userId); 
            return View(id);
        }

        // JoinRoomInvalid function displays the join room view with an invalid message.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - valid: a boolean indicating whether the join room request was valid or not
        // Returns: IActionResult representing the join room view with an invalid message
        private IActionResult JoinRoomInvalid(int userId, bool valid)
        {
            UserId id = new UserId(userId, valid);
            return View("JoinRoom", id);
        }
    }
}
