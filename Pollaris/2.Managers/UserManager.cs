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
            bool result = sql.SignInValidation(email, password);
            return result;
        }

        public bool IsEmailInDatabase(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.CheckIfEmailInDatabase(email);
            return result;
        }

        public bool CreateUser(string firstName, string lastName, string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            int rowsAffected = sql.SignUpValidation(firstName, lastName, email, password);
            if (rowsAffected > 0) return true; 
            return false;
        }

        public int GetUserIdFromEmail(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            int result = sql.GetUserIdFromEmail(email);
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

        public void ChangeProfilePhoto(int userId, string src)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeProfilePhoto(userId, src);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword, string newPassword2)
        {
            SQLAccessor sql = new SQLAccessor();
            if (newPassword != newPassword2) return false; 
            if (newPassword == oldPassword) return false;
            string? currPassword = sql.GetPasswordFromUserId(userId);
            if (currPassword == null) return false; 
            if (currPassword != oldPassword) return false;
            bool result = sql.ChangePassword(userId, newPassword);
            return result;
        }

        public bool SaveProfileInfo(int userId, string firstName, string lastName)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.SaveProfileInfo(userId, firstName, lastName);
            List<int> ids = sql.GetRoomIdsFromUserId(userId); 
            sql.UpdateRoomInstructorName(ids, firstName + " " + lastName); 
            return result;
        }

        public string GetUserNameFromId(int userId) 
        {
            SQLAccessor sql = new SQLAccessor();
            return sql.GetUserNameFromId(userId); 
        }
    }
}