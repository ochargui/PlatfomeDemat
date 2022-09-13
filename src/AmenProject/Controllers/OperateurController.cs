using DEMAT.ApplicationServices.UseCases.CreateOperator;
using DEMAT.ApplicationServices.UseCases.GetOperatorById;
using DEMAT.ApplicationServices.UseCases.GetOperatorByLogin;
using DEMAT.ApplicationServices.UseCases.SinIn;
using DEMAT.ApplicationServices.UseCases.UpdateDisciplineOperateur;
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
	/// Operateur controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class OperateurController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<OperateurController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OperateurController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public OperateurController(IMediator mediator, ILogger<OperateurController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<OperateurModel>> SinIn(string Login ,  string Password )
		{
		
			var result = await _mediator.Send(new SinInCommand(Login, Password));
			if (result == null)
			{
				return NotFound();
			}else
			{
				return result;
			}
		}

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateOperator([FromQuery] CreateOperateurCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetOperatorById([FromQuery] Guid operatorId)
		{
			//Parameters Validation.			
			if (operatorId == null || operatorId == Guid.Empty)
			{
				return BadRequest("operator Id is mandatory !");
			}
			var result = await _mediator.Send(new GetOperatorByIdQuery(operatorId));
			return Ok(result);
		}
/*
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OperateurModel>>>GetAllOperators()
		{
			var result = await _mediator.Send(new GetAllOperatorQuery());
			return Ok(result);
		}
*/
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> UpdateDiscipline(Guid IdOperateur ,string Discipline)
		{
			Discipline.ToUpper();
			var result = await _mediator.Send(new UpdateDisciplineOperateurCommand(IdOperateur , Discipline));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetOperatorByLogin([FromQuery] string login)
		{
			var result = await _mediator.Send(new GetOperatorByLoginQuery(login));
			return Ok(result);
		}


		#endregion
	}
}
