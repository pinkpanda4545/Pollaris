using Pollaris.Models;

namespace Pollaris.Managers
{
    public class SetManager
    { 
        public List<SetInfo> GetSets(int roomId)
        {
            //Should these return with the list of questions attached? 
            //Or would that be a separate call to add them? 
            List<SetInfo> result = new List<SetInfo>();
            result.Add(new SetInfo(1, "1/2/03", "Launch", false));
            result.Add(new SetInfo(2, "9/2/03", "Continue", true));
            result.Add(new SetInfo(3, "Entrepreneurship Competition", "Reset", false));
            result.Add(new SetInfo(4, "This Is Multiple Words", "Continue", false));
            result.Add(new SetInfo(5, "Wise Words", "Launch", false));
            return result;
        }

        public List<SetInfo> GetContinueSets(int roomId)
        {
            // only returns sets that have a status of "continue"
            List<SetInfo> result = new List<SetInfo>();
            result.Add(new SetInfo(1, "1/2/03", "Continue", false));
            result.Add(new SetInfo(2, "9/2/03", "Continue", true));
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
