namespace Pollaris.Models
{
    public class SetInfo
    {
        public SetInfo(int id, string name, string status, bool isActive)
        {
            Name = name;
            Id = id;    
            Status = status;
            IsActive = isActive;
            Questions = new List<QuestionInfo>();
        }

        public SetInfo(int id, string name, string status, bool isActive, List<QuestionInfo> questions)
        {
            Name = name;
            Id = id;
            Status = status;
            IsActive = isActive;
            Questions = questions;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public List<QuestionInfo> Questions { get; set; }
    }
}
