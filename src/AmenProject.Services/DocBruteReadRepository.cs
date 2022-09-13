using DEMAT.ApplicationServices.UseCases.CreateDocBrute;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
	public class DocBruteReadRepository : ReadOnlyRepository<DocBrute>, IDocBruteReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		private readonly IAmenUnitOfWork _amenUnitOfWork;
		private IMediator _mediator;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DocBruteRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public DocBruteReadRepository(IDematContext amenBankContext, IAmenUnitOfWork amenUnitOfWork, IMediator mediator) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
			_amenUnitOfWork = amenUnitOfWork;
			_mediator = mediator;
		}
		#endregion

		#region Methods
		public async Task<IEnumerable<DocBruteModel>> GetAllDocBrutes(CancellationToken cancellationToken)
		{
			return await _amenBankContext.DocBrute
				.AsNoTracking()
				.Select(x => new DocBruteModel()
				{
					Id = x.Id,
					NomDoc = x.NomDoc,
					Commentaire = x.Commentaire,
					Etat = x.Etat,
					LotPacketId = (Guid)x.LotPacketId,
					PacketName = x.LotPacket.NomPAcket

				})
				.ToListAsync(cancellationToken);

		}

		public async Task<DocBruteModel> GetDocBruteById(Guid docBruteId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.DocBrute
				.Where(x => x.Id == docBruteId)
				.AsNoTracking()
				.Select(x => new DocBruteModel()
				{
					Id = x.Id,
					NomDoc = x.NomDoc,
					Commentaire = x.Commentaire,
					Etat = x.Etat,
					LotPacketId = (Guid)x.LotPacketId,
					PacketName =x.LotPacket.NomPAcket


				})
				.FirstOrDefaultAsync(cancellationToken);
		}

		public Task<string> InsertDocBrute(string path, PacketModel packet,OperateurModel op)
		{

			string[] files = Directory.GetFiles(path);

			foreach (string file in files)
			{
				string FileName = Path.GetFileName(file);
				_mediator.Send(new CreateDocBruteCommand
				{ NomDocument = FileName,
					Comment = path + "\\" + FileName,
					LotPacketID = packet.Id,
					EtatDocument = 0,
					DebutTypageDate = DateTimeOffset.Now,
					AgenceID =op.AgenceID
				});
			}

			return Task.FromResult("ok");

		}

		public async Task<IEnumerable<DocBrutePacketRow>> GetDocBruteByPacket(PacketModel packet, CancellationToken cancellationToken)
		{
			return await (from doc in _amenBankContext.DocBrute
						  where doc.LotPacketId == packet.Id
						  select (new DocBrutePacketRow()
						  {
							  Idpacket = (Guid)doc.LotPacketId,
							  NomPAcket = packet.NomPAcket,
							  IdDocBrute = doc.Id,
							  NomDoc = doc.NomDoc,
							  Commentaire = doc.Commentaire,
							  Etat = doc.Etat
						  })
						 ).ToListAsync();
		}
		public async Task<IEnumerable<DocBrute>> GetDocBruteByIdPacket(Guid IdPacke, CancellationToken cancellationToken)
		{
			return await (from doc in _amenBankContext.DocBrute
						  where doc.LotPacketId == IdPacke
						  select (new DocBrute()
						  {   NomDoc = doc.NomDoc,
							  Commentaire = doc.Commentaire,
							  Etat = doc.Etat
						  })
						 ).ToListAsync();
		}


		public async Task<string>  UpdateEtatDocBrute(Guid DocBruteId , int NewEtat, CancellationToken cancellationToken)
        {
			 

		     DocBrute  docBrute = await _amenBankContext.DocBrute
											 .Where(x => x.Id == DocBruteId)
											 .FirstOrDefaultAsync(cancellationToken);
			docBrute.Etat = NewEtat;
			return docBrute.Etat.ToString();
			
	    }

        public async Task<IEnumerable<DocBruteModel>> GetDocBruteByEtatByLotPacketId(int EtatDoc, Guid IdPacket, CancellationToken cancellationToken)
        {
			return await(from x in _amenBankContext.DocBrute
						 where x.LotPacketId == IdPacket && x.Etat == EtatDoc
						 select (new DocBruteModel()
						 {
							 Id = x.Id,
							 NomDoc = x.NomDoc,
							 Commentaire = x.Commentaire,
							 Etat = x.Etat,
							 LotPacketId = (Guid)x.LotPacketId,
							 PacketName = x.LotPacket.NomPAcket
						 })
					 ).ToListAsync();
		}

        public async Task<IEnumerable<DocumentsEnAttenteModel>> ListDocsByEtatAgence(int Etat, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken)
        {
			List<DocumentsEnAttenteModel> Result = new List<DocumentsEnAttenteModel>();
			foreach (AgenceModel agence in agences)
			{
				IEnumerable<DocBruteModel> List = await(from x in _amenBankContext.DocBrute
													   where (x.Etat == Etat)
													   && (x.AgenceId == agence.Id)
													   select new DocBruteModel()
													   {
														   Id = x.Id,
														   NomDoc = x.NomDoc,
														   Commentaire = x.Commentaire,
														   Etat = x.Etat,
														   LotPacketId = (Guid)x.LotPacketId,
														   AgenceId = (Guid)x.AgenceId
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
		public async Task<IEnumerable<JourneOperationModel>> ListDOcsJourne(CancellationToken cancellationToken)
		{
			IEnumerable<DocBruteModel> List = (IEnumerable<DocBruteModel>)await (from x in _amenBankContext.DocBrute
															   where  x.Etat == 0
														       select (new DocBruteModel()
															   {
																   Id = x.Id,
																   NomDoc = x.NomDoc,
																   Commentaire = x.Commentaire,
																   Etat = x.Etat,
																   LotPacketId = (Guid)x.LotPacketId,
																   PacketName = x.LotPacket.NomPAcket


															   })).ToListAsync();

			List<JourneOperationModel> ListeJourne = new List<JourneOperationModel>();

			foreach (DocBruteModel l in List)
			{
				Guid idDocBrute = (Guid)l.Id;
				//PacketModel packet = await _packetReadRepository.GetPacketByIdDocBrute(idDocBrute, cancellationToken);
				string Journe = l.PacketName.Substring(0, 6);

				IEnumerable<DocBruteModel> res = await (from d in _amenBankContext.DocBrute
													   where (d.LotPacket.NomPAcket.StartsWith(Journe))
													   select new DocBruteModel()
													   {
														   Id = d.Id,
														   NomDoc = d.NomDoc,
														   Commentaire = d.Commentaire,
														   Etat = d.Etat,
														   LotPacketId = (Guid)d.LotPacketId,
														   PacketName = d.LotPacket.NomPAcket

													   })
													   .ToListAsync(cancellationToken);

				IEnumerable<DocBruteModel> EnAttente = await ListDocsByEtatJourne(0, Journe, cancellationToken);
				IEnumerable<DocBruteModel> EnCours = await ListDocsByEtatJourne(1, Journe, cancellationToken);
				IEnumerable<DocBruteModel> Saisie = await ListDocsByEtatJourne(2, Journe, cancellationToken);
				string JourneFinal = packageNameToDate(Journe);
				JourneOperationModel row = new JourneOperationModel(JourneFinal, EnAttente.Count(), res.Count(), Saisie.Count());
				if (ListeJourne.Exists(x => x.Journe == row.Journe) == false)
				{
					ListeJourne.Add(row);
				}


			}
			return ListeJourne;
		}

		public async Task<IEnumerable<DocBruteModel>> ListDocsByEtatJourne(int Etat, string Journe, CancellationToken cancellationToken)
		{
			IEnumerable<DocBruteModel> List = await (from d in _amenBankContext.DocBrute
													where (d.Etat == Etat)
												  && (d.LotPacket.NomPAcket.StartsWith(Journe))
													select new DocBruteModel()
													{
														Id = d.Id,
														NomDoc = d.NomDoc,
														Commentaire = d.Commentaire,
														Etat = d.Etat,
														LotPacketId = (Guid)d.LotPacketId,
														PacketName = d.LotPacket.NomPAcket

													}).
													ToListAsync(cancellationToken);
			return List;
		}

		public string packageNameToDate(string Journe)
		{
			string Result = Journe.Substring(0, 2)
				   + "-" +
				   Journe.Substring(2, 2)
				   + "-" +
				   Journe.Substring(4, 2);
			return Result;
		}
		#endregion

	}
}
