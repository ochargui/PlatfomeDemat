using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using Microsoft.EntityFrameworkCore;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DEMAT.Infrastructure.Identity.Models;

namespace DEMAT.Services
{
	public class OperateurReadRepository : ReadOnlyRepository<Operateur>, IOperateurReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		private readonly IArchiveReadRepository _archiveReadRepository;
		private readonly IDocBruteReadRepository _docBruteReadRepository;

		private readonly IAmenUnitOfWork _amenUnitOfWork;


		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OperateurReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public OperateurReadRepository(IAmenUnitOfWork amenUnitOfWork ,IDematContext amenBankContext, IArchiveReadRepository archiveReadRepository, IDocBruteReadRepository docBruteReadRepository) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
			_archiveReadRepository = archiveReadRepository;
			_docBruteReadRepository = docBruteReadRepository;
			_amenUnitOfWork = amenUnitOfWork;
		}

      
		#endregion

		#region Methods

		public async Task<IEnumerable<OperateurModel>> GetAllOperators(CancellationToken cancellationToken)
		{
			 string fmt = "d";
			return await _amenBankContext.Operateur
						.AsNoTracking()
						.Select(x => new OperateurModel()
						{
							id = x.id,
							nom = x.nom,
							prenom = x.prenom,
							login = x.login,
							password = x.password,
							mail = x.mail,
							NumTel = x.NumTel,
							DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
							Role = x.Role,
							Discipline = x.Discipline,
							Equipe = x.Equipe,
							
							AgenceID = (Guid)x.AgenceId,
							AgenceName = x.Agence.NomAgence


						})
						.ToListAsync(cancellationToken);
		}

 

        public async Task<OperateurModel> GetOperatorById(Guid operatorId, CancellationToken cancellationToken)
		{
			string fmt = "d";
			return await _amenBankContext.Operateur
					.Where(x => x.id == operatorId)
					 .AsNoTracking()
					 .Select(x => new OperateurModel()
					 {
						 id = x.id,
						 nom = x.nom,
						 prenom = x.prenom,
						 login = x.login,
						 password = x.password,
						 mail = x.mail,
						 NumTel = x.NumTel,
						 DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
						 Role = x.Role,
						 Discipline = x.Discipline,
						 Equipe = x.Equipe,

						 AgenceID = (Guid)x.AgenceId,
						 AgenceName = x.Agence.NomAgence
					 })
					 .FirstOrDefaultAsync(cancellationToken);

		}


		public async Task<IEnumerable<OperateurModel>> GetOnlineOperators(CancellationToken cancellationToken)
		{
			List<ArchiveModel> ArchiveEncours = await _amenBankContext.Archive
					.AsNoTracking()
					.Where(x => x.Etat == 1)
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
						OperationId = x.OperationId

					})
					.ToListAsync(cancellationToken);
			List<OperateurModel> OperateurConnecter = new List<OperateurModel>();
			foreach(ArchiveModel a in ArchiveEncours)
			{
				OperateurModel operateur = await GetOperatorById((Guid)a.OperateurId, cancellationToken);
				if (OperateurConnecter.Count() == 0)
				{	
					OperateurConnecter.Add(operateur);
				}
				else
			    {
						if (OperateurConnecter.Exists(x => x.id == a.OperateurId) == false)
					    {
							OperateurConnecter.Add(operateur);
					    }
				}
			    
			}

			return OperateurConnecter;


		}

      
        public async  Task<IEnumerable<OperateurModel>> GetOperateurrsEquipe(string E, CancellationToken cancellationToken)
		{
			string fmt = "d";
		
			return await ( from x in _amenBankContext.Operateur
						   where(x.Equipe.Equals(E))
						   select new OperateurModel()
						   {
							   id = x.id,
							   nom = x.nom,
							   prenom = x.prenom,
							   login = x.login,
							   password = x.password,
							   mail = x.mail,
							   NumTel = x.NumTel,
							   DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
							   Role = x.Role,
							   AgenceID = (Guid)x.AgenceId,
							   AgenceName = x.Agence.NomAgence


						   })
						 .ToListAsync(cancellationToken);
				
		}
		#endregion

		public async Task<IEnumerable<OperateurArchiveSatModel>> GetListArchiveOperateur(DateTimeOffset DateDebut, DateTimeOffset DateFIn, string Equipe, CancellationToken cancellationToken)
		{
			IEnumerable <OperateurModel> Operateurs =  await GetOperateurrsEquipe(Equipe.ToUpper(), cancellationToken);
			List<OperateurArchiveSatModel> Result = new List<OperateurArchiveSatModel>();

			foreach (OperateurModel op in Operateurs) 
            {
				OperateurArchiveSatModel row = new OperateurArchiveSatModel();
				TimeSpan dureeTotale = new TimeSpan();
				IEnumerable<ArchiveDetailsModel> archives = await _archiveReadRepository.GetListArchiveOperatorDate(DateDebut, DateFIn, op.id, 2, cancellationToken);
				if (archives.Count() != 0)
				{ 
				   foreach (ArchiveDetailsModel a in archives) 
                   {
						DateTimeOffset d = a.DateDebutSaisie;
						//var targetTime = d.ToOffset(new TimeSpan(-7, 0, 0));
						DateTimeOffset df = a.DateFinTypage;
						TimeSpan DureTraitementDoc = a.DateFinTypage - a.DateDebutSaisie;
					    dureeTotale = dureeTotale + DureTraitementDoc;
                   }
				    row.LoginOpoerator = op.login;
				    row.NBDocuments = archives.Count();
                    int min =  Convert.ToInt32(dureeTotale.TotalMinutes);
				    row.Duree = Duree(dureeTotale.TotalMinutes);
					//moyenne
					double TempsSecondeMoy = dureeTotale.TotalSeconds / archives.Count();
					TimeSpan t2 = TimeSpan.FromSeconds(TempsSecondeMoy);
					string TempsCadenceMoyenne = string.Format("{0:D2}:{1:D2}:{2:D2}", t2.Hours, t2.Minutes, t2.Seconds);
					row.Moyenne = TempsCadenceMoyenne;
					Result.Add(row);
				}
			}
			return Result;
		}
		
		private  string Duree(double t)
        {  string result=null;
            if (t < 60 )
            {
				result = Convert.ToInt32(t) + " m";
            }
			if ( 60 <= t && t <= 1440)
			{
				double d = t / 60;
				int h = Convert.ToInt32(d);
				int min = Convert.ToInt32((d-h)*100);
				result = h+" h "+ min+ " m";
			}
            if (t > 1440)
            {
				double d = t / 1440;
				int day = Convert.ToInt32(d);
				string Rest = (d-day).ToString("F3");
				double x = Convert.ToDouble(Rest)*1440;

                if (x < 60)
                {
					int m = Convert.ToInt32(x);
					result =day + " d" + m + " m";
				}
                else
                {
					double y = x / 60;
					int h = Convert.ToInt32(y);
					int min = Convert.ToInt32((y-h) * 100);
					result = day + " d " + h +" h "+ min + " m";
				}




			}


			return result;

		}

        public async Task<string> UpdateDiscipline(Guid operatorId, string NewDicsipline, CancellationToken cancellationToken)
        {  // string fmt = "d";
			Operateur op = await _amenBankContext.Operateur
					.Where(x => x.id == operatorId)
					 .FirstOrDefaultAsync(cancellationToken);
			op.Discipline = NewDicsipline;

			return op.Discipline ;

		}

        public async Task<OperateurModel> SinIn(string login, string pwd, CancellationToken cancellationToken)
		{
			string fmt = "d";
			OperateurModel op = await _amenBankContext.Operateur
					    .Where(x => x.login.Equals(login) && x.password.Equals(pwd))
						.AsNoTracking()
						.Select(x => new OperateurModel()
						{
							id = x.id,
							nom = x.nom,
							prenom = x.prenom,
							login = x.login,
							password = x.password,
							mail = x.mail,
							NumTel = x.NumTel,
							DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
							Role = x.Role,
							AgenceID = (Guid)x.AgenceId,
							Equipe = x.Equipe,
							Discipline = x.Discipline,
							AgenceName = x.Agence.NomAgence
						})
						.FirstOrDefaultAsync(cancellationToken);

			
				return op;
		   
	}

        public async Task<OperateurModel> GetOperatorByLogin(string login, CancellationToken cancellationToken)
        {
			string fmt = "d";
			return await _amenBankContext.Operateur
					.Where(x => x.login.Equals(login) )
					 .AsNoTracking()
					 .Select(x => new OperateurModel()
					 {
						 id = x.id,
						 nom = x.nom,
						 prenom = x.prenom,
						 login = x.login,
						 password = x.password,
						 mail = x.mail,
						 NumTel = x.NumTel,
						 DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
						 Role = x.Role,
						 Discipline = x.Discipline,
						 Equipe = x.Equipe,

						 AgenceID = (Guid)x.AgenceId,
						 AgenceName = x.Agence.NomAgence
					 })
					 .FirstOrDefaultAsync(cancellationToken);
		}

        Task<OperateurModel> IOperateurReadRepository.GetOperatorById(Guid IdOperator, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
