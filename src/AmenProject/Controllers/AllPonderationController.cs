using DEMAT.ApplicationServices.UseCases.CreateAllPonderations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEMAT.ApplicationServices.UseCases.GetAllPonderationsById;
using DEMAT.ApplicationServices.UseCases.GetAllPonderations;
using DEMAT.Models;

namespace DEMAT.Controllers
{
	/// <summary>
	/// AllPonderation controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class AllPonderationController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<AllPonderationController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AllPonderationController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public AllPonderationController(IMediator mediator, ILogger<AllPonderationController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateAllPonderations([FromBody] CreateAllPonderationsCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<AllPonderationModel>> GetAllPonderationsById([FromQuery] Guid allponderationId)
		{
			//Parameters Validation.			
			if (allponderationId == null || allponderationId == Guid.Empty)
			{
				return BadRequest("allPonderation Id is mandatory !");
			}
			var result = await _mediator.Send(new GetAllPonderationsByIdQuery(allponderationId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<AllPonderationModel>>> GetAllPonderations()
		{
			var result = await _mediator.Send(new GetAllPonderationsQuery());
			return Ok(result);
		}
		#endregion
	}
}
