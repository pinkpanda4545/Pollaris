namespace Pollaris.Models
{
    public class MemberPermissionsInfo
    {
        public MemberPermissionsInfo(int userId, int roomId, UserInfo member)
        {
            UserId = userId;
            RoomId = roomId;
            Member = member;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public UserInfo Member { get; set; }

    }
}