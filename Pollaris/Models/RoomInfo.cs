namespace Pollaris.Models
{
    public class RoomInfo
    {
        public RoomInfo(string name, string instructor, int id, string userType)
        {
            Name = name;
            InstructorName = instructor;
            Id = id;
            Code = "A45-BH9";
            InstructorId = 35905325;
            UserType = userType;
            Sets = new List<SetInfo>(); 
        }

        public RoomInfo(int id, string name, int instructorId, string code)
        {
            Id = id; 
            Name = name;
            InstructorId = instructorId;
            InstructorName = "Steeeeve"; 
            UserType = "Instructor";
            Code = code;
            Sets = new List<SetInfo>();
        }

        public RoomInfo(int id, string name, int instructorId,  string userType, List<SetInfo> sets)
        {
            Id = id;
            Name = name;
            InstructorId = instructorId;
            Code = "A45-BH9";
            UserType = userType;
            Sets = sets;
        }

        public string Name { get; set; }
        public string InstructorName { get; set; }
        public int InstructorId { get; set; }
        public int Id { get; set; }
        public string UserType { get; set; }
        public string Code { get; set; }
        public List<SetInfo> Sets { get; set; }
    }
}
