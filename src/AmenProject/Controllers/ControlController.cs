using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ControlController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ControlController> _logger;

        public ControlController(IMediator mediator,
            ILogger<ControlController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RawDocument>> AssignControlToDocument(Guid rawDocumentId, Guid controlId)
        {
            var result = await _mediator.Send(new AssignControlToDocumentCommand(rawDocumentId, controlId));
            return Ok(result);
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Control>> CreateControl(int FieldNumberScore, int ClientSignatureScore, int BankStampScore,int ScoreLimit,string Name)
        {
            var result = await _mediator.Send(new CreateControlCommand( FieldNumberScore, ClientSignatureScore,  BankStampScore,ScoreLimit,Name));
            return Ok(result);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Control>>> GetAllContols()
        {
            var result = await _mediator.Send(new GetAllControlsQuery());
            return Ok(result);
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Control>> DeleteControl(Guid controlId)
        {
            var result = await _mediator.Send(new DeleteControlCommand(controlId));
            return Ok(result);
        }

    }
}
