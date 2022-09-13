using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllTypologies
{
    public  class GetAllTypologiesQuery : IRequest<IEnumerable<TypologieModel>>
    {
    }
}
