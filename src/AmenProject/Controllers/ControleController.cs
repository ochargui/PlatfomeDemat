using DEMAT.ApplicationServices.UseCases.CreateControle;
using DEMAT.ApplicationServices.UseCases.GetAllControles;
using DEMAT.ApplicationServices.UseCases.GetControlById;
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
	/// Controle controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ControleController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ControleController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ControleController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public ControleController(IMediator mediator, ILogger<ControleController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateControle([FromQuery] CreateControleCommand request)
		{
			//Parameters Validation
			int c = request.CodeC;
			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ControleModel>> GetControleById([FromQuery] Guid controleId)
		{
			//Parameters Validation.			
			if (controleId == null || controleId == Guid.Empty)
			{
				return BadRequest("controle Id is mandatory !");
			}
			var result = await _mediator.Send(new GetControleByIdQuery(controleId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ControleModel>>> GetAllControles()
		{
			var result = await _mediator.Send(new GetALLControlesQuery());
			return Ok(result);
		}
		#endregion
	}
}
