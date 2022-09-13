using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;
using DEMAT.Domain.Entities;
using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Handlers.Queries
{
    public class GetAllControlsQueryHandler : IRequestHandler<GetAllControlsQuery, IEnumerable<Control>>
    {
        private readonly IControlReadRepository _controlRepository;

        public GetAllControlsQueryHandler(IControlReadRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }
        public async Task<IEnumerable<Control>> Handle(GetAllControlsQuery request, CancellationToken cancellationToken)
        {
            return await _controlRepository.GetAllControls(cancellationToken);
        }
    }
}
