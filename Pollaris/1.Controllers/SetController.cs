using Microsoft.AspNetCore.Mvc;
using Pollaris.Managers;
using Pollaris.Models;

namespace Pollaris.Controllers
{
    public class SetController : Controller
    {
        // CreateSet function creates a new set for a room.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // Returns: IActionResult representing the edit set view for the created set
        public IActionResult CreateSet(int userId, int roomId)
        {
            RoomManager rM = new RoomManager();
            SetManager sM = new SetManager();
            string roomName = rM.GetRoomNameFromId(roomId);
            SetInfo set = sM.CreateSet(roomId); 
            EditSetInfo model = new EditSetInfo(userId, roomId, roomName, set.Id);
            return View("EditSet", model);
        }

        // ChangeSetName function changes the name of a set.
        // Inputs:
        // - setId: an integer representing the ID of the set
        // - newName: a string representing the new name of the set
        // Returns: void
        public void ChangeSetName(int setId, string newName)
        {
            SetManager sM = new SetManager();
            sM.ChangeSetName(setId, newName); 
        }

        // DeleteSet function deletes a set from a room.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set to delete
        // Returns: IActionResult representing the room dashboard view
        public IActionResult DeleteSet(int userId, int roomId, int setId)
        {
            SetManager sM = new SetManager();
            sM.DeleteSet(roomId, setId);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        // EditSet function displays the edit set view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // Returns: IActionResult representing the edit set view with the set information
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

        // ContinueStatusAndExit function changes the status of a set to "Continue" and redirects to the room dashboard view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // Returns: IActionResult representing the room dashboard view
        public IActionResult ContinueStatusAndExit(int userId, int roomId, int setId)
        {
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, "C", false);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        // FinishStatusAndExit function changes the status of a set to "Finished" and redirects to the room dashboard view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // Returns: IActionResult representing the room dashboard view
        public IActionResult FinishStatusAndExit(int userId, int roomId, int setId) 
        {
            SetManager sM = new SetManager();
            sM.ChangeStatus(setId, "R", false);
            return Redirect("/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId);
        }

        // SetResponses function displays the set responses view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // - newStatus: a string representing the new status of the set
        // Returns: IActionResult representing the set responses view
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

        // ChangeQuestionAndReset function changes the active question of a set and redirects to the set responses view.
        // Inputs:
        // - userId: an integer representing the ID of the user
        // - roomId: an integer representing the ID of the room
        // - setId: an integer representing the ID of the set
        // Returns: IActionResult representing the set responses view
        public IActionResult ChangeQuestionAndReset (int userId, int roomId, int setId)
        {
            ResponsesManager rM = new ResponsesManager();
            QuestionManager qM = new QuestionManager();
            SetManager sM = new SetManager();
            int activeQuestionIndex = sM.ChangeActiveQuestion(setId); 
            return Redirect("/Set/SetResponses?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&newStatus=" + "C");
        }
    }
}
