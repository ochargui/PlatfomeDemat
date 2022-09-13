using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAgenceById
{
    public class GetAgenceByIdQuery : IRequest<AgenceModel>
    {
        public Guid AgenceId { get; set; }

        public GetAgenceByIdQuery(Guid agenceId)
        {
            AgenceId = agenceId;
        }
   
    }
}
