namespace Pollaris.Models
{
    public class RoomDashboardInfo
    {
        public RoomDashboardInfo(int userId, RoomInfo room) {
            UserId = userId;
            Room = room;
            UserType = "TA";
        }

        public int UserId { get; set; }
        public string UserType { get; set; }
        public RoomInfo Room { get; set; }
    }
}
