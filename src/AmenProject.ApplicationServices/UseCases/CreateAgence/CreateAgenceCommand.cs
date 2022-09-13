using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateAgence
{
    public class CreateAgenceCommand : IRequest<Guid>
    {
        public int AgenceCode { get; set; }
        public string AgenceName { get; set; }
        public string AgenceAdresse { get; set; }
        public Guid? IdZoneAgence{ get; set; }

        internal Agence ToEntity()
        {
            return new Agence
            {
                CodeAgence =AgenceCode,
                NomAgence = AgenceName,
                Adresse = AgenceAdresse,
                ZoneAgenceId = IdZoneAgence
            };
        }
    }
}
