using DEMAT.ApplicationServices.UseCases.CreateAgence;
using DEMAT.ApplicationServices.UseCases.GetAgenceById;
using DEMAT.ApplicationServices.UseCases.GetAllAgences;
using DEMAT.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEMAT.Controllers
{
    /// <summary>
    /// Agence controlleur.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
	[Produces("application/json")]
	public class AgenceController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<AgenceController> _logger;

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AgenceController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public AgenceController(IMediator mediator, ILogger<AgenceController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateAgence([FromQuery] CreateAgenceCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		
	    [HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<AgenceModel>> GetAgenceById([FromQuery] Guid agenceId)
		{
			//Parameters Validation.			
			if (agenceId == null || agenceId == Guid.Empty)
			{
				return BadRequest("Agence Id is mandatory !");
			}
			var result = await _mediator.Send(new GetAgenceByIdQuery(agenceId));
			return Ok(result);
		}




		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<AgenceModel>>> GetAllAgences()
		{
			var result = await _mediator.Send(new GetAllAgencesQuery());
			return Ok(result);
		}

		#endregion
	}
}
