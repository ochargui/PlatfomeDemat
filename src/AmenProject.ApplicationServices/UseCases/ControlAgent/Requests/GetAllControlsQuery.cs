using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class GetAllControlsQuery : IRequest<IEnumerable<Control>>
    {
    }
}
