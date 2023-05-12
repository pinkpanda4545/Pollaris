using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class RoomManager
    {
        private static string codeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        // Retrieve the rooms associated with a given user, along with the user's role in each room.
        // Inputs:
        //  - userId: int representing the ID of the user whose rooms are to be retrieved.
        // Returns: List<RoomInfo>: a list of RoomInfo objects representing the rooms associated with the given user, along with their user role in each room.
        public List<RoomInfo> GetRooms(int userId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> roomIds = sql.GetRoomIdsFromUserId(userId);
            List<RoomInfo> rooms = sql.GetRoomsFromIds(roomIds);
            //ADD USERTYPE
            for (int i = 0; i < rooms.Count; i++)
            {
                string userType = sql.GetRole(userId, rooms[i].Id);
                rooms[i].UserType = userType;
            }
            List<RoomInfo> result = SortRoomsByUserType(rooms);
            return rooms;
        }

        // Sort a list of RoomInfo objects by user type.
        // Inputs:
        //  - rooms: List of RoomInfo objects to be sorted
        // Returns: List of RoomInfo objects sorted by user type
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
            foreach (RoomInfo room in result)
            {
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

        // Retrieve a RoomInfo object associated with the specified roomId and userId.
        // Inputs:
        //  - userId: int representing the ID of the user associated with the room
        //  - roomId: int representing the ID of the room to retrieve
        // Returns: RoomInfo object associated with the specified roomId, or null if no such room exists
        public RoomInfo? GetRoomFromId(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo? room = sql.GetRoomFromId(roomId);
            if (room != null)
            {
                string userType = sql.GetRole(userId, roomId);
            }
            return room;
        }

        // Retrieve the name of the room associated with the specified roomId.
        // Inputs:
        //  - roomId: int representing the ID of the room to retrieve the name of
        // Returns: string representing the name of the room associated with the specified roomId,
        //  or an empty string if no such room exists
        public string GetRoomNameFromId(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo? room = sql.GetRoomFromId(roomId);
            if (room != null)
            {
                return room.Name;
            }
            else
            {
                return "";
            }
        }

        // Validate a room code to ensure it is unique.
        // Inputs:
        //  - roomCode: string representing the room code to validate
        // Returns: int representing the number of rooms in the database with the specified room code
        public int ValidateRoomCode(string roomCode)
        {
            SQLAccessor sql = new SQLAccessor();
            return sql.ValidateRoomCode(roomCode);
        }

        // Add a user to a specified room.
        // Inputs:
        //  - userId: int representing the ID of the user to be added to the room.
        //  - roomId: int representing the ID of the room to which the user is to be added.
        // Returns: bool: true if the user was added to the room successfully; false if the user was already in the room.
        public bool PutUserInRoom(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetMembersFromRoomId(roomId);
            if (ids.Contains(userId)) return false;
            return sql.UserRoomConnection(userId, roomId, "S");
        }

        // Create a new room with the specified name, add the specified user as an admin of the room, and generate a unique room code.
        // Inputs:
        //  - userId: int representing the ID of the user who is creating the room.
        //  - userName: string representing the name of the user who is creating the room.
        //  - roomName: string representing the name of the room to be created.
        // Returns: bool: true if the room was created successfully; false otherwise.
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

        // RandomRoomCode - Generate a random room code to be used for a newly created room.
        // Returns: string: a string representing a randomly generated room code.
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

        // GetRole - Retrieve the user's role in a specified room.
        // Inputs:
        //  - userId: int representing the ID of the user whose role in the room is to be retrieved.
        //  - roomId: int representing the ID of the room whose user role is to be retrieved
        // Returns: string of the user's role for the given room
        public string GetRole(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            return sql.GetRole(userId, roomId);
        }
    }
}
