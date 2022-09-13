using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetTypologieById
{
    public class GetTypologieByIdQuery : IRequest<TypologieModel>
    {
        public Guid TypologieId { get; set; }

        public GetTypologieByIdQuery(Guid typologieId)
        {
            TypologieId = typologieId;
        }
    }
}
