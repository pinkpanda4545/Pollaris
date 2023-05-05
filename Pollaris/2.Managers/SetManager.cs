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

        public List<SetInfo> GetContinueSets(int roomId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetSetIdsFromRoomId(roomId);
            List<SetInfo> result = sql.GetSetsFromIds(ids);
            // only returns sets that have a status of "continue"
            List<SetInfo> result = new List<SetInfo>();
            foreach (SetInfo set in sets)
            {
                if (set.Status == "C")
                {
                    result.Add(set);
                }
            }
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
    }
}
