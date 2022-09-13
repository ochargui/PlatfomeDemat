using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ImportScannedDocs
{
   public  class ImportScannedDocsCommand : IRequest<string>
    {
        public Guid  IdOperateur { get; set; }

        public ImportScannedDocsCommand(Guid idOperateur)
        {
            IdOperateur = idOperateur;
        }
    }

}
