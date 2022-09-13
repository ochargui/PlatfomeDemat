using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteById
{
    public class GetDocBruteByIdQueryHandler : IRequestHandler<GetDocBruteByIdQuery, DocBruteModel>
    {
        private readonly IDocBruteReadRepository _docBruteReadRepository;

        public GetDocBruteByIdQueryHandler(IDocBruteReadRepository docBruteReadRepository)
        {
            _docBruteReadRepository = docBruteReadRepository;
        }



        public async Task<DocBruteModel> Handle(GetDocBruteByIdQuery request, CancellationToken cancellationToken)
        {
            
             var r = await _docBruteReadRepository.GetDocBruteById(request.DocBruteId, cancellationToken);

            return r; 


        }

    }
}
