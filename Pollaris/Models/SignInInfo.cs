namespace Pollaris.Models
{
    public class SignInInfo
    {
        public SignInInfo(bool valid) {
            Valid = valid;
        }
        public bool? Valid { get; set; }
    }
}
