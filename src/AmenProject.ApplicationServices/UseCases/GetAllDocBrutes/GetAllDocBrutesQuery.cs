using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllDocBrutes
{
    public  class GetAllDocBrutesQuery : IRequest<IEnumerable<DocBruteModel>>
    {
    }
}
