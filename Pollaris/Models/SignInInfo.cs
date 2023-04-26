namespace Pollaris.Models
{
    public class SignInInfo
    {
        public SignInInfo(bool valid) {
            Valid = valid;
        }
        public bool Valid { get; set; }

        public string GetBorder()
        {
            if (Valid)
            {
                return "0px"; 
            } else
            {
                return "2px red solid"; 
            }
        }

        public string GetDisplay() {
            if (Valid)
            {
                return "none"; 
            } else
            {
                return "block"; 
            }
        }

    }
}
