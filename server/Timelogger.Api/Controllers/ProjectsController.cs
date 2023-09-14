using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Api.Controllers
{
	[ApiVersion("1.0")]
	public class ProjectsController : ApiBaseController
	{
		private readonly ApiContext _context;

		public ProjectsController(ApiContext context)
		{
			_context = context;
		}

		// GET api/projects
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Projects);
		}
	}
}
