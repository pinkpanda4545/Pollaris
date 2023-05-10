using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class RoomManager
    {
        private static string codeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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

        public string GetRoomNameFromId(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo? room = sql.GetRoomFromId(roomId); 
            if ( room != null )
            {
                return room.Name; 
            } else
            {
                return "";
            }
        }
        public int ValidateRoomCode(string roomCode)
        {
            SQLAccessor sql = new SQLAccessor();
             return sql.ValidateRoomCode(roomCode);
        }

        public bool PutUserInRoom(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetMembersFromRoomId(roomId);
            if (ids.Contains(userId)) return false;
            return sql.UserRoomConnection(userId, roomId, "S"); 
        }

        public bool CreateRoom(int userId, string userName, string roomName) 
        {
            SQLAccessor sql = new SQLAccessor();
            string newCode = RandomRoomCode();
            while (sql.CodeNotAvailable(newCode))
            {
                newCode = RandomRoomCode();
            }
            int roomId = sql.CreateRoom(roomName, newCode, userId, userName);
            if (roomId != 0)
            {
                return sql.UserRoomConnection(userId, roomId, "I");
            }
            return false; 
        }

        public string RandomRoomCode()
        {
            Random rand = new Random();
            string code = "";
            for (int i = 0; i < 3; i++)
            {
                code += codeCharacters[rand.Next(0, 36)];
            }
            code += "-";
            for (int i = 0; i < 3; i++)
            {
                code += codeCharacters[rand.Next(0, 36)];
            }
            return code; 
        }

        public string GetRole(int userId, int roomId)
        {
            return "TA"; 
        }
    }
}
