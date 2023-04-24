namespace Pollaris.Models
{
    public class CreateQuestionInfo
    {
        public CreateQuestionInfo(int userId, int roomId, int setId)
        {
            UserId = userId;
            RoomId = roomId;
            SetId = setId;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int SetId { get; set; }
    }
}
