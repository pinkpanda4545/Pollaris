using Pollaris._3.Accessors;
using Pollaris.Models;
using System;
using System.Data.SqlClient; 

namespace Pollaris.Managers
{
    public class UserManager
    {
        public bool ValidateUser(string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.getReaderForSignInValidation(email, password);
            return result;

        }

        public bool IsEmailInDatabase(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.getReaderToCheckIfEmailInDatabase(email);
            return result;
        }

        public bool CreateUser(string firstName, string lastName, string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            int rowsAffected = sql.signUpValidation(firstName, lastName, email, password);
            if (rowsAffected > 0) return true; 
            return false;
        }

        public int GetUserIdFromEmail(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            int result = sql.getUserIdFromEmail(email);
            return result;
        }

        public List<UserInfo> GetUsersInRoom(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetMembersFromRoomId(roomId); 
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            return result;
        }
         
        public UserInfo? GetUserFromId(int memberId)
        {
            SQLAccessor sql = new SQLAccessor(); 
            UserInfo? result = sql.GetUserFromId(memberId);
            return result;
        }

        public bool ValidateStudentUser(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo? room = sql.GetRoomFromId(roomId);
            if (room == null) return false;
            if (room.InstructorId == userId) return false;

            List<int> ids = sql.GetMembersFromRoomId(roomId); 
            foreach (var id in ids)
            {
                if (id == userId) return true; 
            }
            return false; 
        }
    }
}