namespace Pollaris.Models
{
    public class UserInfo
    {
        public UserInfo(int id, string role, string firstName, string lastName, string email, string profilePhoto) {
            Id= id;
            Role= role;
            FirstName= firstName;
            LastName= lastName;
            Email= email;
            ProfilePhoto= profilePhoto;
        }
        public UserInfo(int id, string role, string firstName, string lastName, string profilePhoto)
        {
            Id = id;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            ProfilePhoto = profilePhoto;
        }

        public int Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
