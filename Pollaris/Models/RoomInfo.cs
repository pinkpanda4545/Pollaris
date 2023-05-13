namespace Pollaris.Models
{
    public class RoomInfo
    {
        public RoomInfo(int id, string name, int instructorId, string instructorName, string code)
        {
            Id = id;
            Name = name;
            InstructorId = instructorId;
            InstructorName = instructorName;
            Code = code;
            UserType = "";
            Sets = new List<SetInfo>(); 
        }

        public RoomInfo(int id, string name, int instructorId, string instructorName, string code, List<SetInfo> sets)
        {
            Id = id;
            Name = name;
            InstructorId = instructorId;
            InstructorName = instructorName;
            Code = code;
            UserType = "";
            Sets = sets;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string InstructorName { get; set; }
        public int InstructorId { get; set; }
        public string Code { get; set; }
        public string? UserType { get; set; }
        public List<SetInfo> Sets { get; set; }

        public void UserIdToUserType(int userId)
        {
            if (userId == this.InstructorId)
            {
                this.UserType = "I"; 
            } else
            {
                this.UserType = "TA";
            }
        }
    }
}
