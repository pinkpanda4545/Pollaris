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

        // Returns a new instance of SqlConnection class with the connection string specified in the class constructor
        public SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Converts a list of integer ids to a string formatted for use in an SQL query
        // Inputs:
        //  - ids: A List of integers representing ids to be used in the SQL query
        // Returns: A string formatted for use in an SQL query with the given list of ids
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
                }
                else
                {
                    result += "'" + ids[i].ToString() + "')";
                }
            }
            return result;
        }


        // Validates whether a user's email and password match those stored in the database
        // Inputs:
        //  - email: A string representing the user's email
        //  - password: A string representing the user's password
        // Returns: A boolean indicating whether the email and password match a user in the database
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

        // Checks whether a given email exists in the database
        // Inputs:
        //  - email: A string representing the email to be checked
        // Returns: A boolean indicating whether the email exists in the database
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

        // Inserts a new user into the database
        // Inputs:
        //  - firstName: A string representing the user's first name
        //  - lastName: A string representing the user's last name
        //  - email: A string representing the user's email
        //  - password: A string representing the user's password
        // Returns: An integer representing the number of rows affected by the insert statement
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

        // Retrieves a user's user_id from their email
        // Inputs:
        //  - email: A string representing the user's email
        // Returns: An integer representing the user's ID
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

        // Retrieves the password of the user with the given ID
        // Inputs:
        //   - userId: the ID of the user whose password to retrieve
        // Returns: The password of the user with the given ID, or null if the user does not exist or has no password
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

        // Changes the password of the user with the given ID
        // Inputs:
        //   - userId: the ID of the user whose password to change
        //   - newPassword: the new password to set for the user
        // Returns: True if the password was changed successfully, false otherwise
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

        // Retrieves the IDs of all users who are members of the room with the given ID
        // Inputs:
        //   - roomId: the ID of the room whose members to retrieve
        // Returns: A list of the IDs of all users who are members of the room with the given ID
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

        // Retrieves information about users with the given IDs
        // Inputs:
        //   - ids: a list of user IDs whose information to retrieve
        // Returns: A list of UserInfo objects, one for each user with an ID in the input list
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

        // Returns the user info of the user with the given id
        // Inputs:
        //  - id: the user id to get the info of
        // Returns: the user info (or null if the user does not exist)
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

        // Saves the user's profile info to the database
        // Inputs:
        //  - userId: the id of the user whose info to update
        //  - firstName: the user's first name
        //  - lastName: the user's last name
        // Returns: true if the update was successful, false otherwise
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

        // Returns the username of the user with the given id
        // Inputs:
        //  - userId: the id of the user to get the username of
        // Returns: the username (or an empty string if the user does not exist)
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


        // Retrieves a room's information based on its ID.
        // Inputs:
        //  - id: the integer ID of the room to retrieve.
        // Returns: a nullable RoomInfo object containing the retrieved room's information.
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

        // Retrieves the IDs of all rooms associated with a given user.
        // Inputs:
        //  - userId: the integer ID of the user whose room IDs to retrieve.
        // Returns: a List of integers representing the IDs of all rooms associated with the user.
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

        // Retrieves the information for all rooms associated with a given set of IDs.
        // Inputs:
        //  - ids: a List of integers representing the IDs of the rooms to retrieve.
        // Returns: a List of RoomInfo objects containing the information for all rooms associated with the given IDs.
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

        // Validates the room code passed as a parameter and returns the ID of the room if the code exists in the database
        // Inputs:
        //  - roomCode: string representing the room code to be validated
        // Returns: integer representing the ID of the room if the code exists in the database, or 0 if it doesn't
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

        // Establishes a connection between a user and a room, with a specified role
        // Inputs:
        //  - userId: integer representing the ID of the user to be connected to the room
        //  - roomId: integer representing the ID of the room to which the user will be connected
        //  - role: string representing the role the user will have in the room
        // Returns: boolean indicating whether the connection was successfully established
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
            }
            catch (SqlException ex)
            {
                connection.Close();
                return false;
            }
        }

        // Checks whether a new room code is already in use in the database
        // Inputs:
        //  - newCode: string representing the room code to be checked
        // Returns: boolean indicating whether the code is already in use (true) or not (false)
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

        // Creates a new room in the database
        // Inputs:
        //  - roomName: string representing the name of the new room
        //  - roomCode: string representing the code of the new room
        //  - instructorId: int representing the id of the instructor of the new room
        //  - instructorName: string representing the name of the instructor of the new room
        // Returns: an int representing the id of the newly created room or 0 if the operation fails
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
                string result = command.ExecuteScalar().ToString();
                int roomId = int.Parse(result);
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
            }
            catch (SqlException e)
            {
                connection.Close();
                return 0;
            }
        }

        // Returns the role of a user in a given room
        // Inputs:
        //  - userId: int representing the id of the user
        //  - roomId: int representing the id of the room
        // Returns: a string representing the role of the user in the room, or an empty string if the operation fails
        public string GetRole(int userId, int roomId)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "SELECT role FROM UsersRoom WHERE user_id = @userId AND room_id = @roomId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@roomId", roomId);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string result = reader.GetString("role");
                connection.Close();
                return result;
            }
            else
            {
                connection.Close();
                return "";
            }
        }

        // Returns a list of question ids associated with a given set id
        // Inputs:
        //  - setId: int representing the id of the set
        // Returns: a List<int> representing the ids of the questions associated with the set
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

        // GetQuestionsFromIds retrieves a list of QuestionInfo objects for a given list of question ids.
        // Inputs:
        //  - questionIds: a List of integers representing the ids of the questions to retrieve
        // Returns: a List of QuestionInfo objects for the questions with the given ids
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

        // CreateQuestion creates a new question with the given type and returns its associated QuestionInfo object.
        // Inputs:
        //  - type: a string representing the type of question to create (e.g. "MC", "SA", "TF", or "R")
        // Returns: a QuestionInfo object representing the newly created question, or null if creation was unsuccessful
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
            string result = command.ExecuteScalar().ToString();
            int questionId = int.Parse(result);
            connection.Close();
            if (questionId == 0)
            {
                return null;
            }
            else
            {
                QuestionInfo question = new QuestionInfo(questionId, questionName, type, true, false);
                return question;
            }
        }

        // SetQuestionConnection associates a question with a given set.
        // Inputs:
        //  - setId: an integer representing the id of the set to which to add the question
        //  - questionId: an integer representing the id of the question to add to the set
        // Returns: true if the association was successful, false otherwise
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
            }
            catch (SqlException e)
            {
                connection.Close();
                return false;
            }

        }

        // Returns a list of option IDs that are associated with a given question ID
        // Inputs:
        //  - questionId: an integer representing the ID of the question
        // Returns: A list of integers representing the IDs of the options associated with the question
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

        // Returns a list of OptionInfo objects that correspond to the given option IDs
        // Inputs:
        //  - optionsIds: a list of integers representing the IDs of the options to retrieve
        // Returns: A list of OptionInfo objects representing the retrieved options
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

        // Changes the active question ID for a given set
        // Inputs:
        //  - setId: an integer representing the ID of the set to modify
        //  - nextQuestionId: an integer representing the ID of the next question to make active
        // Returns: A boolean indicating whether the active question was successfully updated in the database
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

        // Returns a QuestionInfo object from the database with the provided questionId
        // Inputs:
        //  - questionId: the ID of the question to retrieve from the database
        // Returns: a QuestionInfo object representing the retrieved question, or null if no question was found
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

        // Submits a short answer response to the database for the provided user and question IDs
        // Inputs:
        //  - userId: the ID of the user submitting the response
        //  - questionId: the ID of the question being responded to
        //  - answer: the short answer response to be submitted
        // Returns: true if the response was successfully submitted, false otherwise
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

        // Submits a multiple choice or true/false response to the database for the provided user and option IDs
        // Inputs:
        //  - userId: the ID of the user submitting the response
        //  - optionId: the ID of the response option being submitted
        // Returns: true if the response was successfully submitted, false otherwise
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
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        // Submits a user's ranking response for an option in a question
        // Inputs:
        //  - userId: int representing the id of the user submitting the response
        //  - optionId: int representing the id of the option the user is ranking
        //  - rankingChosen: int representing the ranking chosen by the user
        // Returns: bool representing whether the response was successfully submitted or not
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

        // Changes the isGraded field of a question
        // Inputs:
        //  - questionId: int representing the id of the question to modify
        //  - isGraded: bool representing whether the question is graded or not
        // Returns: bool representing whether the modification was successful or not
        public bool ChangeGraded(int questionId, bool isGraded)
        {
            //set question isGraded = isGraded
            SqlConnection connection = getConnection();
            string query = "UPDATE Question SET is_graded = @isGraded WHERE question_id = @questionId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@isGraded", isGraded);
            command.Parameters.AddWithValue("@questionId", questionId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 1)
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

        // Changes the isAnonymous field of a question
        // Inputs:
        //  - questionId: int representing the id of the question to modify
        //  - isAnonymous: bool representing whether the question is anonymous or not
        // Returns: bool representing whether the modification was successful or not
        public bool ChangeAnonymous(int questionId, bool isAnonymous)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Question SET is_anonymous = @isAnonymous WHERE question_id = @questionId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@isAnonymous", isAnonymous);
            command.Parameters.AddWithValue("@questionId", questionId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 1)
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

        // Updates the name of a given question in the database.
        // Inputs:
        //   - questionId: an integer representing the ID of the question to be updated.
        //   - questionName: a string representing the new name for the question.
        // Returns: void
        public void ChangeQuestionName(int questionId, string questionName)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "UPDATE [Question] SET question = @questionName WHERE question_id = @questionId;";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@questionId", questionId);
            command.Parameters.AddWithValue("@questionName", questionName);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Updates the "is_correct" value of a given option in the database.
        // Inputs:
        //   - optionId: an integer representing the ID of the option to be updated.
        //   - isCorrect: a boolean value representing whether or not the option is correct.
        // Returns: a boolean value indicating whether or not the update was successful.
        public bool ChangeOptionCorrect(int optionId, bool isCorrect)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Option] SET is_correct = @isCorrect WHERE option_id = @optionId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@isCorrect", isCorrect);
            command.Parameters.AddWithValue("@optionId", optionId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 1)
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

        // Updates the name of a given option in the database.
        // Inputs:
        //   - optionId: an integer representing the ID of the option to be updated.
        //   - optionName: a string representing the new name for the option.
        // Returns: a boolean value indicating whether or not the update was successful.
        public bool ChangeOptionName(int optionId, string optionName)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Option] SET name = @optionName WHERE option_id = @optionId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@optionName", optionName);
            command.Parameters.AddWithValue("@optionId", optionId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 1)
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

        // Creates a new Option and saves it to the database
        // Inputs:
        //  - None
        // Returns: OptionInfo: A nullable OptionInfo object representing the created Option. Returns null if creation fails.
        public OptionInfo? CreateOption()
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "INSERT INTO [Option] (name, is_correct) VALUES (@name, @isCorrect) SELECT SCOPE_IDENTITY();";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            string name = "Option Name";
            bool isCorrect = false;
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@isCorrect", isCorrect);
            // Execute
            string result = command.ExecuteScalar().ToString();
            int id = int.Parse(result);
            if (id > 0)
            {
                connection.Close();
                return new OptionInfo(id, name, isCorrect, null);
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        // Creates a connection between a Question and an Option in the database
        // Inputs:
        //  - questionId: An integer representing the ID of the Question
        //  - optionId: An integer representing the ID of the Option
        // Returns: void
        public void QuestionOptionConnection(int questionId, int optionId)
        {
            SqlConnection connection = getConnection();
            // Create Query
            string query = "INSERT INTO QuestionOption (question_id, option_id) VALUES (@questionId, @optionId);";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@questionId", questionId);
            command.Parameters.AddWithValue("@optionId", optionId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // Deletes the connections between a list of Options and Questions in the database
        // Inputs:
        //  - ids: A List of integers representing the IDs of the Options to be deleted from the QuestionOption table
        // Returns: void
        public void DeleteOptionsFromQuestionOptions(List<int> ids)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM QuestionOption WHERE option_id IN " + this.ListToSqlString(ids) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Deletes the connections between a list of Options and Responses in the database
        // Inputs:
        //  - ids: A List of integers representing the IDs of the Options to be deleted from the Response table
        // Returns: void
        public void DeleteOptionsFromResponse(List<int> ids)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Response WHERE option_id IN " + this.ListToSqlString(ids) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Deletes all options with the given IDs from the database.
        // Inputs:
        //  - ids: a list of integers representing the IDs of the options to be deleted
        // Returns: void
        public void DeleteOptionsFromIds(List<int> ids)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM [Option] WHERE option_id IN " + this.ListToSqlString(ids) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Retrieves all student responses for the given short answer question from the database.
        // Inputs:
        //  - questionId: an integer representing the ID of the short answer question to retrieve responses for
        // Returns: a list of StudentResponseInfo objects representing the student responses for the given question
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

        // Retrieves all student responses for the given multiple choice question options from the database.
        // Inputs:
        //  - optionIds: a list of integers representing the IDs of the multiple choice question options to retrieve responses for
        // Returns: a list of StudentResponseInfo objects representing the student responses for the given options
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


        // Gets a list of set IDs associated with a room ID
        // Inputs:
        //  - roomId: an integer representing the ID of the room
        // Returns: a List of integers representing the IDs of the sets associated with the room
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

        // Gets a list of SetInfo objects associated with a list of set IDs
        // Inputs:
        //  - setIds: a List of integers representing the IDs of the sets
        // Returns: a List of SetInfo objects representing the information about the sets
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

        // Creates a new SetInfo object in the database
        // Inputs: none
        // Returns: a nullable SetInfo object representing the newly created set or null if creation failed
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
            string result = command.ExecuteScalar().ToString();
            int setId = int.Parse(result);
            if (setId > 0)
            {
                connection.Close();
                return new SetInfo(setId, name, status, isActive, null);
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        // Connects a set with a room in the database
        // Inputs:
        //  - set: a SetInfo object that represents the set being connected to the room
        //  - roomId: an integer representing the ID of the room to connect to the set
        // Returns: void
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

        // Deletes a set from the database
        // Inputs:
        //  - setId: an integer representing the ID of the set to be deleted
        // Returns: void
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

        // Deletes questions from a set in the database
        // Inputs:
        //  - questionIds: a List of integers representing the IDs of the questions to be deleted
        // Returns: void
        public void DeleteQuestionsFromSet(List<int> questionIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM QuestionSet WHERE question_id IN " + this.ListToSqlString(questionIds) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Deletes questions from the database by their IDs
        // Inputs:
        //  - questionIds: a List of integers representing the IDs of the questions to be deleted
        // Returns: void
        public void DeleteQuestionsFromIds(List<int> questionIds)
        {
            SqlConnection connection = getConnection();
            connection.Open();

            string query = "DELETE FROM Question WHERE question_id IN " + this.ListToSqlString(questionIds) + ";";
            SqlCommand command = new(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Remove a set from a room
        // Inputs:
        //  - roomId: An integer representing the ID of the room
        //  - setId: An integer representing the ID of the set to remove
        // Returns: void
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

        // Get set information from a set ID
        // Inputs:
        //  - setId: An integer representing the ID of the set to retrieve
        // Returns: A SetInfo object representing the set information, or null if the set is not found
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

        // Get room ID from a set ID
        // Inputs:
        //  - setId: An integer representing the ID of the set to retrieve the room ID from
        // Returns: An integer representing the ID of the room that the set belongs to, or 0 if the set is not found in RoomSet table
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

        // ChangeRoomActiveSet updates the active set id of a given room.
        // Inputs:
        //  - roomId: integer representing the room id to update
        //  - setId: nullable integer representing the id of the new active set for the room
        // Returns: void
        public void ChangeRoomActiveSet(int roomId, int? setId)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Room] SET active_set_id = @setId WHERE room_id = @roomId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@roomId", roomId);
            command.Parameters.AddWithValue("@setId", setId);

            try
            {
                object result = command.ExecuteScalar();
            }
            catch (SqlException e)
            {

            }
            connection.Close();
        }

        // ChangeRoomActiveSet updates the active set id of a given room.
        // Inputs:
        //  - roomId: integer representing the room id to update
        //  - setId: nullable integer representing the id of the new active set for the room
        // Returns: void
        public void ChangeSetIsActive(int setId, bool isActive)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Set] SET is_active = @isActive WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // ChangeStatus updates the status field of a given set.
        // Inputs:
        //  - setId: integer representing the set id to update
        //  - newStatus: string representing the new value for the status field
        // Returns: void
        public void ChangeStatus(int setId, string newStatus)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Set] SET status = @newStatus WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@setId", setId);
            command.Parameters.AddWithValue("@newStatus", newStatus);

            object result = command.ExecuteNonQuery();
            connection.Close();
        }

        // Changes the name of a Set in the database
        // Inputs:
        // - setId: an integer representing the ID of the Set to be modified
        // - newName: a string representing the new name to be assigned to the Set
        // Returns: a boolean value indicating whether the update was successful or not
        public bool ChangeSetName(int setId, string newName)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE [Set] SET name = @newName WHERE set_id = @setId;";
            connection.Open();
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@setId", setId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 1)
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
    }
}
