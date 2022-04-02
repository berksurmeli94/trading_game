using TG.Core.Attiributes;

namespace TG.API.Model.Request
{
    public class SignInWithEmailRequestModel
    {
        [CustomRequired]
        public string Email { get; set; }
        [CustomRequired]
        public string Password { get; set; }
    }
}
