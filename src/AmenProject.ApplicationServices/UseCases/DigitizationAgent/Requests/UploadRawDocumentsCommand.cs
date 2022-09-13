using DEMAT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.DigitizationAgent.Requests
{
    public class UploadRawDocumentsCommand : IRequest<DocumentPicture>
    {
        public IFormFile File { get; set; }

        public string AgenceCode { get; set; }
        public string AccountingDay { get; set; }

        public UploadRawDocumentsCommand(IFormFile file , string agenceCode,string accountingDay)
        {
            File = file;
            AgenceCode = agenceCode;
            AccountingDay = accountingDay;
   
        }
    }
}
