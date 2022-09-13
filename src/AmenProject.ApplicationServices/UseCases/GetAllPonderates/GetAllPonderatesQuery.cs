using MediatR;
using DEMAT.Models;
using System;
using System.Collections.Generic;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderates
{
    public class GetAllPonderatesQuery : IRequest<IEnumerable<PonderateModel>>
    {
    }
}
