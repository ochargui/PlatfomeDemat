using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class ControlCommand :IRequest<RawDocument>
    {
        public Guid DocumentId { get; set; }
        public string FieldNumber { get; set; }
        public string ClientSignature { get; set; }
        public string BankStamp { get; set; }
        public string Email { get; set; }



        public ControlCommand(Guid documentId, string fieldNumber, string clientSignature, string bankStamp,string email)
        {
            DocumentId = documentId;
            FieldNumber = fieldNumber;
            ClientSignature = clientSignature;
            BankStamp = bankStamp;
            Email = email;
           
        }
    }
}
