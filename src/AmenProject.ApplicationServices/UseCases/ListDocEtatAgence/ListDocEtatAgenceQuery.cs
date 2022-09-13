using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListDocEtatAgence
{
    public class ListDocEtatAgenceQuery : IRequest<IEnumerable<DocumentsEnAttenteModel>>
    {
    }
}
