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

        [Test]
        public void TestWhenUserIdIsTheSameAsRoomID()
        {
            // Setup/Execute
            int userId = 1;
            int roomId = 1;
            int setId = 1;
            int questionID = 1;
            List<string> answers = new List<string>();
            answers.Add("A");
            bool result = controller.SubmitStudentAnswer(userId, roomId, setId, questionID, answers);

            // Test
            Assert.IsFalse(result, "The method did not return false when the room owner id and user id are the same.");
        }

        [Test]
        public void TestQuestionIdIsSetProperly()
        {
            // Setup/Execute
            int questionId = 1;
            List<OptionInfo> options = new List<OptionInfo>();
            options.Add(new OptionInfo(1, "A", false));
            options.Add(new OptionInfo(2, "B", true));
            options.Add(new OptionInfo(3, "C", false));
            options.Add(new OptionInfo(4, "D", false));
            QuestionInfo question = new QuestionInfo(questionId, "Dummy question here.", "MC", options);
            bool result = questionId == question.Id;

            // Test
            Assert.IsTrue(result, "The ID was not saved properly");
        }
    }
}