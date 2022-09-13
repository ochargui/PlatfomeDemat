
using DEMAT.ApplicationServices.UseCases.CreateGroupeControle;
using DEMAT.ApplicationServices.UseCases.GetAllGroupeControles;
using DEMAT.ApplicationServices.UseCases.GetGroupeControleById;
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
	/// GroupeControle controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class GroupeControleController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<GroupeControleController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupeControleController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public GroupeControleController(IMediator mediator, ILogger<GroupeControleController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateGroupeControle([FromQuery] CreatGroupeControleCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetGroupeControleByID([FromQuery] Guid groupeControleId)
		{
			//Parameters Validation.			
			if (groupeControleId == null || groupeControleId == Guid.Empty)
			{
				return BadRequest("groupeControle Id is mandatory !");
			}
			var result = await _mediator.Send(new GetGroupeControleByIdQuery(groupeControleId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<GroupeControleModel>>> GetAllGroupeControles()
		{
			var result = await _mediator.Send(new GetAllGroupeControlesQuery());
			return Ok(result);
		}




		#endregion
	}
}
