using DEMAT.ApplicationServices.UseCases.DownloadJCRepport;
using DEMAT.ApplicationServices.UseCases.RapportFacturation;
using DEMAT.ApplicationServices.UseCases.RapportFichierJournalier;
using DEMAT.ApplicationServices.UseCases.RapportQuotidient;
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
	public class ReportingController : Controller
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
		private readonly ILogger<ReportingController> _logger;
		#endregion

		#region Constructors
		public ReportingController(IMediator mediator, ILogger<ReportingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

		#endregion

		#region Methods

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<RapportJCModel>>> Repport__JC([FromQuery] DateTimeOffset DateDebut, [FromQuery] DateTimeOffset dateFin)
		{
			//Parameters Validation.
			if (DateTimeOffset.MinValue == DateDebut)
			{
				return BadRequest("StartDate is mandatory");
			}
			if (DateTimeOffset.MinValue == dateFin)
			{
				return BadRequest("EndDate is mandatory");
			}

			var result = await _mediator.Send(new JCRepportQuery(DateDebut, dateFin));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<RapportQuotidienModel>>> Repport__Quotidient([FromQuery] DateTimeOffset DateDebut, [FromQuery] Boolean Journee)
		{
			//Parameters Validation.
			if (DateTimeOffset.MinValue == DateDebut)
			{
				return BadRequest("Date is mandatory");
			}
		
            var result = await _mediator.Send(new RapportQuotidientQuery(DateDebut, Journee));
			return Ok(result);
		}


		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<RapportQuotidienModel>>> Repport__Facturatoin([FromQuery] DateTimeOffset DateDebut, [FromQuery] DateTimeOffset DateFin)
		{
			//Parameters Validation.
			if (DateTimeOffset.MinValue == DateDebut)
			{
				return BadRequest("DateDebut is mandatory");
			}
			if (DateTimeOffset.MinValue == DateFin)
			{
				return BadRequest("DateFin is mandatory");
			}

			var result = await _mediator.Send(new RapportFacturationQuery(DateDebut, DateFin));
			return Ok(result);
		}
		
	    [HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<RapportQuotidienModel>>> Repport__Journalier([FromQuery] DateTimeOffset DateDebut, [FromQuery] DateTimeOffset DateFin)
		{
			//Parameters Validation.
			if (DateTimeOffset.MinValue == DateDebut)
			{
				return BadRequest("DateDebut is mandatory");
			}
			if (DateTimeOffset.MinValue == DateFin)
			{
				return BadRequest("DateFin is mandatory");
			}

			var result = await _mediator.Send(new RapportFichierJournalierQuery(DateDebut, DateFin));
			return Ok(result);
		}
		#endregion
	}
}
