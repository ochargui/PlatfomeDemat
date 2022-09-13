using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllControles
{
    public class GetALLControlesQuery : IRequest<IEnumerable<ControleModel>>
    {
    }
}
