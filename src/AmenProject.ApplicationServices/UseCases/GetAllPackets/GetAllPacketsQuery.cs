using MediatR;
using DEMAT.Models;
using System;
using System.Collections.Generic;

namespace DEMAT.ApplicationServices.UseCases.GetAllPackets
{
    public class GetAllPacketsQuery : IRequest<IEnumerable<PacketModel>>
    {
    }
}
