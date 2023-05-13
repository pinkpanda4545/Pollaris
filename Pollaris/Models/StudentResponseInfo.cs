using Azure.Identity;

namespace Pollaris.Models
{
    public class StudentResponseInfo
    {
        public StudentResponseInfo (int userId, int optionId)
        {
            UserId = userId;
            OptionId = optionId;
        }

        public StudentResponseInfo (int userId, string response)
        {
            UserId = userId;
            Response = response; 
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? OptionId { get; set; }
        public string? OptionName { get; set; }
        public int? RankIndex { get; set; }
        public string? Response { get; set; }
    }
}
