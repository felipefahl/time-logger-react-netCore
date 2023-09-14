using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Api.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}
