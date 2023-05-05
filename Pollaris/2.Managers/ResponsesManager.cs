using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class ResponsesManager
    {
        public List<StudentResponseInfo> GetResponsesFromQuestionId(int questionId)
        {
            SQLAccessor sql = new SQLAccessor();
            List<int> ids = sql.GetResponseIdsFromQuestionId(questionId);
            List<StudentResponseInfo> responses = sql.GetResponsesFromIds(ids);
            return responses;
        }
    }
}
