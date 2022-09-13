using DEMAT.ApplicationServices.UseCases.CreateLotArchive;
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
    public class LotArchiveController : Controller
	{
		#region Variables
		/// <summary>
		/// The mediator
		/// </summary>
		private readonly IMediator _mediator;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<LotArchiveController> _logger;
        #endregion
        #region Cont
        public LotArchiveController(IMediator mediator, ILogger<LotArchiveController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

		#endregion
		#region Methods
		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Guid>> CreateLotArchive([FromBody] CreateLotArchiveCommand request)
		{
			//Parameters Validation

			Guid result = await _mediator.Send(request);
			return Ok(result);
		}



		#endregion


	}
}
