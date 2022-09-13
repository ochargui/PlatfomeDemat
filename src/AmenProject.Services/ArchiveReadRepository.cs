using DEMAT.ApplicationServices.UseCases.CreateArchive;
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
	public class ArchiveReadRepository : ReadOnlyRepository<Archive>, IArchiveReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		private readonly IDocBruteReadRepository _docBruteReadRepository;
		private readonly IPacketReadRepository _packetReadRepository;
		private readonly IMediator _mediator;
		private readonly IOperationReadRepository _operationReadRepository;
		private readonly ILotArchiveReadRepository _lotArchiveRepository;

		private readonly IAgenceReadRepository _agenceReadRepository;
		private IConfiguration Configuration;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ArchiveReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public ArchiveReadRepository(IDematContext amenBankContext, IConfiguration configuration,IAgenceReadRepository agenceReadRepository ,  IMediator mediator, IDocBruteReadRepository docBruteReadRepository, IPacketReadRepository packetReadRepository, IOperationReadRepository operationReadRepository, ILotArchiveReadRepository lotArchiveRepository) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
			_mediator = mediator;
			Configuration = configuration;
			_docBruteReadRepository = docBruteReadRepository;
			_packetReadRepository = packetReadRepository;
			_operationReadRepository = operationReadRepository;
			_lotArchiveRepository = lotArchiveRepository;
			_agenceReadRepository = agenceReadRepository;
		}


		#endregion

		#region Methods

		public async Task<IEnumerable<ArchiveModel>> GetAllArchives(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Archive
					.AsNoTracking()
					.Select(x => new ArchiveModel()
					{
						Id = x.Id,
						PathArchive = x.PathArchive,
						NomDOc = x.NomDOc,
						Commentaire = x.Commentaire,
						Etat = x.Etat,
						ValideArchive = x.ValideArchive,
						AgenceId = x.AgenceId,
						OperateurId = x.OperateurId,
						DocBruteId = x.DocBruteId,
						LotArchiveId = x.LotArchiveId,
						OperationId = x.OperationId,
						DateDebutSaisie = x.CreatedDate,
						DateFinTypage = x.DateFinSaisie

					})
					.ToListAsync(cancellationToken);
		}

        public async Task<ArchiveModel> GetArchiveById(Guid archiveId, CancellationToken cancellationToken)
        {

			return await _amenBankContext.Archive
				    .Where(x => x.Id == archiveId)
					.AsNoTracking()
					.Select(x => new ArchiveModel()
					{
						Id = x.Id,
						PathArchive = x.PathArchive,
						NomDOc = x.NomDOc,
						Commentaire = x.Commentaire,
						Etat = x.Etat,
						ValideArchive = x.ValideArchive,
						AgenceId = x.AgenceId,
						OperateurId = x.OperateurId,
						DocBruteId = x.DocBruteId,
						LotArchiveId = x.LotArchiveId,
						OperationId = x.OperationId,
						DateDebutSaisie = x.CreatedDate,
						DateFinTypage = x.DateFinSaisie
					})
				   .FirstOrDefaultAsync(cancellationToken);
		}

        public async Task<string> InsertArchiveUpdateEtatPacket(Guid PacketId, CancellationToken cancellationToken)
        {
			 PacketModel packet = await _packetReadRepository.GetPacketById(PacketId, cancellationToken);
			var packetNAme = packet.NomPAcket;
			var packetNAmeSub = packet.NomPAcket.Substring(0,6);
			IEnumerable <DocBrute> Documents = await _amenBankContext.DocBrute
											 .Include(x => x.LotPacket)
											 .Where(x => x.LotPacketId == packet.Id)
											 .ToListAsync(cancellationToken);


			 foreach (DocBrute Document in Documents)
			 {
				Document.Etat =1;
				await _mediator.Send( new CreateArchiveCommand
                {   Path= Document.Commentaire,
					ArchiveNomDOc = Document.NomDoc,
					ArchiveCommentaire=" ",
					EtatArchive=0,
					Valide=0,
					IdAgence =null,
					IdOperateur = null,
					IdOperation = null,
					IdDocBrute = Document.Id,
					dateDebutSaisie = DateTimeOffset.Now,

				});
				
			 }
             return await Task.FromResult("files Archived !! ");
		}

    
		public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByIdAgence(Guid agenceId, CancellationToken cancellationToken)
        {
			return await _amenBankContext.Archive
					.Where(x => x.AgenceId == agenceId)
					.OrderBy(x => x.CreatedDate)
					.AsNoTracking()
					.Select(x => new ArchiveDetailsModel()
					{
						Id = x.Id,
						PathArchive = x.PathArchive,
						NomDOc = x.NomDOc,
						Commentaire = x.Commentaire,
						Etat = x.Etat,
						ValideArchive = x.ValideArchive,
						AgenceId = x.AgenceId,
						OperateurId = x.OperateurId,
						DocBruteId = x.DocBruteId,
						OperationId = x.OperationId,
						DateDebutSaisie = x.CreatedDate,
						DateFinTypage = x.DateFinSaisie
					})
				   .ToListAsync(cancellationToken);
		}

        public async  Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceOperation(Guid agenceId,Guid opearationId, CancellationToken cancellationToken)
        {
			 return await _amenBankContext.Archive
				.Where (x => x.OperationId == opearationId && x.AgenceId == agenceId)
				.OrderBy(x => x.CreatedDate)
				.AsNoTracking()
				.Select(x => new ArchiveDetailsModel()
				{
					Id = x.Id,
					PathArchive = x.PathArchive,
					NomDOc = x.NomDOc,
					Commentaire = x.Commentaire,
					Etat = x.Etat,
					ValideArchive = x.ValideArchive,
					AgenceId = x.AgenceId,
					OperateurId = x.OperateurId,
					DocBruteId = x.DocBruteId,
					OperationId = x.OperationId,
					DateDebutSaisie = x.DocBrute.CreatedDate

				})
			   .ToListAsync(cancellationToken);


		}

		public async Task<IEnumerable<DocumentsModel>> ListDocumentsAgence(IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List < DocumentsModel> Result = new List<DocumentsModel>() ;
            DocumentsModel row = new DocumentsModel();
            foreach (AgenceModel agence in agences)
            {
                int Nbvalide = 0;
                int NbNonV = 0;

                IEnumerable<ArchiveDetailsModel> archives = await GetListArchiveByIdAgence(agence.Id, cancellationToken);

                foreach (ArchiveDetailsModel archive in archives)
                {
                    if (archive.ValideArchive == 0)
                    {
                        NbNonV++;
                    }
                    else
                        Nbvalide++;
                }
				if (archives.Count() != 0)
				{
					Guid lastElementID = archives.LastOrDefault().Id;
					Archive element = _amenBankContext.Archive.Find(lastElementID);
					string nomDoc = element.NomDOc;
					DateTimeOffset Journe = element.CreatedDate;
					string AgenceName = agence.NomAgence;
					int NombreDocumentsTotale = archives.Count(); ;
					int NombreDocumentsValide = Nbvalide;
					int NombreDocumentsInValide = NbNonV;
					Result.Add(new DocumentsModel(Journe.ToString(), AgenceName, NombreDocumentsTotale, NombreDocumentsValide, NombreDocumentsInValide));
				}
				else
					Result.Add(new DocumentsModel(DateTimeOffset.Now.ToString(), agence.NomAgence, 0, 0, 0));

			}
			return Result;
		}

        public async  Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceOperation(Guid IdOperation, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List<DocumentsModel> Result = new List<DocumentsModel>();
			DocumentsModel row = new DocumentsModel();

			foreach (AgenceModel agence in agences)
			{
				int Nbvalide = 0;
				int NbNonV = 0;

				IEnumerable<ArchiveDetailsModel> archives = await GetListArchiveByAgenceOperation(agence.Id, IdOperation, cancellationToken);
				if (archives.Count() != 0)
				{
					foreach (ArchiveDetailsModel archive in archives)
				    {
					if (archive.ValideArchive == 0)
					{
						NbNonV++;
					}
					else
						Nbvalide++;
				    }

				Guid lastElementID = archives.LastOrDefault().Id;
				Archive element = _amenBankContext.Archive.Find(lastElementID);
				string nomDoc = element.NomDOc;
				DateTimeOffset Journe = element.CreatedDate;
				string AgenceName = agence.NomAgence;
				int NombreDocumentsTotale = archives.Count(); ;
				int NombreDocumentsValide = Nbvalide;
				int NombreDocumentsInValide = NbNonV;
				string JourneFinal = NomJourneTraitement(Journe);
				Result.Add(new DocumentsModel(JourneFinal, AgenceName, NombreDocumentsTotale, NombreDocumentsValide, NombreDocumentsInValide));
			}
		}
			return Result;
		}

		public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDate(Guid Id , DateTimeOffset DateDebut, DateTimeOffset dateFin, CancellationToken cancellationToken)
		{
			DateTimeOffset Earliest = new DateTimeOffset(DateDebut.DateTime.Date);
			DateTimeOffset Latest = new DateTimeOffset(dateFin.DateTime.Date).AddDays(1).AddSeconds(-1);

			IEnumerable<ArchiveDetailsModel> Result =  
				await    (from archive in _amenBankContext.Archive
						  where (archive.AgenceId == Id) 
						  && (archive.CreatedDate >= Earliest)
						  && ( archive.DateFinSaisie <= Latest)
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
							  DateDebutSaisie = archive.CreatedDate,
							  DateFinTypage = archive.DateFinSaisie

						  })
						  .ToListAsync(cancellationToken);
			return Result;

		}

        public async  Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceDate(DateTimeOffset DateDebut, DateTimeOffset dateFin, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List<DocumentsModel> Result = new List<DocumentsModel>();
			foreach (AgenceModel agence in agences)
			{
				int Nbvalide = 0;
				int NbNonV = 0;
				IEnumerable<ArchiveDetailsModel> archivesdateeee = await GetListArchiveByAgenceDate(agence.Id, DateDebut, dateFin, cancellationToken);
				if (archivesdateeee.Count() != 0)
				{
					foreach (ArchiveDetailsModel archive in archivesdateeee)
				    {
					   if (archive.ValideArchive == 0)
					   { 
						NbNonV++; 
					   } 
					   else 
					   { 
					   Nbvalide++;  
					   }
					}
					  DateTimeOffset Journe = archivesdateeee.LastOrDefault().DateDebutSaisie; ;
				      string AgenceName = agence.NomAgence;
				      int NombreDocumentsTotale = archivesdateeee.Count(); ;
				      int NombreDocumentsValide = Nbvalide;
				      int NombreDocumentsInValide = NbNonV;
				      string JourneFinal = NomJourneTraitement(Journe);
					  Result.Add(new DocumentsModel(JourneFinal, AgenceName, NombreDocumentsTotale, 
					                         NombreDocumentsValide, NombreDocumentsInValide));
					
				}
			}
			return Result;
		}

        public async  Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDateOperation(Guid agenceId, Guid opearationId, DateTimeOffset DateDebut, DateTimeOffset dateFin, CancellationToken cancellationToken)
        {
		
			Boolean _operationid = opearationId != Guid.Empty;
			Boolean _dateDebut = DateDebut != DateTimeOffset.MinValue;
			Boolean _dateFin = dateFin != DateTimeOffset.MaxValue;
			DateTimeOffset StartDate = new DateTimeOffset(DateDebut.DateTime.Date);
			DateTimeOffset EndDate = new DateTimeOffset(dateFin.DateTime.Date).AddDays(1).AddSeconds(-1);
			IEnumerable<ArchiveDetailsModel> Result =
				await(from archive in _amenBankContext.Archive
					  where (archive.AgenceId == agenceId)
					  && (!_operationid || archive.OperationId == opearationId)
						  && ((!_dateDebut && !_dateFin) || (archive.CreatedDate >= StartDate && archive.DateFinSaisie <= EndDate ))
		
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
						  DateDebutSaisie = archive.CreatedDate,
						  DateFinTypage = archive.DateFinSaisie

					  })
						  .ToListAsync(cancellationToken);
			return Result;
		}

        public async Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceDateOperation(DateTimeOffset DateDebut, DateTimeOffset DateFin, Guid opearationId, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List<DocumentsModel> Result = new List<DocumentsModel>();
			foreach (AgenceModel agence in agences)
			{
				int Nbvalide = 0;
				int NbNonV = 0;
				
				IEnumerable<ArchiveDetailsModel> archivesdateeee = await GetListArchiveByAgenceDateOperation(agence.Id, opearationId ,DateDebut, DateFin, cancellationToken);
				if (archivesdateeee.Count() != 0)
				{
					foreach (ArchiveDetailsModel archive in archivesdateeee)
				    {
					if (archive.ValideArchive == 0)
					{
						NbNonV++;
					}
					else
					{
						Nbvalide++;
					}
					}

					DateTimeOffset Journe = archivesdateeee.LastOrDefault().DateDebutSaisie; ;
				    string AgenceName = agence.NomAgence;
				    int NombreDocumentsTotale = archivesdateeee.Count(); ;
				    int NombreDocumentsValide = Nbvalide;
				    int NombreDocumentsInValide = NbNonV;
				    string JourneFinal = NomJourneTraitement(Journe);
					Result.Add(new DocumentsModel(JourneFinal, AgenceName, NombreDocumentsTotale,
											 NombreDocumentsValide, NombreDocumentsInValide));
				    
				}
			}
			return Result;
		}

		public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveEtatCodeOperation(int Etat, CancellationToken cancellationToken)
		{
			IEnumerable<ArchiveDetailsModel> List = await(from archive in _amenBankContext.Archive
														  where (archive.Etat == Etat)
														  && (archive.Operation.CodeOperation  != 99 )  													
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
															  DateDebutSaisie = archive.CreatedDate,
															  DateFinTypage = archive.DateFinSaisie

														  })
													 .ToListAsync(cancellationToken);
			return List;
		} 
		
		public async Task<IEnumerable<ArchiveModel>> ListArchiveByEtatJourne(int Etat, string Journe, CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveModel> List = await (from archive in _amenBankContext.Archive
													join op in _amenBankContext.Operation on archive.OperationId equals op.Id
												 	 where (archive.Etat == Etat)
													&& (archive.Operation.CodeOperation != 99)
												    && (archive.DocBrute.LotPacket.NomPAcket.StartsWith(Journe))
													select new ArchiveModel()
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
														Datecreation = archive.CreatedDate,
														DateFinTypage = archive.DateFinSaisie

													}).
													ToListAsync(cancellationToken);
			return List;
		}

        public  async Task<IEnumerable<JourneOperationModel>> ListArchiveJourne(CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveDetailsModel> List = await GetListArchiveEtatCodeOperation(0, cancellationToken);
			List <JourneOperationModel> ListeJourne =  new List<JourneOperationModel>();
			
			foreach (ArchiveDetailsModel l in List)
			{
				Guid idDocBrute = (Guid)l.DocBruteId;
				PacketModel packet = await _packetReadRepository.GetPacketByIdDocBrute(idDocBrute, cancellationToken);
				string Journe = packet.NomPAcket.Substring(0, 6);

				IEnumerable<ArchiveModel> res = await (from archive in _amenBankContext.Archive
													   join d in _amenBankContext.DocBrute on archive.DocBruteId equals d.Id
			                                           join p in _amenBankContext.LotPacket on d.LotPacketId equals p.Id
			                                           where (p.NomPAcket.StartsWith(Journe)) 
				                                       select new ArchiveModel()
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
					Datecreation = archive.CreatedDate

				})
				                                       .ToListAsync(cancellationToken);

				IEnumerable<ArchiveModel> EnAttente = await ListArchiveByEtatJourne(0, Journe, cancellationToken);
				IEnumerable<ArchiveModel> EnCours = await ListArchiveByEtatJourne(1, Journe, cancellationToken);
				IEnumerable<ArchiveModel> Saisie = await ListArchiveByEtatJourne(2, Journe, cancellationToken);
				string JourneFinal = packageNameToDate(Journe);
				JourneOperationModel row = new JourneOperationModel(JourneFinal, EnAttente.Count(), res.Count(), Saisie.Count());
				if (ListeJourne.Exists(x => x.Journe == row.Journe) == false)
				{
					ListeJourne.Add(row);
				}
				

			}
			return ListeJourne;
		}
		public void UpdateJournee(IEnumerable<JourneOperationModel> List, JourneOperationModel row)
		{
            foreach (JourneOperationModel l in List)
            {
				if(l.Journe.Equals(row.Journe)== false)
                {
					l.EnAttente = +row.EnAttente;
					l.EnAttente = +row.totale;
					l.Saisie =+ row.Saisie;
				}
            }
        }
		public string  NomJourneTraitement(DateTimeOffset Journe)
		{
			string m,d;
			if (Journe.Day < 10 )
			{d = "0" + Journe.Day;}else d =  Journe.Day.ToString();
			if (Journe.Month < 10 )
			{m = "0" + Journe.Month;} else m = Journe.Month.ToString();
			string Result = d+"-"+ m + "-" + Journe.Year.ToString();
			return Result;
		}
		public string packageNameToDate(string Journe)
		{
			string Result = Journe.Substring(0,2)
			       +"-"+ 
				   Journe.Substring(2, 2)
				   + "-" +
				   Journe.Substring(4, 2);
			return Result;
		} 

		public async  Task<IEnumerable<DocumentsEnAttenteModel>> ListArchiveByEtatAgence(int Etat, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List <DocumentsEnAttenteModel> Result = new List<DocumentsEnAttenteModel>(); 
			foreach (AgenceModel agence in agences)
			{
				IEnumerable<ArchiveModel> List = await (from archive in _amenBankContext.Archive
														where (archive.Etat == Etat)
														&& (archive.AgenceId == agence.Id)
														select new ArchiveModel()
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
															Datecreation = archive.CreatedDate

														})
														.ToListAsync(cancellationToken);
				int NbDocEnAttente = List.Count();
				DocumentsEnAttenteModel row = new DocumentsEnAttenteModel();
				row.idAgence = agence.Id;
				row.CodeAgence = agence.CodeAgence;
				row.NomAgence = agence.NomAgence;
				row.DocEnAttente = NbDocEnAttente;
				Result.Add(row);




			}
			
			return Result;
		}

        public async  Task<IEnumerable<ArchiveModel>> GetListArchiveEtatOperationId(int Etat, Guid IdOperation, CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveModel> List = await(from archive in _amenBankContext.Archive
														  where (archive.Etat == Etat)
														  && (archive.OperationId  == IdOperation)  													
														  select new ArchiveModel()
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
															  DateDebutSaisie = archive.CreatedDate,
															  DateFinTypage = archive.DateFinSaisie

														  })
										 .ToListAsync(cancellationToken);
			return List;
		}

    
        public async Task<IEnumerable<ArchiveOperation>> GetListArchiveOperation(int Etat, CancellationToken cancellationToken)
        {
			List<ArchiveOperation> Result = new List<ArchiveOperation>();
			IEnumerable <OperationModel> operations = await _operationReadRepository.GetAllOperations(cancellationToken);
			foreach (OperationModel operation in operations)
			{
				ArchiveOperation row = new ArchiveOperation();
				row.Operation  = operation.OperationName;
				//row.CodeOperation = operation.OperationName;
				IEnumerable<ArchiveModel>  ListDocuments = await GetListArchiveEtatOperationId(2, operation.Id, cancellationToken);
				row.NbDocuments  = ListDocuments.Count();
				Result.Add(row);

			}
         return Result;
		}

		public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveOperatorDate(DateTimeOffset DateDebut, DateTimeOffset DateFin, Guid OpearatorId, int Etat, CancellationToken cancellationToken)
		{
			DateTimeOffset Earliest = new DateTimeOffset(DateDebut.DateTime.Date);
			DateTimeOffset Latest = new DateTimeOffset(DateFin.DateTime.Date).AddDays(1).AddSeconds(-1);
			//from archive in _amenBankContext.Archive  where (doc.DateDebutTypage>= Earliest)
			// && (doc.DateFinTypage <= Latest)
			IEnumerable<ArchiveDetailsModel> Result = await (from a in _amenBankContext.Archive
															 join doc in _amenBankContext.DocBrute
															 on a.DocBruteId equals doc.Id
															 where (a.Etat == Etat)
															 && (a.OperateurId == OpearatorId)
															 && (a.CreatedDate >= Earliest)
															 && (a.DateFinSaisie <= Latest)
															 select new ArchiveDetailsModel()
															 {
																 PathArchive = a.PathArchive,
																 NomDOc = a.NomDOc,
																 Commentaire = a.Commentaire,
																 Etat = a.Etat,
																 ValideArchive = a.ValideArchive,
																 AgenceId = a.AgenceId,
																 OperateurId = a.OperateurId,
																 DocBruteId = a.DocBruteId,
																 OperationId = a.OperationId,
																 DateDebutSaisie = a.CreatedDate,
																 DateFinTypage = a.DateFinSaisie

															 }).ToListAsync(cancellationToken);




			return Result;
		}

        public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceEtatDateOperation(Guid agenceId, int Etat, Guid opearationId, DateTimeOffset DateFinTypage, CancellationToken cancellationToken)
        {
			
			DateTimeOffset Latest = new DateTimeOffset(DateFinTypage.DateTime.Date).AddDays(1).AddSeconds(-1);
			DateTimeOffset start = new DateTimeOffset(DateFinTypage.DateTime.Date);
			IEnumerable<ArchiveDetailsModel> l = await _amenBankContext.Archive
			   .Where(x => x.OperationId == opearationId && x.AgenceId == agenceId)
			   .Where(x=> x.Etat== Etat && x.Etat != 99)
			   .Where(x=> x.CreatedDate >= start)
			   .Where(x => x.DateFinSaisie <= Latest)
			   .OrderBy(x => x.CreatedDate)
			   .AsNoTracking()
			   .Select(x => new ArchiveDetailsModel()
			   {
				   Id = x.Id,
				   PathArchive = x.PathArchive,
				   NomDOc = x.NomDOc,
				   Commentaire = x.Commentaire,
				   Etat = x.Etat,
				   ValideArchive = x.ValideArchive,
				   AgenceId = x.AgenceId,
				   OperateurId = x.OperateurId,
				   DocBruteId = x.DocBruteId,
				   OperationId = x.OperationId,
				   DateDebutSaisie = x.CreatedDate,
				   DateFinTypage = x.DateFinSaisie

			   })
			  .ToListAsync(cancellationToken);
			return l ;
			
		}

        public async Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDateOperationLotArchiveId(Guid agenceId, Guid opearationId, DateTimeOffset DateTraitement,  Guid LotArchiveId, CancellationToken cancellationToken)
        {
			
			DateTimeOffset Latest = new DateTimeOffset(DateTraitement.DateTime.Date).AddDays(1).AddSeconds(-1);
			DateTimeOffset start = new DateTimeOffset(DateTraitement.DateTime.Date);
		
			IEnumerable<ArchiveDetailsModel> Result =
				await (from archive in _amenBankContext.Archive
					   where (archive.AgenceId == agenceId)
					   && (archive.OperationId == opearationId)
					  && (archive.CreatedDate >= start && archive.CreatedDate <= Latest)
					  && (archive.LotArchiveId == LotArchiveId)
					   orderby (archive.CreatedDate)
					   select new ArchiveDetailsModel()
					   {
						   Id = archive.Id,
						   PathArchive = archive.PathArchive,
						   NomDOc = archive.NomDOc,
						   Commentaire = archive.Commentaire,
						   Etat = archive.Etat,
						   ValideArchive = archive.ValideArchive,
						   AgenceId = archive.AgenceId,
						   OperateurId = archive.OperateurId,
						   DocBruteId = archive.DocBruteId,
						   OperationId = archive.OperationId,
						   LotArchiveId = archive.LotArchiveId,
						   DateDebutSaisie = archive.CreatedDate,
						   DateFinTypage = archive.DateFinSaisie
					   }).ToListAsync(cancellationToken);
			return Result;
		}

        public async Task<IEnumerable<ArchiveDetailsModel>> GetArchiveByDateFinSaisieEtatAgence(Guid agenceId, int Etat, DateTimeOffset DateDebutTypage, DateTimeOffset DateFinTypage, CancellationToken cancellationToken)
        {
			DateTimeOffset Latest = new DateTimeOffset(DateFinTypage.DateTime.Date).AddDays(1).AddSeconds(-1);
			DateTimeOffset start = new DateTimeOffset(DateDebutTypage.DateTime.Date);
			IEnumerable<ArchiveDetailsModel> l =  await (from archive in _amenBankContext.Archive
														where (archive.Etat == Etat) 
														&& (archive.Etat != 99)
														&& (archive.AgenceId == agenceId )
														&& (archive.CreatedDate >= start)
														&& (archive.CreatedDate >= start)
														&& (archive.DateFinSaisie <= Latest)
														select new ArchiveDetailsModel()
														{   Id = archive.Id,
															PathArchive = archive.PathArchive,
															NomDOc = archive.NomDOc,
															Commentaire = archive.Commentaire,
															Etat = archive.Etat,
															ValideArchive = archive.ValideArchive,
															AgenceId = archive.AgenceId,
															OperateurId = archive.OperateurId,
															DocBruteId = archive.DocBruteId,
															LotArchiveId = archive.LotArchiveId,
															OperationId = archive.OperationId,
															DateDebutSaisie = archive.CreatedDate,
															DateFinTypage = archive.DateFinSaisie

														}).ToListAsync(cancellationToken);
			  
			return l;
		}

        public async Task<IEnumerable<ArchiveDetailsModel>> ListArchiveByEtatAgenceLotDate(Guid agenceId, int Etat, Guid LotArchiveId, DateTimeOffset DateDebutTypage, DateTimeOffset DateFinTypage, CancellationToken cancellationToken)
        {
			DateTimeOffset Latest = new DateTimeOffset(DateFinTypage.DateTime.Date).AddDays(1).AddSeconds(-1);
			DateTimeOffset start = new DateTimeOffset(DateDebutTypage.DateTime.Date);
			IEnumerable<ArchiveDetailsModel> l = await _amenBankContext.Archive
			   .Where(x => x.AgenceId == agenceId)
			   .Where(x => x.LotArchiveId == LotArchiveId)
			   .Where(x => x.Etat == Etat )
			   .Where(x => x.CreatedDate >= start)
			   .Where(x => x.DateFinSaisie <= Latest)
			   .OrderBy(x => x.CreatedDate)
			   .AsNoTracking()
			   .Select(x => new ArchiveDetailsModel()
			   {
				   Id = x.Id,
				   PathArchive = x.PathArchive,
				   NomDOc = x.NomDOc,
				   Commentaire = x.Commentaire,
				   Etat = x.Etat,
				   ValideArchive = x.ValideArchive,
				   AgenceId = x.AgenceId,
				   OperateurId = x.OperateurId,
				   DocBruteId = x.DocBruteId,
				   OperationId = x.OperationId,
				   DateDebutSaisie = x.CreatedDate,
				   DateFinTypage = x.DateFinSaisie

			   })
			  .ToListAsync(cancellationToken);
			return l;
		}

        public async Task<IEnumerable<ArchiveDetailsModel>> ListArchiveByEtatAgenceLotArchive(Guid agenceId, int Etat, Guid LotArchiveId, CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveDetailsModel> l = await _amenBankContext.Archive
		   .Where(x => x.AgenceId == agenceId)
		   .Where(x => x.LotArchiveId == LotArchiveId)
		   .Where(x => x.Etat == Etat)
		   .OrderBy(x => x.CreatedDate)
		   .AsNoTracking()
		   .Select(x => new ArchiveDetailsModel()
		   {
			   Id = x.Id,
			   PathArchive = x.PathArchive,
			   NomDOc = x.NomDOc,
			   Commentaire = x.Commentaire,
			   Etat = x.Etat,
			   ValideArchive = x.ValideArchive,
			   AgenceId = x.AgenceId,
			   OperateurId = x.OperateurId,
			   DocBruteId = x.DocBruteId,
			   OperationId = x.OperationId,
			   DateDebutSaisie = x.CreatedDate,
			   DateFinTypage = x.DateFinSaisie

		   })
		  .ToListAsync(cancellationToken);
			return l;
		}

        public async Task<ArchiveDetailsModel> GetLotArchiveByLotArchiveId(Guid LotArchiveId, CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveDetailsModel> l = await _amenBankContext.Archive
		   .Where(x => x.LotArchiveId == LotArchiveId)
		   .AsNoTracking()
		   .Select(x => new ArchiveDetailsModel()
		   {
			   Id = x.Id,
			   PathArchive = x.PathArchive,
			   NomDOc = x.NomDOc,
			   Commentaire = x.Commentaire,
			   Etat = x.Etat,
			   ValideArchive = x.ValideArchive,
			   AgenceId = x.AgenceId,
			   OperateurId = x.OperateurId,
			   DocBruteId = x.DocBruteId,
			   OperationId = x.OperationId,
			   DateDebutSaisie = x.CreatedDate,
			   DateFinTypage = x.DateFinSaisie

		   })
		  .ToListAsync(cancellationToken);
			return (ArchiveDetailsModel)l;
		}

        public async Task <IEnumerable<ArchiveDetailsModel>> GetLotArchiveByLotArchiveIdEtatAgenceId(Guid LotArchiveId, Guid AgenceId, int Etat, CancellationToken cancellationToken)
        {
			IEnumerable<ArchiveDetailsModel> l = await _amenBankContext.Archive
			.Where(x => x.LotArchiveId == LotArchiveId)
			.Where(x => x.AgenceId == AgenceId)
			.Where(x => x.Etat == Etat)
			.AsNoTracking()
			.Select(x => new ArchiveDetailsModel()
			{
				Id = x.Id,
				PathArchive = x.PathArchive,
				NomDOc = x.NomDOc,
				Commentaire = x.Commentaire,
				Etat = x.Etat,
				ValideArchive = x.ValideArchive,
				AgenceId = x.AgenceId,
				OperateurId = x.OperateurId,
				DocBruteId = x.DocBruteId,
				OperationId = x.OperationId,
				DateDebutSaisie = x.CreatedDate,
				DateFinTypage = x.DateFinSaisie,
				LotArchiveId = x.LotArchiveId

			})
		   .ToListAsync(cancellationToken);
			return l;
		}

        public async Task<string> CreateArchive(Guid DocBruteId, Guid OperatorId, Guid OperationID, CancellationToken cancellationToken)
		{
			string BackupOCR = Configuration["Dir:BackupOCR"];
			string OutputOCR = Configuration["Dir:OutputOCR"];
			//Test if lotArchiveId Exixt or not 
			Guid LotId = await  _lotArchiveRepository.CreateLotArchiveExist(DocBruteId, cancellationToken);
			DocBrute doc = await _amenBankContext.DocBrute
										 .Where(x => x.Id == DocBruteId)
										 .FirstOrDefaultAsync(cancellationToken);
			Operateur operateur = await _amenBankContext.Operateur
											 .Where(x => x.id == OperatorId)
											 .FirstOrDefaultAsync(cancellationToken);
			AgenceModel ag = await _agenceReadRepository.GetAgenceById((Guid)operateur.AgenceId, cancellationToken);
			OperationModel op = await _operationReadRepository.GetOperationById(OperationID, cancellationToken);
			PacketModel packet = await _packetReadRepository.GetPacketById((Guid)doc.LotPacketId, cancellationToken);
			var packetNAmeSub = packet.NomPAcket.Substring(0, 6);
			string x = await _lotArchiveRepository.CreateOutOutOcrOperationDiercotries(packetNAmeSub,ag.CodeAgence, cancellationToken);
			// copie to BackupOCR 
			string OutputOCRpath = OutputOCR + "\\" + packetNAmeSub + "\\" + ag.CodeAgence + "\\"+op.CodeOperation + "\\"+ doc.NomDoc;
			File.Copy(doc.Commentaire, OutputOCRpath, true);

			// copie to outpu Final
			string BackupOCRpath = BackupOCR + "\\" + packetNAmeSub + "\\" + ag.CodeAgence + "\\" + op.CodeOperation +"\\" + doc.NomDoc;
			File.Copy(doc.Commentaire, BackupOCRpath);
			doc.Etat = 2;
			var myDateTimeOffset = (DateTimeOffset)DateTime.UtcNow;
			var NowDate = myDateTimeOffset.DateTime.ToUniversalTime();
			Guid res =  await _mediator.Send(new CreateArchiveCommand
			{
				Path = OutputOCRpath,
				ArchiveNomDOc = doc.NomDoc,
				ArchiveCommentaire = "Typer ",
				EtatArchive = 1,
				Valide = 0,
				IdAgence = ag.Id,
				IdOperateur = OperatorId,
				IdOperation = null,
				IdLot = LotId,
				IdDocBrute = doc.Id,
				dateDebutSaisie = DateTimeOffset.Now

			});

			Archive NewArchive = await _amenBankContext.Archive
											 .Where(x => x.Id == res)
											 .FirstOrDefaultAsync(cancellationToken);
			var myDateTimeOffset2 = (DateTimeOffset)DateTime.UtcNow;
			var editDate = myDateTimeOffset2.DateTime.ToLocalTime();

			NewArchive.Etat = 2; 
			NewArchive.OperationId = OperationID;
			NewArchive.DateFinSaisie = editDate;





			return res.ToString();

		}



        #endregion
    }



}
