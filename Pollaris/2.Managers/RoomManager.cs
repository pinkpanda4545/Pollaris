using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class RoomManager
    {
        public List<RoomInfo> GetRooms(int userId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> roomIds = sql.GetRoomIdsFromUserId(userId);
            List<RoomInfo> rooms = sql.GetRoomsFromIds(roomIds); 
            //ADD USERTYPE
            List<RoomInfo> result = SortRoomsByUserType(rooms);
            return rooms;
        }

        public List<RoomInfo> SortRoomsByUserType(List<RoomInfo> rooms)
        {
            List<RoomInfo> result = new List<RoomInfo>();
            foreach (RoomInfo room in rooms)
            {
                if (room.UserType == "I")
                {
                    result.Add(room);
                }
            }
            foreach (RoomInfo room in result) { 
                if (room.UserType == "TA")
                {
                    result.Add(room);
                }
            }
            foreach (RoomInfo room in result)
            {
                if (room.UserType == "S")
                {
                    result.Add(room);
                }
            }
            return result; 
        }

        public RoomInfo? GetRoomFromId(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo? room = sql.GetRoomFromId(roomId); 
            if (room != null)
            {
                //ADD USERTYPE
            }
            return room; 
        }
    }
}
