using Pollaris._3.Accessors;
using Pollaris.Models;
using System;
using System.Data.SqlClient;

namespace Pollaris.Managers
{
    public class UserManager
    {
        // Validates user credentials against the SQL database.
        // Inputs:
        //  - email: string representing the email address of the user
        //  - password: string representing the password of the user
        // Returns: bool indicating whether the user credentials are valid or not
        public bool ValidateUser(string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.SignInValidation(email, password);
            return result;
        }

        // Checks if the given email is in the SQL database.
        // Inputs:
        //  - email: string representing the email address of the user to check
        // Returns: bool indicating whether the email is in the database or not
        public bool IsEmailInDatabase(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.CheckIfEmailInDatabase(email);
            return result;
        }

        // Creates a new user in the SQL database.
        // Inputs:
        //  - firstName: string representing the first name of the user
        //  - lastName: string representing the last name of the user
        //  - email: string representing the email address of the user
        //  - password: string representing the password of the user
        // Returns: bool indicating whether the user was successfully created or not
        public bool CreateUser(string firstName, string lastName, string email, string password)
        {
            SQLAccessor sql = new SQLAccessor();
            int rowsAffected = sql.SignUpValidation(firstName, lastName, email, password);
            if (rowsAffected > 0) return true;
            return false;
        }

        // Gets the user ID associated with the given email address.
        // Inputs:
        //  - email: string representing the email address of the user
        // Returns: int representing the ID of the user associated with the given email address
        public int GetUserIdFromEmail(string email)
        {
            SQLAccessor sql = new SQLAccessor();
            int result = sql.GetUserIdFromEmail(email);
            return result;
        }

        // Gets a list of UserInfo objects representing the users who are members of the specified room.
        // Inputs:
        //  - roomId: int representing the ID of the room to retrieve the user info for
        // Returns: List<UserInfo> containing the user information for the members of the specified room
        public List<UserInfo> GetUsersInRoom(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetMembersFromRoomId(roomId);
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            return result;
        }

        // Gets the user info for the user with the specified ID.
        // Inputs:
        //  - memberId: int representing the ID of the user to retrieve info for
        // Returns: UserInfo representing the user information for the user with the specified ID, or null if the user is not found
        public UserInfo? GetUserFromId(int memberId)
        {
            SQLAccessor sql = new SQLAccessor();
            UserInfo? result = sql.GetUserFromId(memberId);
            return result;
        }

        // Validates that a user is a member of a specified room.
        // Inputs:
        //  - userId: int representing the ID of the user to validate
        //  - roomId: int representing the ID of the room to validate against
        // Returns: bool indicating whether the user is a member of the specified room or not
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

        // Changes the password for a user with the specified user ID.
        // Inputs:
        // - userId: int representing the ID of the user whose password is being changed
        // - oldPassword: string representing the user's current password
        // - newPassword: string representing the user's desired new password
        // - newPassword2: string representing a second instance of the user's desired new password (used to confirm the password change)
        // Returns: bool indicating whether the password change was successful or not
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

        // Saves the profile information for a user
        // Inputs:
        //  - userId: the ID of the user whose profile information needs to be saved
        //  - firstName: the user's first name
        //  - lastName: the user's last name
        // Returns: a bool indicating whether the profile information was successfully saved
        public bool SaveProfileInfo(int userId, string firstName, string lastName)
        {
            SQLAccessor sql = new SQLAccessor();
            bool result = sql.SaveProfileInfo(userId, firstName, lastName);
            return result;
        }

        // Gets the username for a user with the given ID
        // Inputs:
        //  - userId: the ID of the user whose username is needed
        // Returns: a string containing the username for the user with the given ID
        public string GetUserNameFromId(int userId)
        {
            SQLAccessor sql = new SQLAccessor();
            return sql.GetUserNameFromId(userId);
        }
    }
}