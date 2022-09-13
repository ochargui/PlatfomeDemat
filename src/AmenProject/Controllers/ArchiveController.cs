using DEMAT.ApplicationServices.UseCases.CreateArchive;
using DEMAT.ApplicationServices.UseCases.CreateArchiveUpdateEtatDocBrute;
using DEMAT.ApplicationServices.UseCases.GetAllArchives;
using DEMAT.ApplicationServices.UseCases.GetArchiveById;
using DEMAT.ApplicationServices.UseCases.UpdateEtatdocBrute;
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
	/// Archive controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ArchiveController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<ArchiveController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ArchiveController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public ArchiveController(IMediator mediator, ILogger<ArchiveController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateArchive([FromBody] CreateArchiveCommand request)
		{
			//Parameters Validation
			
			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<string>> CreateArchiveUpdateEataDocBruteByPacketId([FromQuery]  Guid PacketId)
		{
			//Parameters Validation

			var result = await _mediator.Send(new CreateArchiveUpdateEtatDocBruteCommand(PacketId));
			return Ok(result);
		}


		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PacketModel>> GetArchiveById([FromQuery] Guid archiveId)
		{
			//Parameters Validation.			
			if (archiveId == null || archiveId == Guid.Empty)
			{
				return BadRequest("archive Id is mandatory !");
			}
			var result = await _mediator.Send(new GetArchiveByIdQuery(archiveId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ArchiveModel>>> GetAllArchives()
		{
			var result = await _mediator.Send(new GetAllArchivesQuery());
			return Ok(result);
		}


		

		




		#endregion
	}
}
