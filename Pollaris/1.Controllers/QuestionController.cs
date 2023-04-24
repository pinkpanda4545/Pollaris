﻿using Microsoft.AspNetCore.Mvc;
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
            List<SetInfo> continueSets = sM.GetContinueSets(roomId);
            SetInfo? activeSet = sM.GetActiveSet(continueSets);
            activeSet.Questions = qM.GetQuestionsFromSetId(activeSet.Id, roomId); //TAKE OUT THIS LINE WHEN CONNECTED
            int setSize = activeSet.Questions.Count;
            QuestionInfo? activeQuestion = qM.GetActiveQuestionFromSet(activeSet);
            int? questionIndex = qM.GetActiveQuestionIndex(activeSet.Id, roomId) + 1;

            AnswerQuestionInfo model = new AnswerQuestionInfo(userId, roomId, activeSet.Id, setSize, activeQuestion,  questionIndex);
            return View(model);
        }
        public IActionResult CreateQuestion(int userId, int roomId, int setId)
        {
            CreateQuestionInfo model = new CreateQuestionInfo(userId, roomId, setId); 
            return View(model);
        }
        public IActionResult CreateThenEditQuestion(int userId, int roomId, int setId, string type)
        {
            //Go create a new question in the set and return that here. 
            //For now, I'll put in a dummy question
            QuestionInfo question = new QuestionInfo(1, "Enter your question here.", type);
            question.IsGraded = true;
            if (type != "SA")
            {
                List<string> options = new List<string> { "A", "B", "C", "D" };
                question.Options = options;
            }
            EditQuestionInfo model = new EditQuestionInfo(userId, roomId, setId, question);
         
            return View("EditQuestion", model);
        }
        public IActionResult EditQuestion(int userId, int roomId, int setId, int questionId)
        {
            return View();
        }

    }
}