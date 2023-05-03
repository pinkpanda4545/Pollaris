using Pollaris.Models;
using System.Data.SqlClient;

namespace Pollaris._3.Accessors
{
    public class SQLAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj";

        public SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public SqlDataReader getReaderForSignInValidation(SqlConnection connection, string email, string password)
        {
            string query = "SELECT user_id, password FROM Users WHERE email = @email AND password = @password";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            // Execute          
            return command.ExecuteReader();
        }

        public SqlDataReader getReaderToCheckIfEmailInDatabase(SqlConnection connection, string email)
        {
            // Create Query
            string query = "SELECT email FROM Users WHERE email = @email";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@email", email);
            // Execute
            return command.ExecuteReader();
        }

        public int signUpValidation(SqlConnection connection, string firstName, string lastName, string email, string password)
        {
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
            return command.ExecuteNonQuery();
        }

        public int getUserIdFromEmail(SqlConnection connection, string email)
        {
            string query = "SELECT user_id FROM Users WHERE email = @email;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@email", email);

            return (int)command.ExecuteScalar();
        }

        public RoomInfo GetRoomFromId(int id)
        {
            return new RoomInfo(id, "room name here", 1001, "ABC-123");
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

        public void SubmitStudentAnswer(int userId, int roomId, int setId, int questionId)
        {

        }
    }
}
