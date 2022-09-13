using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgence
{
    public class ListDocumentsAgenceQuery : IRequest<IEnumerable<DocumentsModel>>
    {
        
    }
}
