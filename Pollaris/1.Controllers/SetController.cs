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

        public void ChangeSetName(int setId, string newName)
        {

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
            sM.ChangeStatus(setId, "C", false);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        public IActionResult FinishStatusAndExit(int userId, int roomId, int setId) 
        {
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, "R", false);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        public IActionResult SetResponses(int userId, int roomId, int setId, string newStatus)
        {
            QuestionManager qM = new QuestionManager();
            ResponsesManager rM = new ResponsesManager();
            SetManager sM = new SetManager();

            sM.ChangeStatus(setId, newStatus, true);

            List<SetInfo> sets = sM.GetSets(roomId);
            SetInfo set = sets.Where(x => x.Id == setId).First();
            List<QuestionInfo> questions = qM.GetQuestionsFromSetId(setId);
            int activeQuestionIndex = questions.IndexOf(questions.Where(x => x.Id == set.ActiveQuestionId).First());
            List<StudentResponseInfo> responses = rM.GetResponsesFromQuestionId((int)set.ActiveQuestionId, questions[activeQuestionIndex].Type);

            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId, activeQuestionIndex, questions, responses);
            return View(model);
        }

        public IActionResult ChangeQuestionAndReset (int userId, int roomId, int setId)
        {
            ResponsesManager rM = new ResponsesManager();
            QuestionManager qM = new QuestionManager();
            SetManager sM = new SetManager();
            int activeQuestionIndex = sM.ChangeActiveQuestion(setId); 
            List<QuestionInfo> questions = qM.GetQuestionsFromSetId(setId);
            List<StudentResponseInfo> responses = rM.GetResponsesFromQuestionId(questions[activeQuestionIndex].Id, questions[activeQuestionIndex].Type);
            SetResponsesInfo model = new SetResponsesInfo(userId, roomId, setId, activeQuestionIndex, questions, responses);
            return View("SetResponses", model);
        }
    }
}
