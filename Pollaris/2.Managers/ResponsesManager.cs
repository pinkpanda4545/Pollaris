using Pollaris.Models;

namespace Pollaris.Managers
{
    public class ResponsesManager
    {
        public List<StudentResponseInfo> GetResponsesFromQuestionId(int questionId)
        {
            List<StudentResponseInfo> responses = new List<StudentResponseInfo>();
            responses.Add(new StudentResponseInfo(1, "I'm doing great!"));
            responses.Add(new StudentResponseInfo(2, "School is really stressing me out."));
            responses.Add(new StudentResponseInfo(3, "I'm fine."));
            responses.Add(new StudentResponseInfo(4, "No comment."));
            return responses;
        }
    }
}
