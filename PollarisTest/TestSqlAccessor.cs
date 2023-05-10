using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Pollaris._3.Accessors;
using Pollaris.Controllers;
using Pollaris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitPollarisTest
{
    internal class TestSqlAccessor
    {
        private static string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj;";
        private SqlConnection connection;
        private SQLAccessor sql;
        private int validUserId;
        private string validFirstName; 
        private string validLastName;
        private int invalidUserId; 
        private List<int> ids;
        private int roomId;
        private string roomName;
        private string roomCode;
        private int roomInstructorId;
        private string roomInstructorName;
        private int invalidRoomId;
        private List<int> roomIds; 
        private string validEmail;
        private string invalidEmail;
        private bool testCreateFunctions;
        private List<int> validQuestionIds; 

        [SetUp]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
            sql = new SQLAccessor();
            validUserId = 32;
            validFirstName = "Ellenna";
            validLastName = "Divingnzzo"; 
            invalidUserId = 0; 
            ids = new List<int> { 31, 32 };
            roomId = 3;
            invalidRoomId = 1; 
            roomName = "Machine Learning";
            roomCode = "ABC-124";
            roomInstructorId = 32;
            roomInstructorName = "Ellenna Divingnzzo";
            roomIds = new List<int> { 2, 3 };
            validEmail = "ellennamation@gmail.com";
            invalidEmail = "apwiughvnepaiwnvwiapenfiupuwhefpuhawe@gmail.com";
            validQuestionIds = new List<int> { 5, 6, 7, 8 }; 

            testCreateFunctions = false;
        }

        [Test]
        public void TestEmailInDatabaseTrue()
        {
            //Setup
            string email = this.validEmail;
            //Execute
            bool actual = sql.CheckIfEmailInDatabase(email);
            //Test
            Assert.IsTrue(actual);
        }

        [Test]
        public void TestEmailInDatabaseFalse()
        {
            //Setup
            string email = this.invalidEmail;
            //Execute
            bool actual = sql.CheckIfEmailInDatabase(email);
            //Test
            Assert.IsFalse(actual);
        }

        [Test]
        public void TestGetUserIdFromEmailValid()
        {
            //Setup
            string email = this.validEmail;
            int expected = this.validUserId;
            //Execute
            int actual = sql.GetUserIdFromEmail(email);
            bool result = actual == expected;
            //Test
            Assert.IsTrue(result);
        }

        [Test]
        public void TestChangePasswordWithValidInputs()
        {
            //Setup
            int userId = this.validUserId;
            string newPassword = "wassup";
            //Execute
            bool result = sql.ChangePassword(userId, newPassword);
            //Test
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestChangePasswordInvalidUserId()
        {
            //Setup
            int userId = this.invalidUserId;
            string newPassword = "hi";
            //Execute
            bool result = sql.ChangePassword(userId, newPassword);
            //Test
            Assert.IsFalse(result); 
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsId0()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].Id, Is.EqualTo(expected[0].Id));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsFirstName0()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].FirstName, Is.EqualTo(expected[0].FirstName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsLastName0()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].LastName, Is.EqualTo(expected[0].LastName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsPhoto0()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[0].ProfilePhoto, Is.EqualTo(expected[0].ProfilePhoto));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsId1()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].Id, Is.EqualTo(expected[1].Id));
        }

        [Test]
        public void TestGetUsersFromIdsValidIdsFirstName1()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].FirstName, Is.EqualTo(expected[1].FirstName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsLastName1()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].LastName, Is.EqualTo(expected[1].LastName));
        }
        [Test]
        public void TestGetUsersFromIdsValidIdsPhoto1()
        {
            //Setup
            List<int> ids = this.ids;
            List<UserInfo> expected = new List<UserInfo>();
            expected.Add(new UserInfo(ids[0], "Tan", "Phan", ""));
            expected.Add(new UserInfo(ids[1], "Ellenna", "Divingnzzo", ""));
            //Execute
            List<UserInfo> actual = sql.GetUsersFromIds(ids);
            //Test
            Assert.That(actual[1].ProfilePhoto, Is.EqualTo(expected[1].ProfilePhoto));
        }

        [Test] 
        public void TestListToSqlStringHelperFunction()
        {
            //Setup
            List<int> ids = this.ids;
            string expected = "('" + ids[0] + "','" + ids[1] + "')";
            //Execute
            string actual = sql.ListToSqlString(ids);
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test] 
        public void TestGetUsersFromIdsInvalidIdList()
        {
            //Setup
            List<int> ids = new List<int> { this.invalidUserId };
            int expected = 0; 
            //Execute
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            int actual = result.Count;
            //Test
            Assert.That(actual, Is.EqualTo(expected)); 
        }

        [Test]
        public void TestGetUsersFromIdsEmptyIdList()
        {
            //Setup
            List<int> ids = new List<int>();
            int expected = 0;
            //Execute
            List<UserInfo> result = sql.GetUsersFromIds(ids);
            int actual = result.Count;
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestSaveProfileInfoValid()
        {
            //Setup
            int userId = this.validUserId;
            string newFirstName = this.validFirstName;
            string newLastName = this.validLastName; 
            //Execute
            bool result = sql.SaveProfileInfo(userId, newFirstName, newLastName);
            //Test
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestSaveProfileInfoInvalid()
        {
            //Setup
            int userId = this.invalidUserId;
            string newFirstName = this.validFirstName;
            string newLastName = this.validLastName; 
            //Execute
            bool result = sql.SaveProfileInfo(userId, newFirstName, newLastName);
            //Test
            Assert.IsFalse(result);
        }

        [Test] 
        public void TestGetRoomFromIdValidReturnsNotNull()
        {
            //Setup
            int roomId = this.roomId;
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void TestGetRoomFromIdValidId()
        {
            //Setup
            int roomId = this.roomId;
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.That(actual.Id, Is.EqualTo(roomId)); 
        }

        [Test]
        public void TestGetRoomFromIdValidName()
        {
            //Setup
            int roomId = this.roomId;
            string roomName = this.roomName; 
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.That(actual.Name, Is.EqualTo(roomName));
        }

        [Test]
        public void TestGetRoomFromIdValidCode()
        {
            //Setup
            int roomId = this.roomId;
            string roomCode = this.roomCode;
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.That(actual.Code, Is.EqualTo(roomCode));
        }

        [Test]
        public void TestGetRoomFromIdValidInstructorId()
        {
            //Setup
            int roomId = this.roomId;
            int instructorId = this.roomInstructorId;
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.That(actual.InstructorId, Is.EqualTo(instructorId));
        }

        [Test]
        public void TestGetRoomFromIdValidInstructorName()
        {
            //Setup
            int roomId = this.roomId;
            string instructorName = this.roomInstructorName;
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.That(actual.InstructorName, Is.EqualTo(instructorName));
        }

        [Test]
        public void TestGetRoomFromIdInvalidId()
        {
            //Setup
            int roomId = this.invalidRoomId; 
            RoomInfo? actual = sql.GetRoomFromId(roomId);
            Assert.IsNull(actual); 
        }

        [Test]
        public void TestGetRoomsFromIdsValid()
        {
            //Setup
            List<int> ids = this.roomIds;
            //Execute
            List<RoomInfo> rooms = sql.GetRoomsFromIds(ids); 
            //Test
            bool result = rooms.Count > 0;
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestGetRoomsFromIdsValidId0Match()
        {
            //Setup
            List<int> ids = this.roomIds;
            //Execute
            List<RoomInfo> rooms = sql.GetRoomsFromIds(ids);
            //Test
            bool result = rooms[0].Id == ids[0]; 
            Assert.IsTrue(result);
        }

        [Test]
        public void TestGetRoomsFromIdsValidId1Match()
        {
            //Setup
            List<int> ids = this.roomIds;
            //Execute
            List<RoomInfo> rooms = sql.GetRoomsFromIds(ids);
            //Test
            bool result = rooms[1].Id == ids[1];
            Assert.IsTrue(result);
        }

        [Test]
        public void TestGetRoomsFromIdsInvalid()
        {
            //Setup
            List<int> ids = new List<int> { this.invalidRoomId };
            //Execute
            List<RoomInfo> rooms = sql.GetRoomsFromIds(ids);
            //Test
            bool result = rooms.Count == 0;
            Assert.IsTrue(result);
        }

        [Test]
        public void TestUserRoomConnectionValidIds()
        {
            //Setup
            int userId = this.validUserId;
            int roomId = this.roomId;
            //Execute
            bool result = sql.UserRoomConnection(userId, roomId, "I"); 
            Assert.IsTrue(result);
        }

        [Test]
        public void TestUserRoomConnectionInvalidUserId()
        {
            //Setup
            int userId = this.invalidUserId;
            int roomId = this.roomId;
            if (testCreateFunctions)
            {
                //Execute
                bool result = sql.UserRoomConnection(userId, roomId, "I");
                //Test
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void TestUserRoomConnectionInvalidRoomId()
        {
            //Setup
            int userId = this.validUserId;
            int roomId = this.invalidRoomId;
            //Execute
            bool result = sql.UserRoomConnection(userId, roomId, "I");
            Assert.IsFalse(result);
        }

        [Test]
        public void TestCreateRoomValidInputs()
        {
            //Setup
            string roomName = "A";
            string roomCode = "000-000";
            int instructorId = this.validUserId; 
            string instructorName = this.validFirstName + " " + this.validLastName;
            
            if (testCreateFunctions)
            {
                //Execute
                int actual = sql.CreateRoom(roomName, roomCode, instructorId, instructorName);
                bool result = actual != 0;
                //Test
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void TestCreateRoomInvalidInstructorId()
        {
            //Setup 
            string roomName = "A";
            string roomCode = "000-000";
            int instructorId = this.invalidUserId;
            string instructorName = "Hi Hello";
            //Execute
            int result = sql.CreateRoom(roomName, roomCode, instructorId, instructorName);
            //Test
            Assert.That(result, Is.EqualTo(0)); 
        }

        [Test]
        public void TestGetQuestionsFromIdsValid()
        {
            //Setup
            List<int> ids = this.validQuestionIds;
            int expected = this.validQuestionIds.Count;
            //Execute
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(ids);
            int actual = questions.Count; 
            //Test
            Assert.That(actual, Is.EqualTo(expected)); 
        }

        [Test]
        public void TestGetQuestionsFromIdsValidId0()
        {
            //Setup
            List<int> ids = this.validQuestionIds;
            int expected = this.validQuestionIds[0];
            //Execute
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(ids);
            int actual = questions[0].Id;
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetQuestionsFromIdsInvalid()
        {
            //Setup
            List<int> ids = new List<int> { 0 };
            int expected = 0;
            //Execute
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(ids);
            int actual = questions.Count; 
            //Test
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestCreateQuestionValid()
        {
            // Setup
            string questionType = "MC";
            //Execute
            QuestionInfo question = sql.CreateQuestion(questionType);
            // Test
            Assert.IsNotNull(question);
        }

        [Test]
        public void TestSetQuestionConnectionValid()
        {
            //Setup 
            int questionId = this.validQuestionIds[0];
            int setId = 7;
            if(testCreateFunctions)
            {
                //Execute
                bool result = sql.SetQuestionConnection(setId, questionId);
                //Test
                Assert.IsTrue(result);
            }        
        }

        [Test]
        public void TestSetQuestionConnectionInvalidQuestion()
        {
            //Setup
            int questionId = 0;
            int setId = 7;
            //Execute
            bool result = sql.SetQuestionConnection(setId, questionId);
            //Test
            Assert.IsFalse(result); 
        }

        [Test]
        public void TestSetQuestionconnectionInvalidSet()
        {
            //Setup
            int questionId = this.validQuestionIds[0];
            int setId = 0;
            //Execute
            bool result = sql.SetQuestionConnection(setId, questionId);
            //Test
            Assert.IsFalse(result); 
        }

        [Test]
        public void TestGetQuestionFromIdValid()
        {
            //Setup
            int questionId = this.validQuestionIds[0];
            //Execute
            QuestionInfo? result = sql.GetQuestionFromId(questionId);
            //Test
            Assert.IsNotNull(result); 
        }

        [Test]
        public void TestGetQuestionFromIdValidId()
        {
            //Setup
            int questionId = this.validQuestionIds[0];
            //Execute
            QuestionInfo? result = sql.GetQuestionFromId(questionId);
            //Test
            Assert.That(questionId, Is.EqualTo(result.Id)); 
        }

        [Test]
        public void TestGetQuestionFromIdInvalid()
        {
            //Setup
            int questionId = 0;
            //Execute
            QuestionInfo? result = sql.GetQuestionFromId(questionId);
            //Test
            Assert.IsNull(result);
        }

        [Test]
        public void TestSubmitSAResponseValid()
        {
            //Setup
            int questionId = 6;
            int userId = this.validUserId;
            string response = "Hello this is a response.";
            //Execute
            bool result = sql.SubmitSAResponse(userId, questionId, response);
            //Test
            Assert.IsTrue(result);
        }

        public void TestSubmitSAResponseInvalidUser()
        {
            //Setup
            int questionId = 6;
            int userId = this.invalidUserId;
            string response = "Hello this is a response.";
            //Execute
            bool result = sql.SubmitSAResponse(userId, questionId, response);
            //Test
            Assert.IsFalse(result);
        }

        public void TestSubmitSAResponseInvalidQuestion()
        {
            //Setup
            int questionId = 0;
            int userId = this.validUserId;
            string response = "Hello this is a response.";
            //Execute
            bool result = sql.SubmitSAResponse(userId, questionId, response);
            //Test
            Assert.IsFalse(result);
        }

        [Test]
        public void TestSubmitMCorTFResponseValid()
        {
            //Setup
            int userId = this.validUserId;
            int optionId = 2;
            //Execute
            bool result = sql.SubmitMCorTFResponse(userId, optionId);
            //Test
            Assert.IsTrue(result);
        }

        [Test]
        public void TestSubmitMCorTFResponseInvalidUser()
        {
            //Setup
            int userId = this.invalidUserId;
            int optionId = 2;
            //Execute
            bool result = sql.SubmitMCorTFResponse(userId, optionId);
            //Test
            Assert.IsFalse(result);
        }

        [Test]
        public void TestSubmitMCorTFResponseInvalidOption()
        {
            //Setup
            int userId = this.validUserId;
            int optionId = 0;
            //Execute
            bool result = sql.SubmitMCorTFResponse(userId, optionId);
            //Test
            Assert.IsFalse(result);
        }

        //The beginning of the carnage

        [Test]
        public void TestGetUserFromIdWhenIdExists()
        {
            // Setup/execute
            int userId = 31;
            UserInfo? user = sql.GetUserFromId(userId);
            string actual = user.FirstName;
            string expected = "Tan";
            bool result = String.Equals(actual, expected);

            // Test
            Assert.IsTrue(result, "Did not get the user object correctly based on id.");
        }

        [Test]
        public void TestGetUserFromIdWhenIdDoesNotExist()
        {
            // Setup/execute
            int userId = -1;
            UserInfo? user = sql.GetUserFromId(userId);

            // Test
            Assert.IsNull(user, "Did not return a null user object.");
        }

        [Test]
        public void TestGetUserNameFromIdWhenIdExists()
        {
            // Setup/execute
            int userId = 31;
            string userName = sql.GetUserNameFromId(userId);
            string expected = "Tan Phan";
            bool result = String.Equals(userName, expected);

            // Test
            Assert.IsTrue(result, "Did not return the proper user name.");
        }

        [Test]
        public void TestGetUserNameFromIdWhenIdDoesNotExist()
        {
            // Setup/execute
            int userId = -1;
            string userName = sql.GetUserNameFromId(userId);

            // Test
            Assert.IsEmpty(userName, "The username returned was not empty.");
        }

        [Test]
        public void TestGetRoomIdsFromUserIdWhenIdExists()
        {
            // Setup/execute
            int userId = 31;
            int actual = sql.GetRoomIdsFromUserId(userId)[0];
            int expected = 2;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper room id based on user id.");
        }

        [Test]
        public void TestGetRoomIdsFromUserIdWhenIdDoesNotExist()
        {
            // Setup/execute
            int userId = -1;
            int actual = sql.GetRoomIdsFromUserId(userId).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of ids.");
        }

        [Test]
        public void TestCodeNotAvailableWhenCodeIsNotAvailable()
        {
            // Setup/execute
            string roomCode = "ABC-123";
            bool actual = sql.CodeNotAvailable(roomCode);

            // Test
            Assert.IsTrue(actual, "Did not return true when the room code is already used.");
        }

        [Test]
        public void TestCodeNotAvailableWhenCodeIsAvailable()
        {
            // Setup/execute
            string roomCode = "-1";
            bool actual = sql.CodeNotAvailable(roomCode);

            // Test
            Assert.IsFalse(actual, "Did not return false when the room code is not used.");
        }

        [Test]
        public void TestValidateRoomCodeWhenCodeExists()
        {
            // Setup/execute
            string roomCode = "ABC-123";
            int actual = sql.ValidateRoomCode(roomCode);
            int expected = 2;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper room id based on room code.");
        }

        [Test]
        public void TestValidateRoomCodeWhenCodeDoesNotExist()
        {
            // Setup/execute
            string roomCode = "-1";
            int actual = sql.ValidateRoomCode(roomCode);
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return zero for the room id when the room code does not exist.");
        }

        [Test]
        public void TestGetQuestionIdsFromSetIdWhenSetIdExists()
        {
            // Setup/execute
            int setId = 7;
            int actual = sql.GetQuestionIdsFromSetId(setId)[0];
            int expected = 2;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper question id based on set id");
        }

        [Test]
        public void TestGetQuestionIdsFromSetIdWhenSetIdDoesNotExist()
        {
            // Setup/execute
            int setId = -1;
            int actual = sql.GetQuestionIdsFromSetId(setId).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of question ids when set id does not exist.");
        }

        [Test]
        public void TestSignInValidationWhenAccountInDatabase()
        {
            // Setup/execute
            string email = "tanphan3103@gmail.com";
            string password = "123";
            bool actual = sql.SignInValidation(email, password);

            // Test
            Assert.IsTrue(actual, "Did not return that the email and password are in the database.");
        }

        [Test]
        public void TestSignInValidationWhenAccountNotInDatabase()
        {
            // Setup/execute
            string email = "emailnotindatabase@gmail.com";
            string password = "123";
            bool actual = sql.SignInValidation(email, password);

            // Test
            Assert.IsFalse(actual, "Did not return that the email and password are not in the database.");
        }

        [Test]
        public void TestSignUpValidation()
        {
            // Setup/execute
            string firstName = "John";
            string lastName = "Doe";
            string email = "tanphan3103@gmail.com";
            string password = "123";
            int actual = sql.SignUpValidation(firstName, lastName, email, password);
            int expected = 1;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return that profile was properly inserted into the database.");
        }

        [Test]
        public void TestGetPasswordFromUserIdWhenUserIdInDatabase()
        {
            // Setup/execute
            int userId = 31;
            string? actual = sql.GetPasswordFromUserId(userId);
            string expected = "123";
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not get the right password from the database.");
        }

        [Test]
        public void TestGetPasswordFromUserIdWhenUserIdIsNotInDatabase()
        {
            // Setup/execute
            int userId = -1;
            string? result = sql.GetPasswordFromUserId(userId);

            // Test
            Assert.IsNull(result, "The method did not return a null password.");
        }

        [Test]
        public void TestGetMembersFromRoomIdWhenRoomIdExists()
        {
            // Setup/execute
            int roomId = 2;
            List<int> members = sql.GetMembersFromRoomId(roomId);
            int actual = members[0];
            int expected = 31;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "The method did not return the proper use");
        }

        [Test]
        public void TestGetMembersFromRoomIdWhenRoomIdDoesNotExist()
        {
            // Setup/execute
            int roomId = -1;
            List<int> members = sql.GetMembersFromRoomId(roomId);
            int actual = members.Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "The method did not return a member list of length zero.");
        }

        [Test]
        public void TestGetOptionIdsFromQuestionIdWhenIdExists()
        {
            // Setup/execute
            int questionId = 2;
            int actual = sql.GetOptionIdsFromQuestionId(questionId)[0];
            int expected = 2;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper option id based on question id.");
        }

        [Test]
        public void TestGetOptionIdsFromQuestionIdWhenIdDoesNotExist()
        {
            // Setup/execute
            int questionId = -1;
            int actual = sql.GetOptionIdsFromQuestionId(questionId).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of option ids.");
        }

        [Test]
        public void TestGetOptionsFromIdsWhenIdExists()
        {
            // Setup/execute
            List<int> optionIds = new List<int>();
            optionIds.Add(2);
            optionIds.Add(3);
            int actual = sql.GetOptionsFromIds(optionIds)[0].Id;
            int expected = 2;

            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper option id based on question id.");
        }

        [Test]
        public void TestGetOptionsFromIdsWhenIdDoesNotExist()
        {
            // Setup/execute
            List<int> optionIds = new List<int>();
            optionIds.Add(-1);
            optionIds.Add(-2);
            int actual = sql.GetOptionsFromIds(optionIds).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of option objects.");
        }

        [Test]
        public void TestChangeActiveQuestion()
        {
            // Setup/execute
            int setId = 7;
            int nextQuestionId = 5;
            bool actual = sql.ChangeActiveQuestion(setId, nextQuestionId);

            // Test
            Assert.IsTrue(actual, "Did not update the next question Id.");
        }

        //end of the carnage

        [Test] 
        public void TestSubmitRankingResponseValid()
        {
            //Setup
            int userId = this.validUserId;
            int optionId = 2;
            int rankChosen = 5;
            //Execute
            bool result = sql.SubmitRankingResponse(userId, optionId, rankChosen);
            //Test
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestSubmitRankingResponseInvalidUser()
        {
            //Setup
            int userId = this.invalidUserId;
            int optionId = 2;
            int rankChosen = 5;
            //Execute
            bool result = sql.SubmitRankingResponse(userId, optionId, rankChosen);
            //Test
            Assert.IsFalse(result);
        }

        [Test]
        public void TestSubmitRankingResponseInvalidOption()
        {
            //Setup
            int userId = this.validUserId;
            int optionId = 0;
            int rankChosen = 5;
            //Execute
            bool result = sql.SubmitRankingResponse(userId, optionId, rankChosen);
            //Test
            Assert.IsFalse(result);
        }

        [Test]
        public void TestGetSAResponsesValid()
        {
            //Setup
            int questionId = 11;
            int expected = 2;
            //Execute
            List<StudentResponseInfo> studentResponses = sql.GetSAResponsesFromQuestionId(questionId);
            //Test
            int numResponses = studentResponses.Count;
            Assert.That (numResponses, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetSAResponsesValidResponse0()
        {
            //Setup
            int questionId = 11;
            string expected = "A";
            //Execute
            List<StudentResponseInfo> studentResponses = sql.GetSAResponsesFromQuestionId(questionId);
            //Test
            string firstResponse = studentResponses[0].Response;
            Assert.That(firstResponse, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetSAResponsesValidResponse1()
        {
            //Setup
            int questionId = 11;
            string expected = "B";
            //Execute
            List<StudentResponseInfo> studentResponses = sql.GetSAResponsesFromQuestionId(questionId);
            //Test
            string secondResponse = studentResponses[1].Response;
            Assert.That(secondResponse, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetSAResponsesInvalidQuestionId()
        {
            //Setup
            int questionId = 0;
            int expected = 0;
            //Execute
            List<StudentResponseInfo> studentResponses = sql.GetSAResponsesFromQuestionId(questionId);
            //Test
            int actual = studentResponses.Count;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetResponsesFromOptionsMCValid()
        {
            //Setup
            List<int> optionIds = new List<int>() { 9, 10, 11, 12};
            //Execute
            List<StudentResponseInfo> actual = sql.GetResponsesFromOptions(optionIds);
            //Test
            bool result = actual.Count == 2;
            Assert.IsTrue(result); 
        }

        [Test]
        public void TestGetResponsesFromOptionsMCValidCheckIds()
        {
            //Setup
            List<int> optionIds = new List<int>() { 9, 10, 11, 12 };
            //Execute
            List<StudentResponseInfo> actual = sql.GetResponsesFromOptions(optionIds);
            //Test
            List<int> userIds = actual.Select(x => x.UserId).ToList();
            userIds.Sort(); 
            Assert.That(userIds, Is.EqualTo(this.ids));
        }

        [Test] 
        public void TestGetResponsesFromOptionsInvalid()
        {
            //Setup
            List<int> optionIds = new List<int>() { 0 };
            //Execute
            List<StudentResponseInfo> actual = sql.GetResponsesFromOptions(optionIds);
            //Test
            bool result = actual.Count == 0; 
            Assert.IsTrue(result);
        }

        [Test]
        public void TestGetResponsesFromOptionsRankingValid()
        {
            //Setup
            List<int> optionIds = new List<int>() { 4, 5, 6, 7, 8 };
            int expected = 10; 
            //Execute
            List<StudentResponseInfo> actual = sql.GetResponsesFromOptions(optionIds);
            //Test
            Assert.That(actual.Count, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetSetsFromIdsWhenIdsDoNotExist()
        {
            // Setup/execute
            List<int> setIds = new List<int>();
            setIds.Add(-1);
            setIds.Add(-2);
            int actual = sql.GetSetsFromIds(setIds).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of sets with invalid set ids.");

        }

        [Test]
        public void TestGetSetFromIdWhenIdExists()
        {
            // Setup/execute
            int setId = 7;
            int? actual = sql.GetSetFromId(setId).ActiveQuestionId;
            int expected = 5;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper active question id from the set.");

        }

        [Test]
        public void TestGetSetFromIdWhenIdDoesNotExist()
        {
            // Setup/execute
            int setId = -1;
            SetInfo? actual = sql.GetSetFromId(setId);

            // Test
            Assert.IsNull(actual, "Did not return a null set for a nonexistent set id.");

        }

        [Test]
        public void TestGetSetIdsFromRoomIdWhenRoomIdExists()
        {
            // Setup/execute
            int roomId = 2;
            int actual = sql.GetSetIdsFromRoomId(roomId)[0];
            int expected = 7;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper set id based on room id.");

        }

        [Test]
        public void TestGetSetIdsFromRoomIdWhenRoomIdDoesNotExist()
        {
            // Setup/execute
            int roomId = -1;
            int actual = sql.GetSetIdsFromRoomId(roomId).Count;
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return an empty list of set ids when the room id is invalid.");

        }

        [Test]
        public void TestGetSetsFromIdsWhenIdsExist()
        {
            // Setup/execute
            int firstExpectedId = 7;
            int secondExpectedId = 9;
            List<int> setIds = new List<int>();
            setIds.Add(firstExpectedId);
            setIds.Add(secondExpectedId);
            int firstId = sql.GetSetsFromIds(setIds)[0].Id;
            int secondId = sql.GetSetsFromIds(setIds)[1].Id;
            bool result = firstExpectedId == firstId && secondExpectedId == secondId;

            // Test
            Assert.IsTrue(result, "Did not return the proper sets based on room id.");

        }

        [Test]
        public void TestCreateSet()
        {
            // Setup/execute
            SetInfo? set = sql.CreateSet();

            // Test
            Assert.IsNotNull(set, "Did not properly create a set.");

        }

        [Test]
        public void TestGetRoomIdFromSetIdWhenSetIdExists()
        {
            // Setup/execute
            int setId = 7;
            int actual = sql.GetRoomIdFromSetId(setId);
            int expected = 2;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return the proper room id based on set id.");

        }

        [Test]
        public void TestGetRoomIdFromSetIdWhenSetIdDoesNotExist()
        {
            // Setup/execute
            int setId = -1;
            int actual = sql.GetRoomIdFromSetId(setId);
            int expected = 0;
            bool result = actual == expected;

            // Test
            Assert.IsTrue(result, "Did not return zero when an invalid set id was passed in.");

        }
    }
}
