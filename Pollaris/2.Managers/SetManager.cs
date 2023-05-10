using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class SetManager
    { 
        public List<SetInfo> GetSets(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetSetIdsFromRoomId(roomId);
            List<SetInfo> result = sql.GetSetsFromIds(ids);
            return result;
        }

        public SetInfo? GetActiveSet(List<SetInfo> sets)
        {
            foreach (SetInfo set in sets)
            {
                if (set.IsActive) return set;
            }

            return null;
        }

        public SetInfo CreateSet(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            SetInfo set = sql.CreateSet();
            sql.ConnectSetAndRoom(set, roomId);
            return set;
        }

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

        public void ChangeStatus(int setId, string newStatus, bool makeActive)
        {
            SQLAccessor sql = new SQLAccessor();
            int roomId = sql.GetRoomIdFromSetId(setId);
            if (makeActive)
            {
                sql.ChangeRoomActiveSet(roomId, setId);
                sql.ChangeSetIsActive(setId, true); 
            } else
            {
                sql.ChangeRoomActiveSet(roomId, null);
                sql.ChangeSetIsActive(setId, false); 
            }
            sql.ChangeStatus(setId, newStatus);
        }

        //returns the new active question's index
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

        public void ChangeSetName(int setId, string setName) 
        {
            SQLAccessor sql = new SQLAccessor();
            sql.ChangeSetName(setId, setName); 
        }
    }
}
