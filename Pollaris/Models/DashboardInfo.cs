namespace Pollaris.Models
{
    public class DashboardInfo
    {
        public DashboardInfo(int userId, List<RoomInfo> rooms) {
            UserId = userId;
            Rooms = rooms; 
        }

        public int UserId { get; set; }
        public List<RoomInfo> Rooms { get; set; }

    }
}
