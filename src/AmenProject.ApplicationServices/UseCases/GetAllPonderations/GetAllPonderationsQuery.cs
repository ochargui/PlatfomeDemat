using MediatR;
using DEMAT.Models;
using System;
using System.Collections.Generic;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderations
{
    public class GetAllPonderationsQuery : IRequest<IEnumerable<AllPonderationModel>>
    {
    }
}
