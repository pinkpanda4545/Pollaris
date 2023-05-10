namespace Pollaris.Models
{
    public class SetInfo
    {
        public SetInfo(int id, string name, string status, bool isActive, int? activeQuestionId)
        {
            Name = name;
            Id = id;    
            Status = status;
            IsActive = isActive;
            ActiveQuestionId = activeQuestionId; 
            Questions = new List<QuestionInfo>();
        }

        public SetInfo(int id, string name, string status, bool isActive, int? activeQuestionId, List<QuestionInfo> questions)
        {
            Name = name;
            Id = id;
            Status = status;
            IsActive = isActive;
            ActiveQuestionId = activeQuestionId;
            Questions = questions;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public int? ActiveQuestionId { get; set; }
        public List<QuestionInfo> Questions { get; set; }
    }
}
