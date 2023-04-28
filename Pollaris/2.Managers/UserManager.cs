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
            SqlConnection connection = sql.getConnection();
            SqlDataReader reader = sql.getReaderForSignInValidation(connection, email, password);
            bool result = reader.HasRows;
            connection.Close(); 
            return result;

        }

        public bool IsEmailInDatabase(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            SqlConnection connection = sql.getConnection();
            SqlDataReader reader = sql.getReaderToCheckIfEmailInDatabase(connection, email);
            bool result = reader.HasRows;
            connection.Close();
            return result;
        }

        public bool CreateUser(string firstName, string lastName, string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            SqlConnection connection = sql.getConnection();
            int rowsAffected = sql.signUpValidation(connection, firstName, lastName, email, password);
            connection.Close();
            if (rowsAffected > 0) return true; 
            return false;
        }

        public int GetUserIdFromEmail(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            SqlConnection connection = sql.getConnection();
            int result = sql.getUserIdFromEmail(connection, email);
            connection.Close();
            return result;
        }

        public List<UserInfo> GetUsersInRoom(int roomId)
        {
            //Move this to a Manager. Have access to the room's name, instructor, id. 
            List<UserInfo> result = new List<UserInfo>();
            result.Add(new UserInfo(1, "TA", "Vaughn", "Thompson", "/images/profile-photo.jpg"));
            result.Add(new UserInfo(2, "M", "Emily", "Nau", "/images/profile-photo.jpg"));
            result.Add(new UserInfo(3, "I", "Tan", "Phan", "/images/profile-photo.jpg"));
            result.Add(new UserInfo(4, "M", "Ellenna", "D", "/images/profile-photo.jpg"));
            result.Add(new UserInfo(5, "M", "Justin", "Firestone", "/images/profile-photo.jpg"));
            return result;
        }
         
        public UserInfo GetUserFromId(int memberId)
        {
            return new UserInfo(6, "M", "Ryan", "Bockmon", "/images/profile-photo.jpg");
        }

        public bool ValidateStudentUser(int userId, int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            RoomInfo room = sql.GetRoomFromId(roomId);

            if (room.InstructorId == userId) return false;

            List<UserInfo> users = GetUsersInRoom(roomId); 
            foreach (var member in users)
            {
                if (member.Id == userId) return true; 
            }
            return false; 
        }
    }
}