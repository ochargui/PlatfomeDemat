using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetArchiveById
{
    public class GetArchiveByIdQuery : IRequest<ArchiveModel>
    {
        public Guid ArchiveId { get; set; }

        public GetArchiveByIdQuery(Guid archiveId)
        {
            ArchiveId = archiveId;
        }
    }
}

