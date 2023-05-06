using Pollaris.Models;
using System.Data;
using System;
using Microsoft.Data.SqlClient;

namespace Pollaris._3.Accessors
{
    public class SQLAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj; Column Encryption Setting=enabled";
        
        public SqlConnection getConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
            return new SqlConnection(builder.ConnectionString);
        }


        //USER MANAGER
        public bool GetReaderForSignInValidation(string email, string password)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT user_id, password FROM Users WHERE email = @email AND password = @password";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            // Execute          
            bool result = command.ExecuteReader().HasRows;
            connection.Close();
            return result;
        }

        public bool GetReaderToCheckIfEmailInDatabase(string email)
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "SELECT email FROM Users WHERE email = @email";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@email", email);
            // Execute
            bool result = command.ExecuteReader().HasRows;
            connection.Close();
            return result;
        }

        public int SignUpValidation(string firstName, string lastName, string email, string password)
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "INSERT INTO Users (first_name, last_name, email, password) VALUES (@firstName, @lastName, @email, @password);";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            // Execute
            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public int GetUserIdFromEmail(string email)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT user_id FROM Users WHERE email = @email;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@email", email);

            int result = (int)command.ExecuteScalar();
            connection.Close();
            return result;
        }

        public string? GetPasswordFromUserId(int userId)
        {
            return null; 
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            return false; 
        }

        public List<int> GetMembersFromRoomId(int roomId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT user_id FROM UsersRoom WHERE room_id = @room_id;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@room_id", roomId);

            SqlDataReader reader = command.ExecuteReader();
            List<int> result = new List<int>();
            while (reader.Read()) {
                int id = reader.GetInt32(0);
                result.Add(id);
            }
            connection.Close();
            return result;
        }

        public List<UserInfo> GetUsersFromIds(List<int> ids)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Users WHERE user_id IN @user_ids;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@user_ids", ids);

            SqlDataReader reader = command.ExecuteReader();
            List<UserInfo> result = new List<UserInfo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string role = reader.GetString("role");
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                string photo = reader.GetString("photo"); 
                UserInfo user = new UserInfo(id, role, firstName, lastName, photo);
                result.Add(user);
            }

            connection.Close();
            return result;
        }

        public UserInfo? GetUserFromId(int id)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Users WHERE user_id = @user_id";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@user_id", id);

            SqlDataReader reader = command.ExecuteReader();
            UserInfo result = null;
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string? role = reader.GetString("role");
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                string? photo = reader.GetString("photo");
                result = new UserInfo(userId, role, firstName, lastName, photo);
            }

            connection.Close();
            return result;
        }

        public bool SaveProfileInfo(int userId, string firstName, string lastName)
        {
            return false; 
        }

        public string GetUserNameFromId(int userId)
        {
            return "";
        }


        //ROOM MANAGER (mostly)
        public RoomInfo? GetRoomFromId(int id)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Room WHERE room_id = @room_id";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@room_id", id);

            SqlDataReader reader = command.ExecuteReader();
            RoomInfo? result = null;
            while (reader.Read())
            {
                int roomId = reader.GetInt32(0);
                string name = reader.GetString(1);
                int instructorId = reader.GetInt32(2);
                string instructorName = reader.GetString(3);
                string code = reader.GetString(4);
                result = new RoomInfo(roomId, name, instructorId, instructorName, code);
            }

            connection.Close();
            return result;
        }

        public List<int> GetRoomIdsFromUserId(int userId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT room_id FROM UsersRoom WHERE user_id = @user_id;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@user_id", userId);

            SqlDataReader reader = command.ExecuteReader();
            List<int> result = new List<int>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                result.Add(id);
            }
            connection.Close();
            return result;
        }

        public List<RoomInfo> GetRoomsFromIds(List<int> ids)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Room WHERE room_id IN @room_ids;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@room_ids", ids);

            SqlDataReader reader = command.ExecuteReader();
            List<RoomInfo> result = new List<RoomInfo>();
            while (reader.Read())
            {
                int roomId = reader.GetInt32(0);
                string name = reader.GetString(1);
                int instructorId = reader.GetInt32(2);
                string instructorName = reader.GetString(3);
                string code = reader.GetString(4);
                RoomInfo room = new RoomInfo(roomId, name, instructorId, instructorName, code);
                result.Add(room);
            }

            connection.Close();
            return result;
        }

        public int ValidateRoomCode(string roomCode)
        {
            //return 0 if reader.HasRows == false
            //return roomId otherwise
            return 0;
        }

        public bool UserRoomConnection(int userId, int roomId)
        {
            return false; 
        }

        public bool CodeNotAvailable(string newCode)
        {
            //check Rooms table for the code
            //return true if the code is in the table
            //return false if the code is NOT in the table
            return false; 
        }

        public bool CreateRoom(string roomName, string roomCode, int instructorId, string instructorName)
        {

        }

        //QUESTION MANAGER

        public List<int> GetQuestionIdsFromSetId(int setId)
        {
            return new List<int>();
        }

        public List<QuestionInfo> GetQuestionsFromIds(List<int> questionIds)
        {
            return new List<QuestionInfo>(); 
        }

        public QuestionInfo CreateQuestion(string type)
        {
            //Type == "MC", "SA", "TF", or "R" (i think)
        }

        public void SetQuestionConnection(int setId, int questionId)
        {

        }

        public List<int> GetOptionIdsFromQuestionId(int questionId)
        {
            return new List<int>();
        }

        public List<OptionInfo> GetOptionsFromIds(List<int> optionsIds)
        {
            return new List<OptionInfo>();
        }

        public bool ChangeActiveQuestion(int currActiveId, int nextActiveId)
        {
            //deactivate currActive, active nextActive
            return true;
        }

        public QuestionInfo? GetQuestionFromId(int questionId)
        {
            return null; 
        }

        public List<String> GetAnswersFromQuestionId(int questionId)
        {
            return new List<String>();
        }

        public void SubmitStudentAnswer(int userId, int questionId, List<string> answer)
        {

        }


        //RESPONSES MANAGER

        public List<int> GetResponseIdsFromQuestionId(int questionId)
        {
            return new List<int>(); 
        }

        public List<StudentResponseInfo> GetResponsesFromIds(List<int> responseIds)
        {
            return new List<StudentResponseInfo>();
        }


        //SET MANAGER

        public List<int> GetSetIdsFromRoomId(int roomId)
        {
            return new List<int>(); 
        }

        public List<SetInfo> GetSetsFromIds(List<int> setIds)
        {
            return new List<SetInfo>();
        }

        public SetInfo CreateSet(int roomId)
        {
            return null;
        }

        public void DeleteSet(int setId)
        {

        }

        public void RemoveSetFromRoom(int roomId, int setId)
        {

        }

        public SetInfo? GetSetFromId(int setId)
        {
            return null;
        }

        public int GetRoomIdFromSetId(int setId)
        {
            //use Room Sets connection table
            return 0; 
        }

        public int GetActiveSetIdFromRoomId(int roomId)
        {
            //find active set
            return 0; 
        }

        public void ChangeActiveSet(int activeSetId, int newSetId)
        {

        }

        public void ChangeStatus(int setId, string newStatus)
        {
            //newStatus == "C" or "R"
        }
    }
}
