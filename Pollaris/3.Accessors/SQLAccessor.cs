using Pollaris.Models;
using System.Data;
using System;
using Microsoft.Data.SqlClient;

namespace Pollaris._3.Accessors
{
    public class SQLAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj;";

        public SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }


        //USER MANAGER
        public bool SignInValidation(string email, string password)
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

        public bool CheckIfEmailInDatabase(string email)
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
            SqlConnection connection = getConnection();
            string query = "SELECT password FROM Users WHERE user_id = @userId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);

            string result = (string)command.ExecuteScalar();
            connection.Close();
            return result;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Users SET password = @newPassword WHERE user_id = @userId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@newPassword", newPassword);
            command.Parameters.AddWithValue("@user_id", userId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
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
            while (reader.Read())
            {
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
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "UPDATE Users SET first_name = @firstName, last_name = @lastName WHERE user_id = @userId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@first_name", firstName);
            command.Parameters.AddWithValue("@last_name", lastName);
            command.Parameters.AddWithValue("@user_id", userId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public string GetUserNameFromId(int userId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT first_name FROM Users WHERE user_id = @userId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                return reader.GetString(0);
            }
            else
            {
                connection.Close();
                return "";
            }
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
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT room_id FROM Room WHERE room_code = @roomCode;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomCode", roomCode);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int result = reader.GetInt32(0);
                connection.Close();
                return result;
            }
            else
            {
                connection.Close();
                return 0;
            }
        }

        public bool UserRoomConnection(int userId, int roomId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO UsersRoom (user_id, room_id) VALUES (@userId, @roomId)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@roomId", roomId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public bool CodeNotAvailable(string newCode)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT room_id FROM Room WHERE room_code = @newCode;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@newCode", newCode);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public bool CreateRoom(string roomName, string roomCode, int instructorId, string instructorName)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO Room (room_name, room_code, instructor_id, instructor_name) VALUES (@roomName, @roomCode, @instructorId, @instructorName)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomName", roomName);
            command.Parameters.AddWithValue("@roomCode", roomCode);
            command.Parameters.AddWithValue("@instructorId", instructorId);
            command.Parameters.AddWithValue("@instructorName", instructorName);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        //QUESTION MANAGER

        public List<int> GetQuestionIdsFromSetId(int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT question_id FROM QuestionSet WHERE set_id = @setId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            SqlDataReader reader = command.ExecuteReader();
            List<int> questionIds = new List<int>();

            while (reader.Read())
            {
                questionIds.Add(reader.GetInt32(0));
            }

            connection.Close();
            return questionIds;
        }

        public List<QuestionInfo> GetQuestionsFromIds(List<int> questionIds)
        {
            //SqlConnection connection = getConnection();
            //connection.Open();

            //string query = "SELECT * FROM Question WHERE question_id IN @questionIds;";
            //SqlCommand command = new(query, connection);
            //command.Parameters.AddWithValue("@questionIds", questionIds);

            //SqlDataReader reader = command.ExecuteReader();
            //List<RoomInfo> questions = new List<RoomInfo>();
            //while (reader.Read())
            //{
            //    int questionId = reader.GetInt32(0);
            //    string question = reader.GetString(1);
            //    string type = reader.GetString(2);
            //    // TODO
            //    string instructorName = reader.GetString(3);
            //    bool isActive;
            //    if (reader.GetInt32(4) == 1)
            //    {
            //        isActive = true;
            //    }
            //    else
            //    {
            //        isActive = false;
            //    }
            //    RoomInfo room = new RoomInfo(questionId, question, type, instructorName, isActive);
            //    questions.Add(room);
            //}

            //connection.Close();
            //return questions;
            return new List<QuestionInfo>();
        }

        public QuestionInfo CreateQuestion(string type)
        {
            //Type == "MC", "SA", "TF", or "R" (i think)
            return null;
        }

        public bool SetQuestionConnection(int setId, int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO QuestionSet (set_id, question_id) VALUES (@setId, @questionId)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@questionId", questionId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public List<int> GetOptionIdsFromQuestionId(int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT option_id FROM QuestionOption WHERE question_id = @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);
            SqlDataReader reader = command.ExecuteReader();
            List<int> optionIds = new List<int>();

            while (reader.Read())
            {
                optionIds.Add(reader.GetInt32(0));
            }

            connection.Close();
            return optionIds;
        }

        public List<OptionInfo> GetOptionsFromIds(List<int> optionsIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Option WHERE option_id IN @optionsId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@optionsIds", optionsIds);

            SqlDataReader reader = command.ExecuteReader();
            List<OptionInfo> options = new List<OptionInfo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString("name");
                int isCorrectInt = reader.GetInt32("is_correct");
                bool isCorrect;
                if (isCorrectInt == 1)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                }
                OptionInfo option = new OptionInfo(id, name, isCorrect);
                options.Add(option);
            }

            connection.Close();
            return options;
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
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Answer WHERE question_id IN @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);

            SqlDataReader reader = command.ExecuteReader();
            List<string> answers = new List<string>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string answer = reader.GetString("role");
                answers.Add(answer);
            }

            connection.Close();
            return answers;
        }

        public void SubmitStudentAnswer(int userId, int questionId, List<string> answer)
        {
            //SqlConnection connection = getConnection();
            //connection.Open();

            //string query = "INSERT INTO QuestionSet (set_id, question_id) VALUES (@setId, @questionId)";
            //SqlCommand command = new(query, connection);
            //command.Parameters.AddWithValue("@setId", setId);
            //command.Parameters.AddWithValue("@questionId", questionId);

            //int rowsAffected = command.ExecuteNonQuery();
        }


        //RESPONSES MANAGER

        public List<int> GetResponseIdsFromQuestionId(int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT response_id FROM ResponseQuestion WHERE question_id = @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);
            SqlDataReader reader = command.ExecuteReader();
            List<int> responseIds = new List<int>();

            while (reader.Read())
            {
                responseIds.Add(reader.GetInt32(0));
            }

            connection.Close();
            return responseIds;
        }

        public List<StudentResponseInfo> GetResponsesFromIds(List<int> responseIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Response WHERE response_id IN @responseIds;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@responseIds", responseIds);

            SqlDataReader reader = command.ExecuteReader();
            List<StudentResponseInfo> studentResponses = new List<StudentResponseInfo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string response = reader.GetString("response");
                StudentResponseInfo studentResponse = new StudentResponseInfo(id, response);
                studentResponses.Add(studentResponse);
            }

            connection.Close();
            return studentResponses;
        }


        //SET MANAGER

        public List<int> GetSetIdsFromRoomId(int roomId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT set_id FROM RoomSet WHERE room_id = @roomId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomId", roomId);
            SqlDataReader reader = command.ExecuteReader();
            List<int> setIds = new List<int>();

            while (reader.Read())
            {
                setIds.Add(reader.GetInt32(0));
            }

            connection.Close();
            return setIds;
        }

        public List<SetInfo> GetSetsFromIds(List<int> setIds)
        {
            List<SetInfo> sets = new List<SetInfo>();
            for (int i = 0; i < setIds.Count; i++)
            {
                sets.Add(GetSetFromId(setIds[i]));
            }
            return sets;
        }

        public SetInfo CreateSet(int roomId)
        {
            return null;
        }

        public void DeleteSet(int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Set WHERE set_id = @setId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveSetFromRoom(int roomId, int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Room WHERE set_id = @setId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
        }

        public SetInfo? GetSetFromId(int setId)
        {
            //SqlConnection connection = getConnection();
            //connection.Open();

            //string query = "SELECT * FROM Users WHERE user_id = @user_id";
            //SqlCommand command = new(query, connection);
            //command.Parameters.AddWithValue("@user_id", setId);

            //SqlDataReader reader = command.ExecuteReader();
            //SetInfo result = null;
            //if (reader.Read())
            //{
            //    int userId = reader.GetInt32(0);
            //    string? role = reader.GetString("role");
            //    string firstName = reader.GetString("first_name");
            //    string lastName = reader.GetString("last_name");
            //    string? photo = reader.GetString("photo");
            //    result = new UserInfo(userId, role, firstName, lastName, photo);
            //}
            //// TODO

            //connection.Close();
            //return result;
            return null;
        }

        public int GetRoomIdFromSetId(int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT room_id FROM RoomSet WHERE set_id = @setId";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int result = reader.GetInt32(0);
                connection.Close();
                return result;
            }
            else
            {
                // If the setId is not in RoomSet
                connection.Close();
                return 0;
            }
        }

        public int GetActiveSetIdFromRoomId(int roomId)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT active_set_id FROM Room WHERE room_id = @roomId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomId", roomId);

            int result = (int)command.ExecuteScalar();
            connection.Close();
            return result;
        }

        public void ChangeActiveSet(int activeSetId, int newSetId)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Room SET active_set_id = @newSetId WHERE active_set_id = @activeSetId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@newSetId", newSetId);
            command.Parameters.AddWithValue("@activeSetId", activeSetId);

            int result = command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeStatus(int setId, string newStatus)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Set SET status = @newStatus WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);

            int result = command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
