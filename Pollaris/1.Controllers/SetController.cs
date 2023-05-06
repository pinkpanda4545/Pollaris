using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class SetController : Controller
    {
        public IActionResult CreateSet(int userId, int roomId)
        {
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager();
            string roomName = rM.GetRoomNameFromId(roomId);
            SetInfo set = sM.CreateSet(roomId); 
            EditSetInfo model = new EditSetInfo(userId, roomId, roomName, set.Id);
            return View("EditSet", model);
        }

        public IActionResult SaveSetEdits(int userId, int roomId, int setId)
        {
            //ADD MORE PARAMS
            SetManager sM = new SetManager();
            sM.SaveSetEdits(setId); //ADD PARAMS 
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId); 
        }

        public IActionResult DeleteSet(int userId, int roomId, int setId)
        {
            SetManager sM = new SetManager();
            sM.DeleteSet(roomId, setId);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }
        public IActionResult EditSet(int userId, int roomId, int setId)
        {
            QuestionManager qM = new QuestionManager();
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager();
            string roomName = rM.GetRoomNameFromId(roomId);
            string setName = sM.GetSetNameFromId(setId); 
            EditSetInfo model = new EditSetInfo(userId, roomId, roomName, setId, setName);
            model.Questions = qM.GetQuestionsFromSetId(setId);
            return View(model);
        }
        public IActionResult ContinueStatusAndExit(int userId, int roomId, int setId)
        {
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, "C");
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        public IActionResult FinishStatusAndExit(int userId, int roomId, int setId) 
        {
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, "R");
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        public IActionResult SetResponses(int userId, int roomId, int setId, string newStatus)
        {
            QuestionManager qM = new QuestionManager();
            ResponsesManager rM = new ResponsesManager();
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, newStatus); 
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId); 
            model.Questions = qM.GetQuestionsFromSetId(setId);
            int questionId = model.Questions.Where(x => x.IsActive).First().Id; 
            model.Responses = rM.GetResponsesFromQuestionId(questionId); 
            return View(model);
        }

        public IActionResult ChangeQuestionAndReset (int userId, int roomId, int setId)
        {
            ResponsesManager rM = new ResponsesManager();
            QuestionManager qM = new QuestionManager();
            QuestionInfo activeQuestion = qM.ChangeActiveQuestion(setId); 
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId);
            model.Questions = qM.GetQuestionsFromSetId(setId);
            model.Responses = rM.GetResponsesFromQuestionId(activeQuestion.Id);
            return View("SetResponses", model);
        }
    }
}
