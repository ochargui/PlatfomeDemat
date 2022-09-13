using DEMAT.Domain.Interfaces;
using DEMAT.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure
{
    public class DematUnitOfWork : UnitOfWork<DematContext>, IAmenUnitOfWork
    {
       private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DematUnitOfWork> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public DematUnitOfWork(IDematContext context, IServiceProvider serviceProvider, ILogger<DematUnitOfWork> logger, IHostEnvironment hostEnvironment)
         : base(context)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IAgenceRepository AgenceRepository
        {
            get { return _agenceRepository ?? (_agenceRepository = _serviceProvider.GetRequiredService<IAgenceRepository>()); }
        }
        private IAgenceRepository _agenceRepository;

        public IAllPonderationRepository AllPonderationRepository
        {
            get { return _allPonderationRepository ?? (_allPonderationRepository = _serviceProvider.GetRequiredService<IAllPonderationRepository>()); }
        }
        private IAllPonderationRepository _allPonderationRepository;

        public IArchiveRepository ArchiveRepository
        {
            get { return _archiveRepository ?? (_archiveRepository = _serviceProvider.GetRequiredService<IArchiveRepository>()); }
        }
        private IArchiveRepository _archiveRepository;

        public IControleRepository ControleRepository
        {
            get { return _controleRepository ?? (_controleRepository = _serviceProvider.GetRequiredService<IControleRepository>()); }
        }
        private IControleRepository _controleRepository;

        public IDataRepository DataRepository
        {
            get { return _dataRepository ?? (_dataRepository = _serviceProvider.GetRequiredService<IDataRepository>()); }
        }
        private IDataRepository _dataRepository;

        public IDocBruteRepository DocBruteRepository
        {
            get { return _docBruteRepository ?? (_docBruteRepository = _serviceProvider.GetRequiredService<IDocBruteRepository>()); }
        }
        private IDocBruteRepository _docBruteRepository;

        public IGroupeControleRepository GroupeControleRepository
        {
            get { return _groupeControleRepository ?? (_groupeControleRepository = _serviceProvider.GetRequiredService<IGroupeControleRepository>()); }
        }
        private IGroupeControleRepository _groupeControleRepository;

        public IOperateurRepository OperateurRepository
        {
            get { return _operateurRepository ?? (_operateurRepository = _serviceProvider.GetRequiredService<IOperateurRepository>()); }
        }
        private IOperateurRepository _operateurRepository;

        public IOperationRepository OperationRepository
        {
            get { return _operationRepository ?? (_operationRepository = _serviceProvider.GetRequiredService<IOperationRepository>()); }
        }
        private IOperationRepository _operationRepository;

        public IPacketRepository PacketRepository
        {
            get { return _packetRepository ?? (_packetRepository = _serviceProvider.GetRequiredService<IPacketRepository>()); }
        }
        private IPacketRepository _packetRepository;

        public IPonderateRepository PonderateRepository
        {
            get { return _ponderateRepository ?? (_ponderateRepository = _serviceProvider.GetRequiredService<IPonderateRepository>()); }
        }
        private IPonderateRepository _ponderateRepository;

        public ITypologiesRepository TypologiesRepository
        {
            get { return _typologiesRepository ?? (_typologiesRepository = _serviceProvider.GetRequiredService<ITypologiesRepository>()); }
        }
        private ITypologiesRepository _typologiesRepository;

        public IZoneAgenceRepository ZoneAgenceRepository
        {
            get { return _zoneAgenceRepository ?? (_zoneAgenceRepository = _serviceProvider.GetRequiredService<IZoneAgenceRepository>()); }
        }

        private IZoneAgenceRepository _zoneAgenceRepository;


        private ILotArchiveRepository _lotArchiveRepository;
        public ILotArchiveRepository LotArchiveRepository 
        {
            get { return  _lotArchiveRepository ?? ( _lotArchiveRepository = _serviceProvider.GetRequiredService<ILotArchiveRepository>()); }
        }

        private IRawDocumentRepository _rawDocumentRepository;
        public IRawDocumentRepository RawDocumentRepository
        {
            get { return _rawDocumentRepository ?? (_rawDocumentRepository = _serviceProvider.GetRequiredService<IRawDocumentRepository>()); }
        }

        private IControlRepository _controlRepository;
        public IControlRepository ControlRepository
        {
            get { return _controlRepository ?? (_controlRepository = _serviceProvider.GetRequiredService<IControlRepository>()); }
        }
    }


}
