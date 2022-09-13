using DEMAT.ApplicationServices.UseCases.GetListOperateurConnecter;
using DEMAT.ApplicationServices.UseCases.GetStatOperationJourne;
using DEMAT.ApplicationServices.UseCases.GetStatOperatorArchive;
using DEMAT.ApplicationServices.UseCases.ListArchiveEnAttanteOperation;
using DEMAT.ApplicationServices.UseCases.ListDocEtatAgence;
using DEMAT.ApplicationServices.UseCases.ListDocumentsAgence;
using DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceDate;
using DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceOperation;
using DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceOperationDate;
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
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DashboardController : Controller
	{
		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<DashboardController> _logger;
        #endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ArchiveController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public DashboardController(IMediator mediator, ILogger<DashboardController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocumentsModel>>> GetDocumentsAgence()
		{
			var result = await _mediator.Send(new ListDocumentsAgenceQuery());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocumentsModel>>> GetDocumentsAgenceOperation([FromQuery] Guid operationId)
		{
			//Parameters Validation.			
			if (operationId == null || operationId == Guid.Empty)
			{
				return BadRequest("operation Id is mandatory !");
			}
			var result = await _mediator.Send(new ListDocumentsAgenceOperationQuery(operationId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocumentsModel>>> GetDocumentsAgenceDate([FromQuery] DateTimeOffset debut , [FromQuery]  DateTimeOffset fin )
		{
			//Parameters Validation.
			if (debut == null || fin == null)
			{
				return BadRequest("Date  is mandatory !");
			}

			var result = await _mediator.Send(new ListDocumentsAgenceDateQuery(debut,fin) );
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocumentsModel>>> GetDocumentsAgenceOperationDate([FromQuery] DateTimeOffset debut, [FromQuery] DateTimeOffset fin, [FromQuery] Guid IdOperation )
		{
			//Parameters Validation.
			if (debut == null || fin == null)
			{
				return BadRequest("Date  is mandatory !");
			}
			if (IdOperation == null || IdOperation == Guid.Empty)
			{
				return BadRequest("operation Id is mandatory !");
			}

			var result = await _mediator.Send(new ListDocumentsAgenceOperationDateQuery(IdOperation,debut, fin));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<JourneOperationModel>>> GetOperationJourne()
		{
			var result = await _mediator.Send(new GetStatOperationJourneQuery());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DocumentsEnAttenteModel>>> GetDocEnAttenteAgence()
		{
			var result = await _mediator.Send(new ListDocEtatAgenceQuery());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ArchiveOperation>>> GetDocEnAttenteParOperation( )
		{
			var result = await _mediator.Send(new ListArchiveEnAttanteIdOperationQuery());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OperateurModel>>> GetOnlineOperators()
		{
			var result = await _mediator.Send(new GetListOperateurConnecterQuery());
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OperateurModel>>>OperateurJourStat([FromQuery] DateTimeOffset debut, [FromQuery] DateTimeOffset fin)
		{   //Parameters Validation.
			if (debut == null || fin == null)
			{
				return BadRequest("Date  is mandatory !");
			}
			/*if (Equipe == null )
			{
				return BadRequest("operation Id is mandatory !");
			}*/
			string Equipe = "jour";
			var result = await _mediator.Send(new GetStatOperatorArchiveQuery(debut, fin, Equipe));
			return Ok(result);
		}


		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OperateurModel>>> OperateurNuitStat([FromQuery] DateTimeOffset debut, [FromQuery] DateTimeOffset fin)
		{   //Parameters Validation.
			if (debut == null || fin == null)
			{
				return BadRequest("Date  is mandatory !");
			}
			string Equipe = "nuit";
			var result = await _mediator.Send(new GetStatOperatorArchiveQuery(debut, fin, Equipe));
			return Ok(result);
		}

		#endregion


	}
}
