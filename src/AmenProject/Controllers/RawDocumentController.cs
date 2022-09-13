using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using DEMAT.Domain.Entities;
using System;
using DEMAT.ApplicationServices.UseCases.DigitizationAgent.Requests;
using Newtonsoft.Json;
using DEMAT.ApplicationServices.UseCases.ManualTypingAgent.Requests;
using DEMAT.Domain.Entities.Documents;
using DEMAT.ApplicationServices.UseCases.RawDocuments.Requests;
using System.Collections.Generic;
using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RawDocumentController
    {
        private readonly IMediator _mediator;

        public RawDocumentController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DocumentPicture>> UploadRawDocument(IFormFile File ,string AgenceCode ,string AccountingDay)
        {
            string json = JsonConvert.SerializeObject(File, Formatting.Indented);
            Console.WriteLine($"From controller {json}");
            Console.WriteLine($"From controller {AgenceCode}");
            Console.WriteLine($"From controller {AccountingDay}");
            var result = await _mediator.Send(new UploadRawDocumentsCommand(File, AgenceCode, AccountingDay));
            return result;
        }



        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RawDocument>> ManualTyping(Guid DocumentId , string DocumentType)
        {
            var result = await _mediator.Send(new ManualTypingCommand(DocumentId , DocumentType));
            return result;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RawDocument>> ControlDocument(Guid documentId, string fieldNumber, 
            string clientSignature,string bankStamp,string email)
        {
            var result = await _mediator.Send(new ControlCommand(documentId, fieldNumber, clientSignature, bankStamp,email)) ;
            return result;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<RawDocument>> GetAllRawDocuments()
        {
            var result = await _mediator.Send(new GetAllRawDocumentsQuery());
            return result;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<RawDocument> RetypeDocument(Guid documentId)
        {
            var result = await _mediator.Send(new SendToRetypeDocumentCommand(documentId));
            return result;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<RawDocument> RejectDocument(Guid documentId)
        {
            var result = await _mediator.Send(new RejectDocumentCommand(documentId));
            return result;
        }


    }
}
