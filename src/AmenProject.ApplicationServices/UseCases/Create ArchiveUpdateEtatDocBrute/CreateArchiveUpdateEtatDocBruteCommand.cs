using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateArchiveUpdateEtatDocBrute
{
    public class CreateArchiveUpdateEtatDocBruteCommand  :IRequest<string>
    {
        public Guid PacketId { get; set; }

        public CreateArchiveUpdateEtatDocBruteCommand(Guid packetId)
        {
            PacketId = packetId;
        }
    }
}
