using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllArchives
{
    public class GetAllArchivesQuery : IRequest<IEnumerable<ArchiveModel>>
    {
    }
}
