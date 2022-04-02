using TG.API.Code;
using Microsoft.AspNetCore.Mvc;

namespace TG.API.Controllers
{
    [ValidateModel]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class BaseController<T> : ControllerBase where T: BaseController<T>
    {
        
    }
}
