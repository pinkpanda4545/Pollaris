namespace Pollaris.Models
{
    public class SetResponsesInfo
    {
        public SetResponsesInfo(int userId, int roomId, int setId, int activeQuestionIndex, List<QuestionInfo> questions, List<StudentResponseInfo> responses) 
        {
            UserId = userId;
            RoomId = roomId;
            SetId = setId;
            ActiveQuestionIndex = activeQuestionIndex;
            Questions = questions;
            Responses = responses;
        }

        public int UserId { get; set; } 
        public int RoomId { get; set; }
        public int SetId { get; set; }
        public int ActiveQuestionIndex { get; set; }
        public List<QuestionInfo> Questions { get; set; }
        public List<StudentResponseInfo>? Responses { get; set; }
    }
}
