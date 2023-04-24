namespace Pollaris.Models
{
    public class EditQuestionInfo
    {
        public EditQuestionInfo(int userId, int roomId, int setId, QuestionInfo question) {
            UserId = userId;
            RoomId = roomId;
            SetId = setId;
            Question = question;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int SetId { get; set; }
        public QuestionInfo Question { get; set; }
    }
}
