using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllZoneAgences
{
    class GetAllZoneAgenceQueryHandler : IRequestHandler<GetAllZoneAgenceQuery, IEnumerable<ZoneAgenceModel>>
    {
        private readonly IZoneAgenceReadRepository _zoneAgenceReadRepository;

        public GetAllZoneAgenceQueryHandler(IZoneAgenceReadRepository zoneAgenceReadRepository)
        {
            _zoneAgenceReadRepository = zoneAgenceReadRepository;
        }
    
        public async  Task<IEnumerable<ZoneAgenceModel>> Handle(GetAllZoneAgenceQuery request, CancellationToken cancellationToken)
        {
           return await _zoneAgenceReadRepository.GetAllZoneAgences(cancellationToken);
        }
    }
}
