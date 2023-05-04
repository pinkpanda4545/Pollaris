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

        public bool getReaderForSignInValidation(string email, string password)
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

        public bool getReaderToCheckIfEmailInDatabase(string email)
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

        public int signUpValidation(string firstName, string lastName, string email, string password)
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

        public int getUserIdFromEmail(string email)
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

        public List<String> GetAnswers(int roomId, int setId, int questionId)
        {
            switch (questionId)
            {
                case 1:
                    return new List<String> {"good"};
                case 2:
                    return new List<String> {"B"};
                case 3:
                    return new List<String> {"False"};
                case 4:
                    return new List<String> { "A", "B", "C", "D" };
            }
            return new List<String> { "A", "B", "C", "D" };
        }

        public void SubmitStudentAnswer(int userId, int roomId, int setId, int questionId, bool correct)
        {

        }
    }
}
