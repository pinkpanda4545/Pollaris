namespace Pollaris.Models
{
    public class QuestionInfo
    {
        public QuestionInfo(int id, string question, string type, bool isGraded, bool isAnonymous)
        {
            Id = id;
            Question = question;
            Type = type;
            IsGraded = isGraded;
            IsAnonymous = isAnonymous;
        }

        public QuestionInfo(int id, string question, string type, bool isGraded, bool isAnonymous, List<OptionInfo> options)
        {
            Id = id;
            Question = question;
            Type = type;
            IsGraded = isGraded;
            IsAnonymous = isAnonymous;
            Options = options;
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public bool IsGraded { get; set; }
        public bool IsAnonymous { get; set; }
        public List<OptionInfo>? Options { get; set; }
    }
}
