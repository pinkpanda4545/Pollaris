using Microsoft.AspNetCore.Mvc;
using Pollaris._3.Accessors;
using Pollaris.Managers;
using Pollaris.Models; 

namespace Pollaris.Controllers
{
    public class QuestionController : Controller
    {
        // AnswerQuestion function retrieves the necessary information to answer a question and displays the answer question view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // Returns: IActionResult representing the answer question view
        public IActionResult AnswerQuestion(int userId, int roomId)
        {
            SetManager sM = new SetManager();
            QuestionManager qM = new QuestionManager();
            List<SetInfo> sets = sM.GetSets(roomId);
            SetInfo activeSet = sM.GetActiveSet(sets);
            if (activeSet != null)
            {
                activeSet.Questions = qM.GetQuestionsFromSetId(activeSet.Id);
                int setSize = activeSet.Questions.Count;
                int activeQuestionId = (int)activeSet.ActiveQuestionId;
                QuestionInfo activeQuestion = activeSet.Questions.Where(x => x.Id == activeQuestionId).First();
                int questionIndex = activeSet.Questions.IndexOf(activeQuestion);
                AnswerQuestionInfo model = new AnswerQuestionInfo(userId, roomId, activeSet.Id, setSize, activeQuestion, questionIndex);
                return View(model);
            }
            else
            {
                AnswerQuestionInfo model = new AnswerQuestionInfo(userId, roomId, null, null, null, null);
                return View(model);
            }
        }

        // CreateQuestion function displays the create question view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // Returns: IActionResult representing the create question view
        public IActionResult CreateQuestion(int userId, int roomId, int setId)
        {
            CreateQuestionInfo model = new CreateQuestionInfo(userId, roomId, setId); 
            return View(model);
        }

        // CreateThenEditQuestion function creates a new question and displays the edit question view for the newly created question.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // - type: a string representing the type of the question ("TF" for True/False)
        // Returns: IActionResult representing the edit question view
        public IActionResult CreateThenEditQuestion(int userId, int roomId, int setId, string type)
        {
            QuestionManager qM = new QuestionManager();
            QuestionInfo initialQuestion = qM.CreateQuestion(setId, type);

            if (type == "TF")
            {
                OptionManager oM = new OptionManager();
                int trueOptionId = oM.CreateNewOption(initialQuestion.Id);
                oM.ChangeOptionName(trueOptionId, "True");
                int falseOptionId = oM.CreateNewOption(initialQuestion.Id);
                oM.ChangeOptionName(falseOptionId, "False");
            }
            QuestionInfo question = qM.GetQuestionFromId(initialQuestion.Id);
            EditQuestionInfo model = new EditQuestionInfo(userId, roomId, setId, question);
         
            return View("EditQuestion", model);
        }

        // EditQuestion function displays the edit question view for an existing question.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // - questionId: an integer representing the ID of the question
        // Returns: IActionResult representing the edit question view
        public IActionResult EditQuestion(int userId, int roomId, int setId, int questionId)
        {
            QuestionManager qM = new QuestionManager();
            QuestionInfo question = qM.GetQuestionFromId(questionId); 
            EditQuestionInfo model = new EditQuestionInfo(userId, roomId, setId, question);
            return View(model);
        }

        // DeleteQuestion function deletes a question.
        // Inputs:
        // - questionId: an integer representing the ID of the question
        // Returns: void
        public void DeleteQuestion(int questionId)
        {
            QuestionManager qM = new QuestionManager();
            qM.DeleteQuestion(questionId); 
        }

        // SubmitStudentAnswer function submits a student's answer to a question.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - questionId: an integer representing the ID of the question
        // - answers: a list of strings representing the student's answers
        // Returns: a boolean indicating whether the submission was successful or not
        [HttpPost]
        [Route("/Question/SubmitStudentAnswer")]
        public bool SubmitStudentAnswer(int userId, int roomId, int questionId, List<string> answers)
        {
            if (answers == null || answers.Count < 1) return false; 
            UserManager uM = new UserManager(); 
            if (uM.ValidateStudentUser(userId, roomId))
            {
                QuestionManager qM = new QuestionManager();
                QuestionInfo question = qM.GetQuestionFromId(questionId);
                if (question == null || answers == null) return false;
                return qM.SubmitAnswer(userId, question, answers);
            }
            return false; 
        }

        // ChangeGraded function changes the graded status of a question.
        // Inputs:
        // - questionId: an integer representing the ID of the question
        // - isGraded: a boolean indicating whether the question should be graded or not
        // Returns: void
        public void ChangeGraded(int questionId, bool isGraded)
        {
            QuestionManager qM = new QuestionManager();
            qM.ChangeGraded(questionId, isGraded); 
        }

        // ChangeAnonymous function changes the anonymous status of a question.
        // Inputs:
        // - questionId: an integer representing the ID of the question
        // - isAnonymous: a boolean indicating whether the question should be anonymous or not
        // Returns: void
        public void ChangeAnonymous (int questionId, bool isAnonymous)
        {
            QuestionManager qM = new QuestionManager();
            qM.ChangeAnonymous(questionId, isAnonymous); 
        }

        // ChangeQuestionName function changes the name of a question.
        // Inputs:
        // - questionId: an integer representing the ID of the question
        // - questionName: a string representing the new name of the question
        // Returns: void
        public void ChangeQuestionName(int questionId, string questionName)
        {
            QuestionManager qM = new QuestionManager();
            qM.ChangeQuestionName(questionId, questionName); 
        }

    }
}
