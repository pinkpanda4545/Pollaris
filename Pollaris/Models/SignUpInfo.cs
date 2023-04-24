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

        public bool? EmailAvailable { get; set; }
        public bool? UserCreated { get; set; }
    }
}
