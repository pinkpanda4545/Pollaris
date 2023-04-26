using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class SetController : Controller
    {
        public IActionResult CreateSet(int userId, int roomId)
        {
            //Create new set in the SQL. 
            //Return the setId. 
            string roomName = "[ROOM NAME]";
            int setId = 12345; 
            EditSetInfo model = new EditSetInfo(userId, roomId, roomName, setId);
            return View("EditSet", model);
        }

        public IActionResult SaveSetEdits(int userId, int roomId, int setId)
        {
            //Add more params and save to sql]
            RoomManager rM = new RoomManager();
            RoomInfo room = rM.GetRoomFromId(roomId); 
            RoomDashboardInfo model = new RoomDashboardInfo(userId, room); 
            return RedirectToAction("RoomDashboard", "Dashboard", model);
        }

        public IActionResult DeleteSet(int userId, int roomId, int setId)
        {
            //Delete set from the database. 
            //Update RoomDashboard. 
            RoomManager rM = new RoomManager();
            RoomInfo room = rM.GetRoomFromId(roomId);
            RoomDashboardInfo model = new RoomDashboardInfo(userId, room);
            return RedirectToAction("RoomDashboard", "Dashboard", model);
        }
        public IActionResult EditSet(int userId, int roomId, int setId)
        {
            //SET NAME SHOULDN'T BE PARAMETER!!
            QuestionManager qM = new QuestionManager();
            string roomName = "[ROOM NAME]";
            EditSetInfo model = new EditSetInfo(userId, roomId, roomName, setId, "[SET NAME]");
            model.Questions = qM.GetQuestionsFromSetId(setId);
            return View(model);
        }
        public IActionResult ContinueStatusAndExit(int userId, int roomId, int setId)
        {
            //Change set's status to continue

            RoomManager rM = new RoomManager();
            RoomInfo room = rM.GetRoomFromId(roomId);
            RoomDashboardInfo model = new RoomDashboardInfo(userId, room);
            return RedirectToAction("RoomDashboard", "Dashboard", model);
        }

        public IActionResult FinishStatusAndExit(int userId, int roomId, int setId) 
        {
            //change set's status to reset

            RoomManager rM = new RoomManager();
            RoomInfo room = rM.GetRoomFromId(roomId);
            RoomDashboardInfo model = new RoomDashboardInfo(userId, room);
            return RedirectToAction("RoomDashboard", "Dashboard", model);
        }

        public IActionResult LaunchSet(int userId, int roomId, int setId)
        {
            QuestionManager qM = new QuestionManager();
            ResponsesManager rM = new ResponsesManager(); 
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId); 
            model.Questions = qM.GetQuestionsFromSetId(setId);
            int questionId = model.Questions.Where(x => x.IsActive).First().Id; 
            model.Responses = rM.GetResponsesFromQuestionId(questionId); 
            return View("SetResponses", model);
        }

        public IActionResult ContinueSet(int userId, int roomId, int setId)
        {
            QuestionManager qM = new QuestionManager();
            ResponsesManager rM = new ResponsesManager();
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId);
            model.Questions = qM.GetQuestionsFromSetId(setId);
            int questionId = model.Questions.Where(x => x.IsActive).First().Id;
            model.Responses = rM.GetResponsesFromQuestionId(questionId);
            return View("SetResponses", model);
        }

        public IActionResult ResetSet(int userId, int roomId, int setId)
        {
            QuestionManager qM = new QuestionManager();
            ResponsesManager rM = new ResponsesManager();
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId);
            model.Questions = qM.GetQuestionsFromSetId(setId);
            int questionId = model.Questions.Where(x => x.IsActive).First().Id;
            model.Responses = rM.GetResponsesFromQuestionId(questionId);
            return View("SetResponses", model);
        }

        public IActionResult ChangeQuestionAndReset (int userId, int roomId, int setId, int questionId)
        {
            //SEND OFF SQL QUERY TO CHANGE THE ACTIVE QUESTION
            //When the SQL Query returns, you could just return SetResponses(userId,...); 

            //For now I'll do it manually. 
            //QUESTION ID IS A TEMPORARY PARAMETER?

            QuestionManager qM = new QuestionManager();
            List<QuestionInfo> newQuestions = qM.ChangeActiveQuestion(questionId);

            ResponsesManager rM = new ResponsesManager();
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId);
            model.Questions = newQuestions;
            model.Responses = rM.GetResponsesFromQuestionId(questionId + 1);
            return View("SetResponses", model);
        }

        
    }
}
