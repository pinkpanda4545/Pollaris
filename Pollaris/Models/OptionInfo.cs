namespace Pollaris.Models
{
    public class OptionInfo
    {
        public OptionInfo(int id, string name, bool isCorrect, int rankIndex) { 
            Id = id;
            Name = name;
            IsCorrect = isCorrect;
            RankIndex = rankIndex; 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public int RankIndex { get; set; }

        public string GetRed()
        {
            if (IsCorrect)
            {
                return "red-circle"; 
            } else
            {
                return "empty-circle"; 
            }
        }
    }
}
