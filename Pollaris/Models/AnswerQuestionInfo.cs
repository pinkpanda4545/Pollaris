using System;
namespace Pollaris.Models
{
    public class AnswerQuestionInfo
    {
        public AnswerQuestionInfo(int userId, int roomId, int? setId, int? setSize, QuestionInfo? question, int? currQuestion)
        {
            UserId = userId;
            RoomId = roomId;
            SetId = setId;
            SetSize = setSize;
            Question = question;
            CurrQuestion = currQuestion;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int? SetId { get; set; }
        public int? SetSize { get; set; }
        public QuestionInfo? Question { get; set; }
        public int? CurrQuestion { get; set; }
    }
}