using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateZoneAgence
{
    public class CreateZoneAgenceCommand : IRequest<Guid>
    {
        public int CodeZone { get; set; }
        public String AdresseZoneAgence{ get; set; }

      

        internal ZoneAgence ToEntity()
        {
            return new ZoneAgence
            {
                codeZoneAgence = CodeZone,
                ZoneAgenceAdresse = AdresseZoneAgence


            };
        }
    }
}
