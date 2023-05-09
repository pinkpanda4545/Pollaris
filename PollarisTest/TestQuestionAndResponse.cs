using Microsoft.Data.SqlClient;
using Pollaris._3.Accessors;
using Pollaris.Controllers;
using Pollaris.Managers;
using Pollaris.Models;

namespace NUnitPollarisTest
{
    public class TestQuestionAndResponse
    {

        private QuestionController controller;

        [SetUp]
        public void Setup()
        {
            controller = new QuestionController();
        }


        [Test]
        public void TestShortAnswerQuestionWhenCorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 1;
            List<string> answers = new List<string>();
            answers.Add("good");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsTrue(result, "Expected the answer to be correct but returned incorrect.");
        }

        [Test]
        public void TestShortAnswerQuestionWhenIncorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 1;
            List<string> answers = new List<string>();
            answers.Add("bad");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "Expected the answer to be incorrect but returned correct.");
        }

        [Test]
        public void TestMultipleChoiceQuestionWhenCorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 2;
            List<string> answers = new List<string>();
            answers.Add("B");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsTrue(result, "Expected the answer to be correct but returned incorrect.");
        }

        [Test]
        public void TestMultipleChoiceQuestionWhenIncorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 2;
            List<string> answers = new List<string>();
            answers.Add("A");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "Expected the answer to be incorrect but returned correct.");
        }

        [Test]
        public void TestTrueFalseQuestionWhenCorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 3;
            List<string> answers = new List<string>();
            answers.Add("False");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsTrue(result, "Expected the answer to be correct but returned incorrect.");
        }

        [Test]
        public void TestTrueFalseQuestionWhenIncorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 3;
            List<string> answers = new List<string>();
            answers.Add("True");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "Expected the answer to be incorrect but returned correct.");
        }

        [Test]
        public void TestRankingQuestionWhenCorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 4;
            List<string> answers = new List<string>();
            answers.Add("A");
            answers.Add("B");
            answers.Add("C");
            answers.Add("D");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsTrue(result, "Expected the answer to be correct but returned incorrect.");
        }

        [Test]
        public void TestRankingQuestionWhenIncorrect()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 4;
            List<string> answers = new List<string>();
            answers.Add("D");
            answers.Add("B");
            answers.Add("C");
            answers.Add("A");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "Expected the answer to be incorrect but returned correct.");
        }

        [Test]
        public void TestWhenQuestionNull()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 5;
            List<string> answers = new List<string>();
            answers.Add("D");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "The method did not return false for null questions.");
        }

        [Test]
        public void TestWhenAnswerNull()
        {
            // Setup/Execute
            int userId = 2;
            int roomId = 1;
            int setId = 1;
            int questionID = 1;
            List<string> answers = new List<string>();
            answers.Add(null);
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "The method did not return false for null answers.");
        }

        //[Test]
        //public void TestQuestionIdIsSetProperly()
        //{
        //    // Setup/Execute
        //    int roomId = 1;
        //    int setId = 1;
        //    int questionId = 1;
        //    QuestionManager qM = new QuestionManager();
        //    QuestionInfo question = qM.GetQuestionFromIds(questionId);
        //    bool result = questionId == question.Id;

        //    // Test
        //    Assert.IsTrue(result, "The ID was not saved properly");
        //}

        //[Test]
        //public void TestQuestionIsNotNull()
        //{
        //    // Setup/Execute
        //    int roomId = 1;
        //    int setId = 1;
        //    int questionId = 1;
        //    QuestionManager qM = new QuestionManager();
        //    QuestionInfo question = qM.GetQuestionFromIds(roomId, setId, questionId);
        //    bool result = question != null; 

        //    // Test
        //    Assert.IsTrue(result, "The question returned was null");
        //}

        [Test]
        public void TestAnswersEmpty()
        {
            // Setup/Execute
            int userId = 1; 
            int roomId = 1;
            int setId = 1;
            int questionId = 1;
            List<string> answers = new List<string>();
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionId, answers);

            // Test
            Assert.IsFalse(result, "The method did not return false for empty answers.");
        }

        [Test]
        public void TestAnswersNull()
        {
            // Setup/Execute
            int userId = 1;
            int roomId = 1;
            int setId = 1;
            int questionId = 1;
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionId, null);

            // Test
            Assert.IsFalse(result, "The method did not return false for null answers.");
        }

        [Test]
        public void TestValidateStudentUserReturnsFalseIfUserIdEqualsRoomOwnerId()
        {
            // Setup/Execute
            int userId = 1001;
            int roomId = 1;

            UserManager uM = new UserManager();
            bool result = uM.ValidateStudentUser(userId, roomId);

            // Test
            Assert.IsFalse(result, "The method did not return false when the user is the room's instructor.");
        }

        [Test]
        public void TestValidateStudentUserReturnsTrueIfUserIdDoesntEqualRoomOwnerId()
        {
            // Setup/Execute
            int userId = 1;
            int roomId = 1;

            UserManager uM = new UserManager();
            bool result = uM.ValidateStudentUser(userId, roomId);

            // Test
            Assert.IsTrue(result, "The method did not return true when the user isn't the room's instructor.");
        }

        [Test]
        public void TestValidateStudentUserReturnsTrueIfUserIdIsInRoomMembers()
        {
            // Setup/Execute
            int userId = 1;
            int roomId = 1;

            UserManager uM = new UserManager();
            bool result = uM.ValidateStudentUser(userId, roomId);

            // Test
            Assert.IsTrue(result, "The method did not return true when the user is in the room's member list.");
        }

        [Test]
        public void TestValidateStudentUserReturnsFalseIfUserIdIsntInRoomMembers()
        {
            // Setup/Execute
            int userId = 6;
            int roomId = 1;

            UserManager uM = new UserManager();
            bool result = uM.ValidateStudentUser(userId, roomId);

            // Test
            Assert.IsFalse(result, "The method did not return false when the user isn't in the room's member list.");
        }

        [Test]
        public void TestSQLGetUsersFromIds()
        {
            int id = 5;
            SQLAccessor sql = new SQLAccessor();
            UserInfo? result = sql.GetUserFromId(id);
            Console.WriteLine(result);
            Assert.IsTrue(result != null);
        }

        [Test]
        public void TestStuff()
        {
            string connectionString = "Data Source=tcp:pollarissql.database.windows.net,1433;Initial Catalog=Pollaris;User Id=sqladmin;Password=iajdfij#29dfkjb(fj; Column Encryption Setting=enabled";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;

            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            string query = "SELECT email FROM Users WHERE user_id = @userId";
            connection.Open();
            SqlCommand command = new(query, connection);
            // Add parameters
            command.Parameters.AddWithValue("@userId", 1);
            // Execute          
            SqlDataReader reader = command.ExecuteReader();
            string email = "";
            while (reader.Read())
            {
                email = reader.GetString(0);
            }
            bool result = String.Equals(email, "tanphan3103@gmail.com");

            // Test
            Assert.IsTrue(result);
        }
    }
}