using Pollaris._3.Accessors;
using Pollaris.Models;

namespace Pollaris.Managers
{
    public class ResponsesManager
    {
        public List<StudentResponseInfo> GetResponsesFromQuestionId(int questionId, string type)
        {
            SQLAccessor sql = new SQLAccessor();
            if (type == "MC" || type == "TF")
            {
                List<int> ids = sql.GetOptionIdsFromQuestionId(questionId);
                List<StudentResponseInfo> responses = sql.GetResponsesFromOptions(ids);
                List<OptionInfo> options = sql.GetOptionsFromIds(ids); 
                for (int i = 0; i < responses.Count; i++)
                {
                    responses[i].OptionName = options.Where(x => x.Id == responses[i].OptionId).First().Name;
                    responses[i].UserName = sql.GetUserNameFromId(responses[i].UserId);
                }
                return responses;
            } else if (type == "R")
            {
                List<int> ids = sql.GetOptionIdsFromQuestionId(questionId);
                List<StudentResponseInfo> responses = sql.GetResponsesFromOptions(ids);
                List<OptionInfo> options = sql.GetOptionsFromIds(ids);
                List<int> uniqueUserIds = this.GetUniqueUsers(responses);
                List<StudentResponseInfo> result = new List<StudentResponseInfo>(); 
                foreach (int userId in uniqueUserIds)
                {
                    List<StudentResponseInfo> tempResponses = responses.Where(x => x.UserId == userId).ToList();
                    tempResponses.OrderBy(x => x.RankIndex);
                    string answer = "";
                    for (int i = 0; i < tempResponses.Count; i++)
                    {
                        answer += options.Where(x => x.Id == tempResponses[i].OptionId).First().Name;
                        if (i < tempResponses.Count - 1)
                        {
                            answer += ", ";
                        }
                    }
                    result.Add(new StudentResponseInfo(userId, answer));
                }
                return result;
            } else
            {
                List<StudentResponseInfo> responses = sql.GetSAResponsesFromQuestionId(questionId);
                return responses; 
            }
        }

        public List<int> GetUniqueUsers(List<StudentResponseInfo> responses)
        {
            List<int> result = new List<int>(); 
            for (int i = 0; i < responses.Count;i++)
            {
                if (!result.Contains(responses[i].UserId))
                {
                    result.Add(responses[i].UserId);
                }
            }
            return result;
        }
    }
}
