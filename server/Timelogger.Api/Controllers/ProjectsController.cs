using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Api.Dtos;
using Timelogger.Api.Mappings;
using Timelogger.Api.Validations;
using Timelogger.Interfaces.Services;

namespace Timelogger.Api.Controllers
{
	/// <summary>
	/// Api Projects Controller
	/// </summary>
	[ApiVersion("1.0")]
	[Produces("application/json")]
	public class ProjectsController : ApiBaseController
	{
		private readonly IProjectService _projectService;

		public ProjectsController(IProjectService projectService)
		{
			_projectService = projectService;
		}

		/// <summary>
		/// Get a list of projects 
		/// </summary>
		/// <remarks>This functionality allows you get a list of projects.</remarks>
		/// <param name="orderByDeadline">A boolean, if true, will sort the list by most recently deadlines</param>
		/// <param name="onlyActives">A boolean, if true, will filter only projects that are not finished yet</param>
		/// <response code="200">OK</response>
		/// <response code="500">Internal Server Error</response>
		[ProducesResponseType(statusCode: 200, type: typeof(List<ProjectGetResponseDto>))]
		[ProducesResponseType(statusCode: 500, type: typeof(ExceptionDto))]
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery(Name = "orderByDeadline")] bool orderByDeadline, [FromQuery(Name = "onlyActives")] bool onlyActives)
		{
			var projects = await _projectService.ListProjectAsync(orderByDeadline, onlyActives);
			var projectResponseList = projects.Select(x => x.ToProjectGetResponseDto());

			return Ok(projectResponseList);
		}

		/// <summary>
		/// Get a Timelog list for a existing project 
		/// </summary>
		/// <remarks>This functionality allows you get a list of timelogs for a existing project.</remarks>
		/// <param name="id">Project Id</param>
		/// <response code="200">OK</response>
		/// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
		/// <response code="500">Internal Server Error</response>
		[ProducesResponseType(statusCode: 200, type: typeof(List<ProjectTimelogGetResponseDto>))]
		[ProducesResponseType(statusCode: 400, type: typeof(ExceptionDto))]
        [ProducesResponseType(statusCode: 404, type: typeof(ExceptionDto))]
		[ProducesResponseType(statusCode: 500, type: typeof(ExceptionDto))]
		[HttpGet("{id}/TimeLogs")]
		public async Task<IActionResult> Get([FromRoute(Name = "id"), Required] Guid id)
		{
			var timeLogs = await _projectService.GetProjectTimeLogListAsync(id);
			var timeLogResponseList = timeLogs.Select(x => x.ToProjectTimelogGetResponseDto());

			return Ok(timeLogResponseList);
		}

		/// <summary>
		/// Create Timelog for a existing project
		/// </summary>
		/// <remarks>This functionality allows you register a timelog for a existing project.</remarks>
		/// <param name="id">Project Id</param>
		/// <param name="timelog">Post the necessary information to register a timelog</param>
		/// <response code="201">Created</response>
		/// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
		/// <response code="500">Internal Server Error</response>
		[Consumes("application/json")]
		[ProducesResponseType(statusCode: 200, type: typeof(TimelogInsertResponseDto))]
		[ProducesResponseType(statusCode: 400, type: typeof(ExceptionDto))]
        [ProducesResponseType(statusCode: 404, type: typeof(ExceptionDto))]
		[ProducesResponseType(statusCode: 500, type: typeof(ExceptionDto))]
		[HttpPost("{id}/TimeLogs")]
		public async Task<IActionResult> InsertTimelog([FromRoute(Name = "id"), Required] Guid id, [FromBody] TimelogInsertRequestDto timelog)
		{
			TimelogInsertRequestValidator.Validate(timelog);
			
			var projectTimeLog = timelog.ToTimelog(id);

			var insertedTimeLog = await _projectService.InsertProjectTimeLogAsync(id, projectTimeLog, timelog.ProjectFinished);

			var timelogInsertResult = insertedTimeLog.ToTimelogInsertResponseDto();

			return Ok(timelogInsertResult);
		}
	}
}
