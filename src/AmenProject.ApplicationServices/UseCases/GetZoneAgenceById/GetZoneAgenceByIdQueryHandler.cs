using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetZoneAgenceById
{
    public  class GetZoneAgenceByIdQueryHandler : IRequestHandler<GetZoneAgenceByIdQuery, ZoneAgenceModel>
    {
        private readonly IZoneAgenceReadRepository _zoneAgenceReadRepository;

        public GetZoneAgenceByIdQueryHandler(IZoneAgenceReadRepository zoneAgenceReadRepository)
        {
            _zoneAgenceReadRepository = zoneAgenceReadRepository;
        }

        public async Task<ZoneAgenceModel> Handle(GetZoneAgenceByIdQuery request, CancellationToken cancellationToken)
        {
           return await _zoneAgenceReadRepository.GetZoneAgenceById(request.ZoneAgenceId, cancellationToken);
        }
    }
}
