using MediatR;
using DEMAT.Models;
using System;
using System.Collections.Generic;

namespace DEMAT.ApplicationServices.UseCases.GetAllDatas
{
    public class GetAllDatasQuery : IRequest<IEnumerable<DataModel>>
    {
    }
}
