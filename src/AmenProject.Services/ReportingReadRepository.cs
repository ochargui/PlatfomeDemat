using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Interfaces;
using DEMAT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GrapeCity.Documents.Excel;


namespace DEMAT.Services
{
    public class ReportingReadRepository : ReadOnlyRepository<Archive>, IReportingReadRepository
    {
        private readonly IDematContext _amenBankContext;
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAgenceReadRepository _agenceReadRepository;
        private readonly IOperationReadRepository _operationReadRepository;
        private readonly IDataReadRepository _dataReadRepository;
        private readonly IZoneAgenceReadRepository _zoneAgenceReadRepository;
        private readonly IControleReadRepository _controleReadRepository;
        private readonly IConfiguration _configuration;
        private readonly IPacketReadRepository _packetReadRepository;
        private readonly ILotArchiveReadRepository _lotArchiveReadRepository;

        public ReportingReadRepository(ILotArchiveReadRepository lotArchiveReadRepository, IDematContext amenBankContext, IArchiveReadRepository archiveReadRepository, IAgenceReadRepository agenceReadRepository, IOperationReadRepository operationReadRepository, IDataReadRepository dataReadRepository, IZoneAgenceReadRepository zoneAgenceReadRepository, IControleReadRepository controleReadRepository, IConfiguration configuration, IPacketReadRepository packetReadRepository) : base(amenBankContext)
        {
            _lotArchiveReadRepository = lotArchiveReadRepository;
            _amenBankContext = amenBankContext;
            _archiveReadRepository = archiveReadRepository;
            _agenceReadRepository = agenceReadRepository;
            _operationReadRepository = operationReadRepository;
            _dataReadRepository = dataReadRepository;
            _zoneAgenceReadRepository = zoneAgenceReadRepository;
            _controleReadRepository = controleReadRepository;
            _configuration = configuration;
            _packetReadRepository = packetReadRepository;
        }

        public async Task<IEnumerable<RapportJCModel>> SelectJourneeByDateComptable(DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken)
        {
            DateTimeOffset Start = new DateTimeOffset(DateDebut.DateTime.Date);
            DateTimeOffset End = new DateTimeOffset(DateFin.DateTime.Date).AddDays(1).AddSeconds(-1);
            List<RapportJCModel> result= new List<RapportJCModel>();
            IEnumerable <AgenceModel> agences  = await _agenceReadRepository.GetAllAgences(cancellationToken);
            foreach (AgenceModel agence in agences)
            {
                IEnumerable<ArchiveDetailsModel> list  =
                await (from archive in _amenBankContext.Archive
                       join doc in _amenBankContext.DocBrute on archive.DocBruteId equals doc.Id
                       where (archive.AgenceId == agence.Id)
                        && (archive.CreatedDate >= Start)
                        && (archive.DateFinSaisie <= End)
                        && (archive.Etat == 2 || archive.Etat ==6 || archive.Operation.CodeOperation == 99)
                        orderby (archive.CreatedDate)
                         select new ArchiveDetailsModel()
                         {
                           PathArchive = archive.PathArchive,
                           NomDOc = archive.NomDOc,
                           Commentaire = archive.Commentaire,
                           Etat = archive.Etat,
                           ValideArchive = archive.ValideArchive,
                           AgenceId = archive.AgenceId,
                           OperateurId = archive.OperateurId,
                           DocBruteId = archive.DocBruteId,
                           OperationId = archive.OperationId,
                           DateDebutSaisie = archive.CreatedDate

                         }).ToListAsync(cancellationToken);

       
                     result.Add(new RapportJCModel(agence.CodeAgence,agence.NomAgence, list.Count()));
            }

            return result;
        }


        public async Task<IEnumerable<RapportQuotidienModel>> DataRapportQuotidien(DateTimeOffset DateTraitement, bool journe, CancellationToken cancellationToken)
        {
            int controle_1 = 0;
            int controle_5 = 0;
            int controle_6 = 0;
            int controle_26 = 0;
     
            List<AgenceModel> agences = (List<AgenceModel>)await _agenceReadRepository.GetAgenceByDateEtat(2, DateTraitement, cancellationToken);
            List<RapportQuotidienModel> result = new List<RapportQuotidienModel>();
            List<OperationModel> operations = (List<OperationModel>)await _operationReadRepository.GetAllOperations(cancellationToken);
           

            #region test 
              if (journe == false) { 
                 foreach (AgenceModel ag in agences)
                 {
                   foreach (OperationModel op in operations)
                   {
                       List<ArchiveDetailsModel> archives = (List<ArchiveDetailsModel>)await _archiveReadRepository.GetListArchiveByAgenceEtatDateOperation(ag.Id,2, op.Id, DateTraitement, cancellationToken);
                       foreach (ArchiveDetailsModel a in archives)
                       {
                         
                            if ( (await _dataReadRepository.GetDataByArchiveControle(a.Id, 1, cancellationToken)).Count() !=0 )
                                controle_1++;
                            if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 5, cancellationToken)).Count() != 0)
                                controle_5++;
                            if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 6, cancellationToken)).Count() != 0)
                                controle_6++;
                            if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 26, cancellationToken)).Count() != 0)
                                controle_26++;

                       }
                       if ( archives.Count() != 0  )
                       {   
                            ZoneAgenceModel zone = await _zoneAgenceReadRepository.GetZoneAgenceById((Guid)ag.ZoneAgenceId, cancellationToken);
                           string ZoneAgenceName = zone.ZoneAgenceAdresse;
                            string CodeAgence = ag.CodeAgence.ToString(); ;
                           string Agencename = ag.NomAgence;
                           string OpName = op.OperationName;

                           #region 1 row 
                            string Controlename1 = (await _controleReadRepository.GetControleByCode(1, cancellationToken)).Name;
                            result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename1, controle_1));
                           #endregion  

                           #region 2 row 
                           string Controlename5 = (await _controleReadRepository.GetControleByCode(5, cancellationToken)).Name;
                           result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename5, controle_5));
                           #endregion  

                           #region 3 row 
                          
                           string Controlename6 = (await _controleReadRepository.GetControleByCode(6, cancellationToken)).Name;
                           result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename6,controle_6));
                           #endregion  

                           #region 4 row 
                           
                           string Controlename26 = (await _controleReadRepository.GetControleByCode(26, cancellationToken)).Name;
                           result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename26,controle_26));
                           #endregion

                       }
                   }
                 }
              }
              else if (journe == true )
            {
                List<LotArchiveModel> lot = (List<LotArchiveModel>)await _lotArchiveReadRepository.SelectJourneeByDateFinSaisieEtat(2, DateTraitement, cancellationToken);
                foreach (AgenceModel ag in agences)
                   {
                       foreach (OperationModel op in operations)
                       {

                           foreach (LotArchiveModel l in lot)
                           {
                              List<ArchiveDetailsModel> archives = (List<ArchiveDetailsModel>)await _archiveReadRepository.GetListArchiveByAgenceDateOperationLotArchiveId(ag.Id,  op.Id, DateTraitement,l.Id, cancellationToken);

                               foreach (ArchiveDetailsModel a in archives)
                               {
                                if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 1, cancellationToken)).Count() != 0)
                                    controle_1++;
                                if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 5, cancellationToken)).Count() != 0)
                                    controle_5++;
                                if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 6, cancellationToken)).Count() != 0)
                                    controle_6++;
                                if ((await _dataReadRepository.GetDataByArchiveControle(a.Id, 26, cancellationToken)).Count() != 0)
                                    controle_26++;
                               }   
                               if (archives.Count() != 0)
                               {
                                ZoneAgenceModel zone = await _zoneAgenceReadRepository.GetZoneAgenceById((Guid)ag.ZoneAgenceId, cancellationToken);
                                string ZoneAgenceName = zone.ZoneAgenceAdresse;
                                string CodeAgence = ag.CodeAgence.ToString();
                                string Agencename = ag.NomAgence;
                                string OpName = op.OperationName;

                                       #region 1 row 
                                        string Controlename1 = (await _controleReadRepository.GetControleByCode(1, cancellationToken)).Name;
                                        result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename1, controle_1));
                                        #endregion

                                       #region 2 row 
                                       string Controlename5 = (await _controleReadRepository.GetControleByCode(5, cancellationToken)).Name;
                                       result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename5, controle_5));
                                       #endregion

                                       #region 3 row 
                                       string Controlename6 = (await _controleReadRepository.GetControleByCode(6, cancellationToken)).Name;
                                       result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename6, controle_6));
                                       #endregion

                                       #region 4 row 
                                       string Controlename26 = (await _controleReadRepository.GetControleByCode(26, cancellationToken)).Name;
                                       result.Add(new RapportQuotidienModel(ZoneAgenceName, CodeAgence, Agencename, OpName, Controlename26, controle_26));
                                       #endregion

                               }


                              


                           }
                       }

                   }


               }
            #endregion
            return result;
        }


        public async  Task<IEnumerable<RapportJournalierModel>> DataFichierJournalier(DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken)
        {
            List<RapportJournalierModel> Data = new List<RapportJournalierModel>();
            DateTimeOffset Latest = new DateTimeOffset(DateFin.DateTime.Date).AddDays(1).AddSeconds(-1);
            DateTimeOffset start = new DateTimeOffset(DateDebut.DateTime.Date);
            Boolean _dateDebutAct = start != DateTimeOffset.MinValue;
            Boolean _dateFinAct = Latest != DateTimeOffset.MinValue;
        
            Data = await (from archive in _amenBankContext.Archive
                          where ((!_dateDebutAct && !_dateFinAct) || (archive.CreatedDate >= start) && (archive.DateFinSaisie <= Latest))
                         
                          select new RapportJournalierModel ()
                          {
                              AgenceCode = Convert.ToInt32(archive.Agence.CodeAgence) ,
                              AgenceName = archive.Agence.NomAgence,
                              LotArchiveName = archive.LotArchive.LotArchiveName,
                              DateFinSaisie =archive.DateFinSaisie,
                              OperationName= archive.Operation.OperationName,
                              ArchiveName=archive.NomDOc,
                              Path=archive.PathArchive
                          } ).ToListAsync(cancellationToken);
            return Data;
        }

        public async Task<IEnumerable<RapportFacturationModel>> DataRapportFacturation(DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken)
        {
            RapportFacturationModel obj;
          
            List<RapportFacturationModel> Data = new List<RapportFacturationModel>(); 
            int sommeDocTraite = 0;
            int sommmeDiversRejet = 0;
            IEnumerable<AgenceModel> Agences = (IEnumerable<AgenceModel>)await _agenceReadRepository.GetAllAgences(cancellationToken);
           
               IEnumerable<LotArchiveModel> ListLotArchives = (IEnumerable<LotArchiveModel>)await _lotArchiveReadRepository.GetAllLotArchive(cancellationToken);
                foreach (LotArchiveModel lotArchive in ListLotArchives)
                {   foreach (AgenceModel a in Agences)
                    {
                       IEnumerable<ArchiveDetailsModel> ListArchive = await _archiveReadRepository.GetLotArchiveByLotArchiveIdEtatAgenceId(lotArchive.Id,a.Id, 2,cancellationToken);
                       if (ListArchive.Count() != 0)
                       {
                        ArchiveDetailsModel firstItem = ListArchive.First();
                        var tt = firstItem.LotArchiveId;
                        sommeDocTraite = ((List<ArchiveDetailsModel>)await _archiveReadRepository.ListArchiveByEtatAgenceLotDate((Guid)firstItem.AgenceId, 2, (Guid)firstItem.LotArchiveId, DateDebut, DateFin, cancellationToken)).Count();
                        sommmeDiversRejet = ((List<ArchiveDetailsModel>)await _archiveReadRepository.ListArchiveByEtatAgenceLotArchive((Guid)firstItem.AgenceId, 99, (Guid)firstItem.LotArchiveId, cancellationToken)).Count() +
                                        ((List<ArchiveDetailsModel>)await _archiveReadRepository.ListArchiveByEtatAgenceLotArchive((Guid)firstItem.AgenceId, 6, (Guid)firstItem.LotArchiveId, cancellationToken)).Count();
                        obj = new RapportFacturationModel(a.NomAgence, a.CodeAgence, lotArchive.LotArchiveName, sommeDocTraite, sommmeDiversRejet);
                        Data.Add(obj);
                       }
                    }
                }
            
              
            
            return Data;
        }
    }
}
