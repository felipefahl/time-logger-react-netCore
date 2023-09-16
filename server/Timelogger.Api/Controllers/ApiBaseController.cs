using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}
