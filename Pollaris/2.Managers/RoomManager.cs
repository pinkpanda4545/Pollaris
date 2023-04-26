using Pollaris.Models;

namespace Pollaris.Managers
{
    public class RoomManager
    {
        public List<RoomInfo> GetRooms(int userId)
        {
            //go get the classes for the user based on id. 
            //put the RoomInfo creation here
            //need to sort by Instructor, TA, then Student types. 
            List<RoomInfo> rooms = new List<RoomInfo>();
            rooms.Add(new RoomInfo("Software Engineering IV", "Firestone", 1, "Instructor"));
            rooms.Add(new RoomInfo("Leadership", "Cooper", 2, "TA"));
            rooms.Add(new RoomInfo("Finance", "Baugh", 3, "Student"));
            rooms.Add(new RoomInfo("Machine Learning", "Bockmon", 4, "Student"));
            rooms.Add(new RoomInfo("Campus Band", "Bush", 5, "Student"));
            rooms.Add(new RoomInfo("Calculus AB", "Tomlinson", 6, "Student"));
            rooms.Add(new RoomInfo("Calculus BC", "Tomlinson", 7, "Student"));

            return rooms;
        }
        public RoomInfo GetRoomFromId(int roomId)
        {
            return new RoomInfo("Room Name Here", "ABC", roomId, "Student");
        }
    }
}
