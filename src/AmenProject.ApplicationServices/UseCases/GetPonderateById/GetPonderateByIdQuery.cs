using MediatR;
using DEMAT.Models;
using System;

namespace DEMAT.ApplicationServices.UseCases.GetPonderateById
{
    public class GetPonderateByIdQuery : IRequest<PonderateModel>
    {
        public Guid PonderateId { get; set; }

        public GetPonderateByIdQuery(Guid ponderateId)
        {
            PonderateId = ponderateId;
        }
    }
}
