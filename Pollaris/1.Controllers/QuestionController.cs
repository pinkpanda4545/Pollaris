using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models; 

namespace Pollaris.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult AnswerQuestion(int userId, int roomId)
        {
            SetManager sM = new SetManager();
            QuestionManager qM = new QuestionManager();
            List<SetInfo> sets = sM.GetSets(roomId);
            SetInfo activeSet = sM.GetActiveSet(sets);
            activeSet.Questions = qM.GetQuestionsFromSetId(activeSet.Id); 
            int setSize = activeSet.Questions.Count;
            QuestionInfo? activeQuestion = qM.GetActiveQuestionFromSet(activeSet);
            int? questionIndex = qM.GetActiveQuestionIndex(activeSet.Id, roomId) + 1;

            AnswerQuestionInfo model = new AnswerQuestionInfo(userId, roomId, activeSet.Id, setSize, activeQuestion, questionIndex);
            return View(model);
        }
        public IActionResult CreateQuestion(int userId, int roomId, int setId)
        {
            CreateQuestionInfo model = new CreateQuestionInfo(userId, roomId, setId); 
            return View(model);
        }
        public IActionResult CreateThenEditQuestion(int userId, int roomId, int setId, string type)
        {
            QuestionManager qM = new QuestionManager();
            QuestionInfo question = qM.CreateQuestion(type);
            EditQuestionInfo model = new EditQuestionInfo(userId, roomId, setId, question);
         
            return View("EditQuestion", model);
        }

        public IActionResult EditQuestion(int userId, int roomId, int setId, int questionId)
        {
            QuestionManager qM = new QuestionManager();
            QuestionInfo question = qM.GetQuestionFromId(questionId); 
            EditQuestionInfo model = new EditQuestionInfo(userId, roomId, setId, question);
            return View(model);
        }

        [HttpPost]
        [Route("/Question/SubmitStudentAnswer")]
        public bool SubmitStudentAnswer(int userId, int roomId, int setId, int questionId, List<string> answers)
        {
            if (answers == null || answers.Count < 1) return false; 
            UserManager uM = new UserManager(); 
            if (uM.ValidateStudentUser(userId, roomId))
            {
                QuestionManager qM = new QuestionManager();
                QuestionInfo question = qM.GetQuestionFromId(questionId);
                if (question == null || answers == null) return false;
                return qM.SubmitAnswer(userId, roomId, setId, question, answers);
            }
            return false; 
        }
    }
}
