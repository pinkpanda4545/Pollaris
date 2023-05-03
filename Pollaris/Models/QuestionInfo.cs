namespace Pollaris.Models
{
    public class QuestionInfo
    {
        public QuestionInfo(int id, string question, string type, List<OptionInfo> options, bool isActive)
        {
            Id = id;
            Question = question;
            Type = type;
            IsActive = isActive;
            Options = options;
        }

        public QuestionInfo(int id, string question, string type, List<OptionInfo> options) 
        { 
            Id = id;
            Question = question;
            Type = type;
            IsActive = false; 
            Options = options;
        }

        public QuestionInfo(int id, string question, string type)
        {
            Id = id;
            Question = question;
            Type = type;
            IsActive = false;
        }

        public QuestionInfo(int id, string question, string type, bool isActive)
        {
            Id = id;
            Question = question;
            Type = type;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsGraded { get; set; }
        public bool IsAnonymous { get; set; }
        public List<OptionInfo>? Options { get; set; }
    }
}
