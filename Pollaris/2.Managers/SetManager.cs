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
            SetInfo set = sql.CreateSet(roomId); 
            return set;
        }

        public void SaveSetEdits(int setId)
        {

        }

        public void DeleteSet(int roomId, int setId)
        {
            SQLAccessor sql = new SQLAccessor();
            sql.DeleteSet(setId);
            sql.RemoveSetFromRoom(roomId, setId); 
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

        public void ChangeStatus(int setId, string newStatus)
        {
            SQLAccessor sql = new SQLAccessor();
            int roomId = sql.GetRoomIdFromSetId(setId);
            int activeSetId = sql.GetActiveSetIdFromRoomId(roomId);
            sql.ChangeActiveSet(activeSetId, setId);
            sql.ChangeStatus(setId, newStatus);
        }

    }
}
