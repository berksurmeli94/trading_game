using TG.Core.Attiributes;

namespace TG.Common.Models.Request
{
    public class RegisterRequestModel
    {
        [CustomRequired]
        public string FirstName { get; set; }
        [CustomRequired]
        public string LastName { get; set; }
        [CustomRequired]
        public string PhoneNumber { get; set; }
        [CustomRequired]
        public string Email { get; set; }
        [CustomRequired]
        public string Password { get; set; }
        [CustomRequired]
        public string RePassword { get; set; }
    }
}
