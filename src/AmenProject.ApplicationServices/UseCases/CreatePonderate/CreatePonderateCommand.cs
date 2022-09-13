using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreatePonderate
{
    public class CreatePonderateCommand : IRequest<Guid>
    {
        public String Nom { get; set; }
        public String Valeur { get; set; }

        public Guid? IdOperation { get; set; }

        internal Ponderate ToEntity()
        {
            return new Ponderate
            {
                Nom = Nom,
                Valeur = Valeur,
                OperationId = IdOperation
            };
        }


    }
}
