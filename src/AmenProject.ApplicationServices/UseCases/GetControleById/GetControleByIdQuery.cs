using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetControlById
{
    public class GetControleByIdQuery : IRequest<ControleModel>
    {
        public Guid ControleId { get; set; }

        public GetControleByIdQuery(Guid controleId)
        {
            ControleId = controleId;
        }
    }
}

