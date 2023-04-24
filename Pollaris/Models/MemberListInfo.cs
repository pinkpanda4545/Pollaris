namespace Pollaris.Models
{
    public class MemberListInfo
    {
        public MemberListInfo(int userId, int roomId, List<UserInfo> users)
        {
            UserId = userId;
            RoomId = roomId;
            Users = users;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public List<UserInfo> Users { get; set; }
    }
}