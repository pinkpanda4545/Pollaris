using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class SetManager
    {

        // Retrieves a list of SetInfo objects for a given roomId by retrieving the Set IDs for the room and querying for the SetInfo objects with those IDs.
        // Inputs:
        //  - roomId: int representing the ID of the room to retrieve sets for.
        // Returns: List<SetInfo> containing the set information for the specified room.
        public List<SetInfo> GetSets(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetSetIdsFromRoomId(roomId);
            List<SetInfo> result = sql.GetSetsFromIds(ids);
            return result;
        }

        // Retrieves the active SetInfo object from a given list of SetInfo objects, if one exists.
        // Inputs:
        //  - sets: List<SetInfo> containing the set information to search for an active set.
        // Returns: SetInfo representing the active set, or null if no active set exists in the given list.

        public SetInfo? GetActiveSet(List<SetInfo> sets)
        {
            foreach (SetInfo set in sets)
            {
                if (set.IsActive) return set;
            }

            return null;
        }

        // Creates a new SetInfo object and associates it with the specified roomId.
        // Inputs:
        //  - roomId: int representing the ID of the room to associate the new set with.
        // Returns: SetInfo representing the newly created set.
        public SetInfo CreateSet(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            SetInfo set = sql.CreateSet();
            sql.ConnectSetAndRoom(set, roomId);
            return set;
        }

        // Deletes a set from a room along with all the questions, options, and responses associated with it.
        // Inputs:
        //  - roomId: int representing the ID of the room from which to delete the set
        //  - setId: int representing the ID of the set to delete
        // Returns: void
        public void DeleteSet(int roomId, int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> questionIds = sql.GetQuestionIdsFromSetId(setId);
            List<int> optionIds = new List<int>();
            foreach (int questionId in questionIds)
            {
                optionIds.AddRange(sql.GetOptionIdsFromQuestionId(questionId));
            }
            sql.DeleteOptionsFromQuestionOptions(optionIds);
            sql.DeleteOptionsFromResponse(optionIds);
            sql.DeleteOptionsFromIds(optionIds);
            sql.DeleteQuestionsFromSet(questionIds);
            sql.DeleteQuestionsFromIds(questionIds);
            sql.RemoveSetFromRoom(roomId, setId);
            sql.DeleteSet(setId);
        }

        // Gets the name of a set from its ID.
        // Inputs:
        //  - setId: int representing the ID of the set
        // Returns: string representing the name of the set
        public string GetSetNameFromId(int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            SetInfo? set = sql.GetSetFromId(setId);
            if (set != null)
            {
                return set.Name;
            }
            return "";
        }

        // Changes the status of a set and makes it active if specified.
        // Inputs:
        //  - setId: int representing the ID of the set
        //  - newStatus: string representing the new status for the set
        //  - makeActive: bool representing whether to make the set active
        // Returns: void
        public void ChangeStatus(int setId, string newStatus, bool makeActive)
        {
            SQLAccessor sql = new SQLAccessor();
            int roomId = sql.GetRoomIdFromSetId(setId);
            if (makeActive)
            {
                sql.ChangeRoomActiveSet(roomId, setId);
                sql.ChangeSetIsActive(setId, true);
            }
            else
            {
                sql.ChangeRoomActiveSet(roomId, null);
                sql.ChangeSetIsActive(setId, false);
            }
            sql.ChangeStatus(setId, newStatus);
        }


        // Changes the active question for a set and returns the index of the new active question.
        // Inputs:
        //  - setId: int representing the ID of the set
        // Returns: int representing the index of the new active question
        public int ChangeActiveQuestion(int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetQuestionIdsFromSetId(setId);
            List<QuestionInfo> questions = sql.GetQuestionsFromIds(ids);
            SetInfo set = sql.GetSetFromId(setId);
            int activeQuestionId = (int)set.ActiveQuestionId;
            int nextQuestionId = questions.IndexOf(questions.Where(x => x.Id == activeQuestionId).First()) + 1;
            sql.ChangeActiveQuestion(setId, nextQuestionId);
            return questions.IndexOf(questions.Where(x => x.Id == nextQuestionId).First());
        }

        // This method changes the name of a set in the database.
        // Inputs:
        // - setId: An int representing the ID of the set to modify.
        // - setName: A string representing the new name for the set.
        // Returns: void
        public void ChangeSetName(int setId, string setName)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeSetName(setId, setName);
        }
    }
}
