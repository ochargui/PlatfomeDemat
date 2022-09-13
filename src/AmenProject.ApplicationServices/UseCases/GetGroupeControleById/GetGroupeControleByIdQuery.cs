using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetGroupeControleById
{
    public class GetGroupeControleByIdQuery : IRequest<GroupeControleModel>
    {
        public Guid GroupeControleID { get; set; }

        public GetGroupeControleByIdQuery(Guid groupeControleId)
        {
            GroupeControleID = groupeControleId;
        }
    }
}
