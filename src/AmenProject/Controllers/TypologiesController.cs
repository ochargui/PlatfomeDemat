using DEMAT.ApplicationServices.UseCases.CreateTypologie;
using DEMAT.ApplicationServices.UseCases.GetAllTypologies;
using DEMAT.ApplicationServices.UseCases.GetTypologieById;
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
	/// Typologie controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class TypologieController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<TypologieController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TypologieController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public TypologieController(IMediator mediator, ILogger<TypologieController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateTypologie([FromBody] CreateTypologiesCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<TypologieModel>> GetTypologieById([FromQuery] Guid typologietId)
		{
			//Parameters Validation.			
			if (typologietId == null || typologietId == Guid.Empty)
			{
				return BadRequest("Typologie Id is mandatory !");
			}
			var result = await _mediator.Send(new GetTypologieByIdQuery(typologietId));
			return Ok(result);
		}


		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<TypologieModel>>> GetAllTypologies()
		{
			var result = await _mediator.Send(new GetAllTypologiesQuery());
			return Ok(result);
		}


		#endregion
	}
}
