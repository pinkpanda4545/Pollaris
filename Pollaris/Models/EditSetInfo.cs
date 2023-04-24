namespace Pollaris.Models
{
    public class EditSetInfo
    {
        public EditSetInfo(int userId, int roomId, string roomName, int setId)
        {
            UserId= userId;
            RoomId= roomId;
            RoomName= roomName;
            SetId= setId;
            SetName = ""; 
        }
        public EditSetInfo(int userId, int roomId, string roomName, int setId, string setName)
        {
            UserId = userId;
            RoomId = roomId;
            RoomName = roomName;
            SetId = setId;
            SetName = setName; 
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int SetId { get; set; }
        public string? SetName { get; set; }
        public List<QuestionInfo>? Questions { get; set; }
    }
}
