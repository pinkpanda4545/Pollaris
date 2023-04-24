namespace Pollaris.Models
{
    public class StudentResponseInfo
    {
        public StudentResponseInfo (int? id, string response)
        {
            Id = id;
            Response = response;
        }

        public int? Id { get; set; }
        public string Response { get; set; }
    }
}
