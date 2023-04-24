namespace Pollaris.Models
{
    public class SetResponsesInfo
    {
        public SetResponsesInfo(int userId, int roomId, int setId) 
        {
            UserId = userId;
            RoomId = roomId;
            SetId = setId;
            Questions = new List<QuestionInfo>();
        }

        public int UserId { get; set; } 
        public int RoomId { get; set; }
        public int SetId { get; set; }
        public List<QuestionInfo> Questions { get; set; }
        public List<StudentResponseInfo>? Responses { get; set; }
    }
}
