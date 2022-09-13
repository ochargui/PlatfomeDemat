using DEMAT.ApplicationServices.UseCases.CreateOperation;
using DEMAT.ApplicationServices.UseCases.GetAllOperations;
using DEMAT.ApplicationServices.UseCases.GetOperationById;
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
	/// Operation controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class OperationController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<OperationController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OperationController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public OperationController(IMediator mediator, ILogger<OperationController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateOperation([FromQuery] CreateOperationCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<OperationModel>> GetOperationById([FromQuery] Guid operationId)
		{
			//Parameters Validation.			
			if (operationId == null || operationId == Guid.Empty)
			{
				return BadRequest("operation Id is mandatory !");
			}
			var result = await _mediator.Send(new GetOperationByIdQuery(operationId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OperationModel>>> GetAllOperations()
		{
			var result = await _mediator.Send(new GetAllOperationsQuery());
			return Ok(result);
		}




		#endregion
	}
}
