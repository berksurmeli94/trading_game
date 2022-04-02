using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Services
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string UserId { get; }
    }
}
