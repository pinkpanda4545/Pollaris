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
            int activeQuestionId = (int)activeSet.ActiveQuestionId;
            QuestionInfo activeQuestion = activeSet.Questions.Where(x => x.Id == activeQuestionId).First(); 
            int questionIndex = activeSet.Questions.IndexOf(activeQuestion);
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
            QuestionInfo question = qM.CreateQuestion(setId, type);
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

        public void DeleteQuestion(int questionId)
        {

        }

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

        public void ChangeGraded(int questionId, bool isGraded)
        {

        }

        public void ChangeAnonymous (int questionId, bool isAnonymous)
        {

        }

    }
}
