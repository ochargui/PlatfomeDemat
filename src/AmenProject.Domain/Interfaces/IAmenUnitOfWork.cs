using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Interfaces
{
    public interface IAmenUnitOfWork : IUnitOfWork
    {
        IAgenceRepository AgenceRepository { get; }
        IAllPonderationRepository AllPonderationRepository { get; }
        IArchiveRepository ArchiveRepository { get; }
        IControleRepository ControleRepository { get; }
        IDataRepository DataRepository { get; }
        IDocBruteRepository DocBruteRepository { get; }
        IGroupeControleRepository GroupeControleRepository { get; }
        IOperateurRepository OperateurRepository { get; }
        IOperationRepository OperationRepository { get; }
        IPacketRepository PacketRepository { get; }
        IPonderateRepository PonderateRepository { get; }
        ITypologiesRepository TypologiesRepository { get; }
        IZoneAgenceRepository ZoneAgenceRepository { get; }
        ILotArchiveRepository LotArchiveRepository { get; }
        IRawDocumentRepository RawDocumentRepository { get; }
        IControlRepository ControlRepository { get; }
    }
}
