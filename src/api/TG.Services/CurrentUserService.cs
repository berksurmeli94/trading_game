using Microsoft.AspNetCore.Http;
using System.Linq;

namespace TG.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public bool IsAuthenticated => context.HttpContext.User.Identity.IsAuthenticated;

        public string UserIPAddress => context.HttpContext.Connection.RemoteIpAddress?.ToString();

        public string UserId
        {
            get
            {
                return context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userID")?.Value ?? context.HttpContext.User.FindFirst("sub")?.Value;
            }
        }
    }
}
