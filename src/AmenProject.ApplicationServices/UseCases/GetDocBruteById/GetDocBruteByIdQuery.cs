using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteById
{
    public class GetDocBruteByIdQuery : IRequest<DocBruteModel>
    {
        public Guid DocBruteId { get; set; }

        public GetDocBruteByIdQuery(Guid docBruteId)
        {
            DocBruteId = docBruteId;
        }
    }
}
