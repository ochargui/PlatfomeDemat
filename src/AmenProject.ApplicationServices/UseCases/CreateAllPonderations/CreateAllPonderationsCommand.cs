using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateAllPonderations
{
    public class CreateAllPonderationsCommand : IRequest<Guid>
    {
        public string Ponderation { get; set; }
        public int TotalPoints { get; set; }
        public Guid? ControleId { get; set; }

        internal AllPonderation ToEntity()
        {
            return new AllPonderation
            {
                Ponderation = Ponderation,
                TotalPoints = TotalPoints,
                ControleId = ControleId
            };
        }


    }
}
