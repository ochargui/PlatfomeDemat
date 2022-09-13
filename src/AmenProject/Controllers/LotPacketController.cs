using DEMAT.ApplicationServices.UseCases.CreatePacket;
using DEMAT.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEMAT.ApplicationServices.UseCases.GetPacketById;
using DEMAT.ApplicationServices.UseCases.GetAllPackets;

using DEMAT.ApplicationServices.UseCases.ImportScannedDocs;
using DEMAT.ApplicationServices.UseCases.CreateInputOputDirectory;
using DEMAT.ApplicationServices.UseCases.CreateArchiveUpdateEtatDocBrute;
using DEMAT.ApplicationServices.UseCases.GetListPacketByEtatDocBrute;
using DEMAT.Api.Requests;

namespace DEMAT.Controllers
{
	/// <summary>
	/// LotPacket controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class LotPacketController : Controller
    {

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<LotPacketController> _logger;
		
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LotPacketController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public LotPacketController(IMediator mediator, ILogger<LotPacketController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateLotPacket([FromBody] CreatePacketCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetPacketById([FromQuery] Guid packetId)
		{
			//Parameters Validation.			
			if (packetId == null || packetId == Guid.Empty)
            {
				return BadRequest("Packet Id is mandatory !");
            }
			var result = await _mediator.Send(new GetPacketByIdQuery(packetId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<PacketModel>>> GetAllPackets()
		{
			var result = await _mediator.Send(new GetAllPacketsQuery());
			return Ok(result);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> CreateDirectoy([FromQuery] CreateInputOutputDirectoryRequest request)
		{

			var result = await _mediator.Send(request.ToCommand());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> ImportScannedDocsFromInputToOutput ([FromQuery] Guid OperateurId)
		{
			var result = await _mediator.Send(new ImportScannedDocsCommand(OperateurId));
		    return Ok(result);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> ArchiverLotPacketDocBrutes([FromQuery] Guid packetId)
		{
			var result = await _mediator.Send(new CreateArchiveUpdateEtatDocBruteCommand(packetId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetListPacketByEtatDocBrute([FromQuery] int Etat)
		{
			
			var result = await _mediator.Send(new GetListPacketByEtatDocBruteQuery(Etat));
			return Ok(result);
		}
		

		#endregion
	}
}
