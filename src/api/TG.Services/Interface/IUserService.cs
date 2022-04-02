using TG.API.Model.Request;
using TG.Common.Models.Request;
using TG.Entities;
using System.Threading.Tasks;

namespace TG.Services.Interface
{
    public interface IUserService
    {
        public Task<User> SignInWithEmail(SignInWithEmailRequestModel model);
        public Task<User> Register(RegisterRequestModel model);
    }
}
