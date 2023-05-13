namespace Pollaris.Models
{
    public class UserId
    {
        public UserId(int id)
        {
            Id = id;
            Valid = true; 
        }

        public UserId(int id, bool error)
        {
            Id = id;
            Valid = error;
        }

        public int Id { get; set; }
        public bool Valid { get; set; }
        public string? Photo { get; set; }
    }
}
