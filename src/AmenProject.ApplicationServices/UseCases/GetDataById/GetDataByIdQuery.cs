using MediatR;
using DEMAT.Models;
using System;

namespace DEMAT.ApplicationServices.UseCases.GetDataById
{
    public class GetDataByIdQuery : IRequest<DataModel>
    {
        public Guid DataId { get; set; }

        public GetDataByIdQuery(Guid dataId)
        {
            DataId = dataId;
        }
    }
}
