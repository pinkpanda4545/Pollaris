namespace Pollaris.Models
{
    public class SignUpInfo
    {
        public SignUpInfo(bool emailAvailable) {
            EmailAvailable= emailAvailable;
        }

        public SignUpInfo(bool emailAvailable, bool userCreated) { 
            EmailAvailable= emailAvailable;
            UserCreated= userCreated;
        }

        public bool EmailAvailable { get; set; }
        public bool UserCreated { get; set; }

        public string GetBorder()
        {
            if (EmailAvailable)
            {
                return "0px";
            } else
            {
                return "2px red solid"; 
            }
        }

        public string GetEmailDisplay()
        {
            if (EmailAvailable)
            {
                return "none"; 
            } else
            {
                return "block"; 
            }
        }

        public string GetValidDisplay()
        {
            if (UserCreated)
            {
                return "none";
            } else
            {
                return "block";
            }
        }
    }
}
