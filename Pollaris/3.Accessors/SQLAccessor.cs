using Pollaris.Models;
using System.Data;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Routing;

namespace Pollaris._3.Accessors
{
    public class SQLAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj;";

        public SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public string ListToSqlString(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return "('0')"; 
            }
            string result = "("; 
            for (int i = 0; i < ids.Count; i++)
            {
                if (i < ids.Count - 1)
                {
                    result += "'" + ids[i].ToString() + "',";
                } else
                {
                    result += "'" + ids[i].ToString() + "')";
                }
            }
            return result; 
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
            command.Parameters.AddWithValue("@userId", userId);

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

            string query = "SELECT * FROM Users WHERE user_id IN " + this.ListToSqlString(ids) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<UserInfo> result = new List<UserInfo>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                object photoValue = reader.GetValue("photo");
                string photo = ""; 
                if (photoValue != null && photoValue != DBNull.Value)
                {
                    photo = (string)photoValue;
                }
                UserInfo user = new UserInfo(id, firstName, lastName, photo);
                result.Add(user);
            }

            connection.Close();
            return result;
        }

        public UserInfo? GetUserFromId(int id)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Users WHERE user_id = @userId";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", id);

            SqlDataReader reader = command.ExecuteReader();
            UserInfo? result = null;
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                object photoValue = reader.GetValue("photo");
                string photo = "";
                if (photoValue != null && photoValue != DBNull.Value)
                {
                    photo = (string)photoValue;
                }
                result = new UserInfo(userId, firstName, lastName, photo);
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
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@userId", userId);

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

            string query = "SELECT first_name, last_name FROM Users WHERE user_id = @userId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string firstName = reader.GetString("first_name");
                string lastName = reader.GetString("last_name");
                string userName = String.Concat(firstName, " ", lastName);
                connection.Close();
                return userName;
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
                string name = reader.GetString("room_name");
                int instructorId = reader.GetInt32("instructor_id");
                string instructorName = reader.GetString("instructor_name");
                string code = reader.GetString("room_code");
                result = new RoomInfo(roomId, name, instructorId, instructorName, code);
            }

            connection.Close();
            return result;
        }

        public List<int> GetRoomIdsFromUserId(int userId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT room_id FROM UsersRoom WHERE user_id = @userId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);

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

            string query = "SELECT * FROM Room WHERE room_id IN " + this.ListToSqlString(ids) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<RoomInfo> result = new List<RoomInfo>();
            while (reader.Read())
            {
                int roomId = reader.GetInt32(0);
                string name = reader.GetString("room_name");
                int instructorId = reader.GetInt32("instructor_id");
                string instructorName = reader.GetString("instructor_name");
                string code = reader.GetString("room_code");
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

        public bool UserRoomConnection(int userId, int roomId, string role)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO UsersRoom (user_id, room_id, role) VALUES (@userId, @roomId, @role)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@roomId", roomId);
            command.Parameters.AddWithValue("@role", role);

            try
            {
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
            } catch (SqlException ex)
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

            if (reader.HasRows)
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

        public int CreateRoom(string roomName, string roomCode, int instructorId, string instructorName)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO Room (room_name, room_code, instructor_id, instructor_name) VALUES (@roomName, @roomCode, @instructorId, @instructorName) SELECT SCOPE_IDENTITY();";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomName", roomName);
            command.Parameters.AddWithValue("@roomCode", roomCode);
            command.Parameters.AddWithValue("@instructorId", instructorId);
            command.Parameters.AddWithValue("@instructorName", instructorName);

            try
            {
                int roomId = (int)command.ExecuteScalar();
                if (roomId != 0)
                {
                    connection.Close();
                    return roomId;
                }
                else
                {
                    connection.Close();
                    return 0;
                }
            } catch (SqlException e)
            {
                connection.Close();
                return 0; 
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
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Question WHERE question_id IN " + this.ListToSqlString(questionIds) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<QuestionInfo> questions = new List<QuestionInfo>();
            while (reader.Read())
            {
                int questionId = reader.GetInt32(0);
                string question = reader.GetString("question");
                string type = reader.GetString("question_type");
                bool isGraded = reader.GetBoolean("is_graded");
                bool isAnonymous = reader.GetBoolean("is_anonymous");
                QuestionInfo room = new QuestionInfo(questionId, question, type, isGraded, isAnonymous);
                questions.Add(room);
            }

            connection.Close();
            return questions;
        }

        public QuestionInfo CreateQuestion(string type)
        {
            //Type == "MC", "SA", "TF", or "R" (i think)
            //SELECT SCOPE_IDENTITY();
            SqlConnection connection = getConnection();
            connection.Open();
            string questionName = ""; 

            // Create Query
            string query = "INSERT INTO Question (question, question_type, is_graded, is_anonymous) VALUES (@question, @questionType, @isGraded, @isAnonymous) SELECT SCOPE_IDENTITY();";
 
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@question", questionName);
            command.Parameters.AddWithValue("@questionType", type);
            command.Parameters.AddWithValue("@isGraded", true);
            command.Parameters.AddWithValue("@isAnonymous", false);
            // Execute
            int questionId = (int)command.ExecuteNonQuery();
            connection.Close();
            if (questionId == 0)
            {
                return null;
            } else
            {
                QuestionInfo question = new QuestionInfo(questionId, questionName, type, true, false);
                return question; 
            }
        }

        public bool SetQuestionConnection(int setId, int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO QuestionSet (set_id, question_id) VALUES (@setId, @questionId)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@questionId", questionId);

            try
            {
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
            } catch (SqlException  e)
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

            string query = "SELECT * FROM [Option] WHERE option_id IN " + this.ListToSqlString(optionsIds) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<OptionInfo> options = new List<OptionInfo>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString("name");
                object isCorrectValue = reader.GetValue("is_correct");
                bool isCorrect = false;
                if (isCorrectValue != null && isCorrectValue != DBNull.Value)
                {
                    isCorrect = (bool)isCorrectValue;
                }
                object rankIndexValue = reader.GetValue("rank_index");
                int rankIndex = 0;
                if (rankIndexValue != null && rankIndexValue != DBNull.Value)
                {
                    rankIndex = (int)rankIndexValue;
                }
                OptionInfo option = new OptionInfo(id, name, isCorrect, rankIndex);
                options.Add(option);
            }

            connection.Close();
            return options;
        }

        public bool ChangeActiveQuestion(int setId, int nextQuestionId)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Set] SET active_question_id = @nextQuestionId WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@nextQuestionId", nextQuestionId);

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

        public QuestionInfo? GetQuestionFromId(int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Question WHERE question_id = @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);

            SqlDataReader reader = command.ExecuteReader();
            QuestionInfo question = null;
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string questionName = reader.GetString("question");
                string questionType = reader.GetString("question_type");
                bool isGraded = reader.GetBoolean("is_graded");
                bool isAnonymous = reader.GetBoolean("is_anonymous");
                question = new QuestionInfo(id, questionName, questionType, isGraded, isAnonymous);
            }

            connection.Close();
            return question;
        }

        public bool SubmitSAResponse(int userId, int questionId, string answer)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO ShortAnswer (user_id, question_id, response) VALUES (@userId, @questionId, @response)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@questionId", questionId);
            command.Parameters.AddWithValue("@response", answer);

            try
            {
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
            catch (SqlException e)
            {
                connection.Close();
                return false;
            }
        }

        public bool SubmitMCorTFResponse(int userId, int optionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO Response (user_id, option_id) VALUES (@userId, @optionId)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@optionId", optionId);

            try
            {
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
            } catch (SqlException e)
            {
                return false; 
            }
        }

        public bool SubmitRankingResponse(int userId, int optionId, int rankingChosen)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "INSERT INTO Response (user_id, option_id, rank_index) VALUES (@userId, @optionId, @rankIndex)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@optionId", optionId);
            command.Parameters.AddWithValue("@rankIndex", rankingChosen);

            try
            {
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
            catch (SqlException e)
            {
                connection.Close();
                return false;
            }
        }


        //RESPONSES MANAGER

        public List<StudentResponseInfo> GetSAResponsesFromQuestionId(int questionId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM ShortAnswer WHERE question_id = @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);

            SqlDataReader reader = command.ExecuteReader();
            List<StudentResponseInfo> responses = new List<StudentResponseInfo>();

            while (reader.Read())
            {
                int userId = reader.GetInt32("user_id");
                string response = reader.GetString("response");
                responses.Add(new StudentResponseInfo(userId, response)); 
            }

            connection.Close();
            return responses;
        }

        public List<StudentResponseInfo> GetResponsesFromOptions(List<int> optionIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM Response WHERE option_id IN " + this.ListToSqlString(optionIds) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<StudentResponseInfo> studentResponses = new List<StudentResponseInfo>();
            while (reader.Read())
            {
                int userId = reader.GetInt32("user_id");
                int optionId = reader.GetInt32("option_id");
                StudentResponseInfo studentResponse = new StudentResponseInfo(userId, optionId);
                object rankChosen = reader.GetValue("rank_index");
                if (rankChosen != null && rankChosen != DBNull.Value)
                {
                    studentResponse.RankIndex = (int)rankChosen;
                }
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
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM [Set] WHERE set_id IN " + this.ListToSqlString(setIds) + ";";
            SqlCommand command = new(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<SetInfo> result = new List<SetInfo>();
            while (reader.Read())
            {
                int setId = reader.GetInt32(0);
                string name = reader.GetString("name");
                string status = reader.GetString("status");
                bool isActive = reader.GetBoolean("is_active");
                object activeQuestionIdValue = reader.GetValue("active_question_id");
                int activeQuestionId = 0;
                if (activeQuestionIdValue != null && activeQuestionIdValue != DBNull.Value)
                {
                    activeQuestionId = (int)activeQuestionIdValue;
                }
                SetInfo set = new SetInfo(setId, name, status, isActive, activeQuestionId);
                result.Add(set);
            }

            connection.Close();
            return result;
        }

        public SetInfo? CreateSet()
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "INSERT INTO [Set] (name, status, is_active) VALUES (@name, @status, @isActive) SELECT SCOPE_IDENTITY();";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            string name = "Set Name";
            string status = "L";
            bool isActive = false;
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@isActive", isActive);
            // Execute
            int id = (int) command.ExecuteNonQuery();
            if (id > 0)
            {
                connection.Close();
                return new SetInfo(id, name, status, isActive, null);
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public void ConnectSetAndRoom(SetInfo set, int roomId)
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "INSERT INTO RoomSet (set_id, room_id) VALUES (@setId, @roomId);";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@setId", set.Id);
            command.Parameters.AddWithValue("@roomId", roomId);
            // Execute
            int id = (int)command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteSet(int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Set WHERE set_id = @setId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteQuestionsFromSet(int setId, List<int> questionIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM QuestionSet WHERE set_id = @setId AND question_id IN " + this.ListToSqlString(questionIds) + ";";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteQuestionsFromIds(List<int> questionIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Question WHERE question_id IN " + this.ListToSqlString(questionIds) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }
        public void RemoveSetFromRoom(int roomId, int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM RoomSet WHERE set_id = @setId AND room_id = @roomId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@roomId", roomId);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
        }

        public SetInfo? GetSetFromId(int setId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT * FROM [Set] WHERE set_id = @setId";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);

            SqlDataReader reader = command.ExecuteReader();
            SetInfo result = null;
            if (reader.Read())
            {
                string name = reader.GetString("name");
                string status = reader.GetString("status");
                bool isActive = reader.GetBoolean("is_active");
                object activeQuestionIdValue = reader.GetValue("active_question_id");
                int activeQuestionId = 0;
                if (activeQuestionIdValue != null && activeQuestionIdValue != DBNull.Value)
                {
                    activeQuestionId = (int)activeQuestionIdValue;
                }
                result = new SetInfo(setId, name, status, isActive, activeQuestionId);
                connection.Close();
                return result;
            }
            else
            {
                connection.Close();
                return result;
            }
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

        public void ChangeRoomActiveSet(int roomId, int? setId)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Room SET active_set_id = @setId WHERE room_id = @roomId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomId", roomId);
            command.Parameters.AddWithValue("@setId", setId);
            
            try
            {
                int result = (int)command.ExecuteScalar();
            } catch (SqlException e)
            {

            }
            connection.Close();
        }

        public void ChangeStatus(int setId, string newStatus)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Set] SET status = @newStatus WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);

            int result = command.ExecuteNonQuery();
            connection.Close();
        }


    }
}
