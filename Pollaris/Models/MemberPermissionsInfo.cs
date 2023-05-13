namespace Pollaris.Models
{
    public class MemberPermissionsInfo
    {
        public MemberPermissionsInfo(int userId, int roomId, UserInfo member, string memberRole)
        {
            UserId = userId;
            RoomId = roomId;
            Member = member;
            MemberRole = memberRole; 
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public UserInfo Member { get; set; }
        public string MemberRole { get; set; }

    }
}