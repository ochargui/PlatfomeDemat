using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetListOperateurConnecter
{
    public class GetListOperateurConnecterQuery : IRequest<IEnumerable<OperateurModel>>
    {

    }
}
