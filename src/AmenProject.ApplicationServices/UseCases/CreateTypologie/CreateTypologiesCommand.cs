using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateTypologie
{
    public class CreateTypologiesCommand : IRequest<Guid>
    {
        public String cibleControle { get; set; }
        public String ciblePondaration { get; set; }
        internal Typologies ToEntity()
        {
            return new Typologies
            {
                CibleControle = cibleControle,
                CiblePondaration = ciblePondaration
            };

        }
    }
}
