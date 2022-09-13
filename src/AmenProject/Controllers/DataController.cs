using DEMAT.ApplicationServices.UseCases.CreateData;
using DEMAT.ApplicationServices.UseCases.GetDataById;
using DEMAT.ApplicationServices.UseCases.GetAllDatas;
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
	/// Data controlleur
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class DataController : Controller
	{

		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<DataController> _logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DataController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator.</param>
		/// <param name="logger">The logger.</param>
		public DataController(IMediator mediator, ILogger<DataController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		#endregion

		#region Methods

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateData([FromQuery] CreateDataCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<DataModel>> GetDataById([FromQuery] Guid dataId)
		{
			//Parameters Validation.			
			if (dataId == null || dataId == Guid.Empty)
			{
				return BadRequest("data Id is mandatory !");
			}
			var result = await _mediator.Send(new GetDataByIdQuery(dataId));
			return Ok(result);
		}

		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DataModel>>> GetAllDatas()
		{
			var result = await _mediator.Send(new GetAllDatasQuery());
			return Ok(result);
		}
		#endregion
	}
}
