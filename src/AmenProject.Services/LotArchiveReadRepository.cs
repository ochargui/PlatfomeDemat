using DEMAT.ApplicationServices.UseCases.CreateLotArchive;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Services
{
    public class LotArchiveReadRepository : ReadOnlyRepository<LotArchive>, ILotArchiveReadRepository
    {

        private readonly IDematContext _amenBankContext;
        private readonly IMediator _mediator;
        private readonly IDocBruteReadRepository _docBruteReadRepository;
        private readonly IPacketReadRepository _packetReadRepository;
        private IConfiguration Configuration;

        public LotArchiveReadRepository(IDematContext amenBankContext, IMediator mediator, IDocBruteReadRepository docBruteReadRepository, IPacketReadRepository packetReadRepository, IConfiguration configuration) : base(amenBankContext)
        {
            _amenBankContext = amenBankContext;
            _mediator = mediator;
            _docBruteReadRepository = docBruteReadRepository;
            _packetReadRepository = packetReadRepository;
            Configuration = configuration;
        }

        public async Task<Guid> CreateLotArchiveExist(Guid DocBruteId, CancellationToken cancellationToken)
        {
            //Test if lotArchiveId Exixt or not 
            DocBruteModel doc = await _docBruteReadRepository.GetDocBruteById(DocBruteId, cancellationToken);
            PacketModel packet = await _packetReadRepository.GetPacketById(doc.LotPacketId, cancellationToken);
            var packetNAmeSub = packet.NomPAcket.Substring(0, 6);
            IEnumerable<LotArchiveModel> Lots = await GetAllLotArchive(cancellationToken);
            bool Exist = false;
            Guid result = Guid.Empty;
            foreach (LotArchiveModel l in Lots)
            {
                if (l.LotArchiveName.Equals(packetNAmeSub) == true)
                {
                    result = l.Id;
                    Exist = true;
                
                }
            }
            if (Exist == false)
            {
                result = await _mediator.Send(new CreateLotArchiveCommand
                 {

                    Name = packetNAmeSub,

                });
               
            }
         
            return result;
        }

        public async Task<string> CreateOutOutOcrOperationDiercotries(string journee,int AgenceCode,CancellationToken cancellationToken)
        {
            string BackupOCR = Configuration["Dir:BackupOCR"];
            string OutputOCR = Configuration["Dir:OutputOCR"];
            string path = OutputOCR + "\\" + journee + "\\" + AgenceCode;
            string BackupPath = BackupOCR + "\\" + journee + "\\" + AgenceCode;
       
            Directory.CreateDirectory(path + "\\" + 1);
            Directory.CreateDirectory(path + "\\" + 2);
            Directory.CreateDirectory(path + "\\" + 3);
            Directory.CreateDirectory(path + "\\" + 6);
            Directory.CreateDirectory(path + "\\" + 9);
            Directory.CreateDirectory(path + "\\" + 11);

            Directory.CreateDirectory(BackupPath + "\\" + 1);
            Directory.CreateDirectory(BackupPath + "\\" + 2);
            Directory.CreateDirectory(BackupPath + "\\" + 3);
            Directory.CreateDirectory(BackupPath + "\\" + 6);
            Directory.CreateDirectory(BackupPath + "\\" + 9);
            Directory.CreateDirectory(BackupPath + "\\" + 11);

            return path ;
        }

        public async Task<IEnumerable<LotArchiveModel>> GetAllLotArchive(CancellationToken cancellationToken)
        {

            IEnumerable<LotArchiveModel> list = await _amenBankContext.LotArchive
                .AsNoTracking()
                .Select(a => new LotArchiveModel()
                {
                    Id = (Guid)a.Id,
                    CodeLotArchive = a.CodeLotArchive,
                    LotArchiveName = a.LotArchiveName,
                    DateFinSaisie = a.DateFinSaisie
                })
                .Distinct()
                .ToListAsync(cancellationToken);

            return list;


        }

        public async Task<LotArchiveModel> GetLotArchiveById(Guid LotArchiveId, CancellationToken cancellationToken)
        {
            return await _amenBankContext.LotArchive
                    .Where(x => x.Id == LotArchiveId)
                    .AsNoTracking()
                    .Select(x => new LotArchiveModel()
                    {
                        Id = x.Id,
                        LotArchiveName = x.LotArchiveName,
                        CodeLotArchive = x.CodeLotArchive,
                        DateDebutSaisie = x.CreatedDate,
                        DateFinSaisie = x.DateFinSaisie
,
                       
                    })
                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<LotArchiveModel> GetLotArchiveByName(string LotName, CancellationToken cancellationToken)
        {
            return await _amenBankContext.LotArchive
                    .Where(x => x.LotArchiveName.Equals(LotName))
                    .AsNoTracking()
                    .Select(x => new LotArchiveModel()
                    { 
                        Id = x.Id,
                        LotArchiveName = x.LotArchiveName,
                        CodeLotArchive = x.CodeLotArchive,
                        DateDebutSaisie = x.CreatedDate,
                        DateFinSaisie = x.DateFinSaisie
,

                    })
                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<LotArchiveModel>> SelectJourneeByDateFinSaisieEtat(int Etat, DateTimeOffset DateTraitement, CancellationToken cancellationToken)
        {
            DateTimeOffset start = new DateTimeOffset(DateTraitement.DateTime.Date);
            DateTimeOffset Latest = new DateTimeOffset(DateTraitement.DateTime.Date).AddDays(1).AddSeconds(-1);
             var d =  start.Date;

            List<LotArchiveModel> l =   await  (from a in _amenBankContext.Archive
                                               //join lot in _amenBankContext.LotArchive on a.LotArchiveId equals lot.Id
                                                where(a.Etat == Etat)
                                                && (a.CreatedDate >= start)
                                                 && (a.DateFinSaisie <= Latest)
                                                select new LotArchiveModel()
                                                {
                                                             Id = (Guid)a.LotArchiveId,
                                                             CodeLotArchive = a.LotArchive.CodeLotArchive,
                                                             LotArchiveName = a.LotArchive.LotArchiveName,
                                                             DateFinSaisie = a.LotArchive.DateFinSaisie


                                                }).ToListAsync(cancellationToken);
            //new List<LotArchiveModel>();



            return l; 
        }
    
        
    }
}
