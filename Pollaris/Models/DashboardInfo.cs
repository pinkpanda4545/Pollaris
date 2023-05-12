namespace Pollaris.Models
{
    public class DashboardInfo
    {
        public DashboardInfo(int userId, List<RoomInfo> rooms, string? photo) {
            UserId = userId;
            Rooms = rooms;
            Photo = photo; 
        }

        public int UserId { get; set; }
        public List<RoomInfo> Rooms { get; set; }
        public string? Photo { get; set; }
    }
}
