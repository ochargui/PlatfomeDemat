using DEMAT.ApplicationServices.UseCases.CreatePonderate;
using DEMAT.ApplicationServices.UseCases.GetPonderateById;
using DEMAT.ApplicationServices.UseCases.GetAllPonderates;
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
	/// Ponderate controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class PonderateController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PonderateController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="PonderateController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public PonderateController(IMediator mediator, ILogger<PonderateController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreatePonderate([FromBody] CreatePonderateCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PonderateModel>> GetPonderateById([FromQuery] Guid PonderateId)
		{
			//Parameters Validation.			
			if (PonderateId == null || PonderateId == Guid.Empty)
			{
				return BadRequest("Ponderate Id is mandatory !");
			}
			var result = await _mediator.Send(new GetPonderateByIdQuery(PonderateId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<PonderateModel>>> GetAllPonderates()
		{
			var result = await _mediator.Send(new GetAllPonderatesQuery());
			return Ok(result);
		}
		#endregion
	}
}
