using MediatR;
using DEMAT.Models;
using System;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderationsById
{
    public class GetAllPonderationsByIdQuery : IRequest<AllPonderationModel>
    {
        public Guid AllPonderationId { get; set; }

        public GetAllPonderationsByIdQuery(Guid allPonderationId)
        {
            AllPonderationId = allPonderationId;
        }
    }
}
