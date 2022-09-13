using DEMAT.ApplicationServices.UseCases.CreateZoneAgence;
using DEMAT.ApplicationServices.UseCases.GetAllZoneAgences;
using DEMAT.ApplicationServices.UseCases.GetZoneAgenceById;
using DEMAT.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMAT.Controllers
{
	/// <summary>
	/// ZoneAgence controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ZoneAgenceController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ZoneAgenceController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ZoneAgenceController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public ZoneAgenceController(IMediator mediator, ILogger<ZoneAgenceController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateZoneAgence([FromQuery] CreateZoneAgenceCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ZoneAgenceModel>> GetZoneAgenceById([FromQuery] Guid ZoneagenceId)
		{
			//Parameters Validation.			
			if (ZoneagenceId == null || ZoneagenceId == Guid.Empty)
			{
				return BadRequest("ZoneAgence Id is mandatory !");
			}
			var result = await _mediator.Send(new GetZoneAgenceByIdQuery(ZoneagenceId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ZoneAgenceModel>>> GetAllZoneAgences()
		{
			var result = await _mediator.Send(new GetAllZoneAgenceQuery());
			return Ok(result);
		}
		#endregion
	}
}
