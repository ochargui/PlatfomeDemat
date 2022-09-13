using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateControle
{
    public class CreateControleCommand : IRequest<Guid>
    {
        public String Nom { get; set; }
        public int AnomalieCode { get; set; }
        public int CodeC{ get; set; }
        public Guid? IDGroupeControle { get; set; }
        
        
        internal Controle ToEntity()
        {
            return new Controle
            {
                Name = Nom,
                CodeAnomalie = AnomalieCode,
                GroupeControleId = IDGroupeControle,
                CodeControle = CodeC

            };
        }
    }
}
