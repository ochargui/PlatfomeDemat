using DEMAT.ApplicationServices.UseCases.CreateDocBrute;
using DEMAT.ApplicationServices.UseCases.GetAllDocBrutes;
using DEMAT.ApplicationServices.UseCases.GetDocBruteByEtatByLotPacketId;
using DEMAT.ApplicationServices.UseCases.GetDocBruteById;
using DEMAT.ApplicationServices.UseCases.GetDocBruteByIdLotPacket;
using DEMAT.ApplicationServices.UseCases.GetPacketById;
using DEMAT.ApplicationServices.UseCases.TypageDocBrute;
using DEMAT.ApplicationServices.UseCases.UpdateEtatdocBrute;
using DEMAT.Domain.Entities;
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
	/// DocBrute controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class DocBruteController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<DocBruteController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DocBruteController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public DocBruteController(IMediator mediator, ILogger<DocBruteController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateDocBrute([FromBody] CreateDocBruteCommand request)
		{
			//Parameters Validation

		       Guid result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<DocBruteModel>> GetDocBruteById([FromQuery] Guid docBruteId)
		{
			//Parameters Validation.			
			if (docBruteId == null || docBruteId == Guid.Empty)
			{
				return BadRequest("DocBrute Id is mandatory !");
			}
			var result = await _mediator.Send(new GetDocBruteByIdQuery(docBruteId));
			return Ok(result);
		}



		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocBruteModel>>> GetAllDocBrutes()
		{
			var result = await _mediator.Send(new GetAllDocBrutesQuery());
			return Ok(result);
		}
		[HttpPut("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocBruteModel>>> UpdateEtat([FromQuery] Guid IdDocBrute ,int Etat)
		{
			var result = await _mediator.Send(new UpdateEtatdocBruteCommand(IdDocBrute,Etat));
			return Ok(result);
		}


		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocBrutePacketRow>>> GetDocBruteByLotPacket([FromQuery] Guid PacketId)
		{
			PacketModel Packet = await _mediator.Send( new GetPacketByIdQuery(PacketId) );
			var result = await _mediator.Send(new GetDocBruteByIdLotPacketQuery(Packet));
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>>TyperDocBruteCreateArchive([FromQuery] Guid OperatorId, [FromQuery] Guid OperationId, [FromQuery] Guid DocBruteId)
		{
			//Parameters Validation

			string result = await _mediator.Send(new TypageDocBruteCommand(OperatorId, OperationId ,DocBruteId));
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocBrutePacketRow>>> GetDocBruteByEtatByLotPacketId([FromQuery] Guid PacketId , [FromQuery] int EtatDoc)
		{
			var result = await _mediator.Send(new GetDocBruteByEtatByLotPacketIdQuery(EtatDoc,PacketId));
			return Ok(result);
		}
		#endregion
	}
}
