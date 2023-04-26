namespace Pollaris.Models
{
    public class RoomInfo
    {
        public RoomInfo(string name, string instructor, int id, string userType)
        {
            Name = name;
            Instructor = instructor;
            Id = id;
            Code = "A45-BH9";
            UserType = userType;
            Sets = new List<SetInfo>(); 
        }

        public RoomInfo(string name, string instructor, int id, string userType, List<SetInfo> sets)
        {
            Name = name;
            Instructor = instructor;
            Id = id;
            Code = "A45-BH9";
            UserType = userType;
            Sets = sets;
        }

        public string Name { get; set; }
        public string Instructor { get; set; }
        public int Id { get; set; }
        public string UserType { get; set; }
        public string Code { get; set; }
        public List<SetInfo> Sets { get; set; }
    }
}
