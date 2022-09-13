using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetZoneAgenceById
{
    public class GetZoneAgenceByIdQuery : IRequest<ZoneAgenceModel>
    {
        public Guid ZoneAgenceId { get; set; }

        public GetZoneAgenceByIdQuery(Guid zoneAgenceId)
        {
            ZoneAgenceId = zoneAgenceId;
        }
    
    }
}
