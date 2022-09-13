using DEMAT.ApplicationServices.UseCases.CreatePacket;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Services
{
    public class PacketReadRepository : ReadOnlyRepository<LotPacket>, IPacketReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		private readonly IDocBruteReadRepository _docBruteReadRepository;
		private IConfiguration Configuration;
		private readonly IMediator _mediator;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="PacketReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public PacketReadRepository(IDematContext amenBankContext, IConfiguration configuration, IMediator mediator, IDocBruteReadRepository docBruteReadRepository) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
			Configuration = configuration;
			_mediator = mediator;
			_docBruteReadRepository = docBruteReadRepository;

		}
        #endregion

        #region Methods
        public async Task<PacketModel> GetPacketById(Guid packetId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.LotPacket
					.Where(x => x.Id == packetId)
					.AsNoTracking()
					.Select(x => new PacketModel() { 
						Id = x.Id,
						NomPAcket = x.NomPAcket,
						EtatLotPacket = x.EtatLotPacket,
						NombreDoc = x.NombreDoc
					})
					.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IEnumerable<PacketModel>> GetAllPackets(CancellationToken cancellationToken)
		{
			return await _amenBankContext.LotPacket
					.AsNoTracking()
					.Select(x => new PacketModel()
					{
						Id = x.Id,
						NomPAcket = x.NomPAcket,
						EtatLotPacket = x.EtatLotPacket,
						NombreDoc = x.NombreDoc
					})
					.ToListAsync(cancellationToken);
		}

		public async  Task<string> CopieInputFilesToOutputLocal(OperateurModel op,CancellationToken cancellationToken)
		{
			string PathDirectory = Configuration["Dir:DirectoryScan"];
			string InputLocal = Configuration["Dir:InputLocal"];
			string OutputLocal = Configuration["Dir:OutputLocal"];
			string InputOCR = Configuration["Dir:InputOCR"];
			string BackupOCR = Configuration["Dir:BackupOCR"];
			string OutputOCR = Configuration["Dir:OutputOCR"];
			string PackageNameResult, NamePackageOutputLocal, NamePackageInputOCR, PathPacket;
			string NamePackage = PackageName();
			int NumberOfDocuments;
	         
		   

				if (Directory.GetFiles(InputLocal).Count() != 0)
			    {
				

				if ((Directory.GetDirectories(OutputLocal).Count()) == 0)
				{
					PackageNameResult = CreateNewPackage(NamePackage, InputLocal, OutputLocal);
					NamePackageOutputLocal = OutputLocal + "\\" + NamePackage + "\\" + PackageNameResult;
					NamePackageInputOCR = InputOCR + "\\" + NamePackage + "\\" + CreateNewPackage(NamePackage, InputLocal, InputOCR);
					
					importScannedDocuments1(InputLocal, NamePackageOutputLocal, NamePackageInputOCR);
					NumberOfDocuments = Directory.GetFiles(NamePackageInputOCR).Count();
					PathPacket = NamePackageInputOCR;
				}
                else 
                {
					if (Directory.Exists(OutputLocal+"\\"+ NamePackage) )
					{
						PackageNameResult = CreateNewSubPackage(NamePackage, InputLocal, OutputLocal);
						NamePackageOutputLocal = OutputLocal + "\\" + NamePackage + "\\" + PackageNameResult;
						NamePackageInputOCR = InputOCR + "\\" + NamePackage + "\\" + CreateNewSubPackage(NamePackage, InputLocal, InputOCR);
						importScannedDocuments1(InputLocal, NamePackageOutputLocal, NamePackageInputOCR);
						NumberOfDocuments = Directory.GetFiles(NamePackageInputOCR).Count();
						PathPacket = NamePackageInputOCR;

					}
					else
                    {

						PackageNameResult = CreateNewPackage(NamePackage, InputLocal, OutputLocal);
						NamePackageOutputLocal = OutputLocal + "\\" + NamePackage + "\\" + PackageNameResult;
						NamePackageInputOCR = InputOCR + "\\" + NamePackage + "\\" + CreateNewPackage(NamePackage, InputLocal, InputOCR);
						importScannedDocuments1(InputLocal, NamePackageOutputLocal, NamePackageInputOCR);
						NumberOfDocuments = Directory.GetFiles(NamePackageInputOCR).Count();
						PathPacket = NamePackageInputOCR;
					}
				}

				    Guid idPacket = await _mediator.Send(new CreatePacketCommand {
                        NbDoc = NumberOfDocuments,
				        Nom = PackageNameResult,
				        Etat= 0
				    });
				    PacketModel packet = await GetPacketById(idPacket,cancellationToken);
				
				await _docBruteReadRepository.InsertDocBrute(PathPacket, packet,op);
				
			    }
			  else
			  {
				PackageNameResult =null;
			  }

		   
			return await Task.FromResult(PackageNameResult);
		}
	     
		private  string PackageName()
        {
			
			string Year = DateTimeOffset.Now.Year.ToString();
			int Month = DateTimeOffset.Now.Month;
			int Day = DateTimeOffset.Now.Day;

			string y,m,d;
			y = Year.Substring(2,2);
			if (Month<10)
            {
				m = "0" + (Month.ToString());         
			}
			else
			{ 
				m = Month.ToString();
			}
			if (Day < 10)
			{
				d = "0" + (Day.ToString());
			}
			else
			{
				d = Day.ToString();
			}
			string date=d+m+y;

			return date;
        }

		private void importScannedDocuments1(string input , string outputLocal , string inputOCR)
        {
			string[] files = Directory.GetFiles(input);
			foreach (string file in files)
			{
				string FileName = Path.GetFileName(file);

				// copie to outpu Local
				string OutputLocalPath = Path.Combine(outputLocal, FileName);
				File.Copy(file, OutputLocalPath, true);

				// Move to outpu Final
				string OutputFinalPath = Path.Combine(inputOCR, FileName);
				File.Move(file, OutputFinalPath);
            }
		}

     	private string CreateNewPackage(string NamePackage, string input, string Distination)
	    {

				// create folder package 
				String Package = Distination + "\\" + NamePackage;
				Directory.CreateDirectory(Package);
				// create Subfolder package 
				NamePackage = NamePackage + "P1";
				String Subfolder = Package + "\\" + NamePackage;
				Directory.CreateDirectory(Subfolder);

				return NamePackage;
        }

		private string CreateNewSubPackage(string NamePackage, string input, string Distination)
        {

			string SubfoldersPath = Distination + "\\" + NamePackage;
			int NumberOfSubfolders = Directory.GetDirectories(SubfoldersPath).Count();
			String Package = Distination + "\\" + NamePackage;
			NamePackage = NamePackage + "P" + (NumberOfSubfolders + 1).ToString();
			String Subfolder = Package + "\\" + NamePackage;
			Directory.CreateDirectory(Subfolder);

          	return NamePackage;
		}

        public async Task<PacketModel> GetPacketByIdDocBrute(Guid DocBruteID, CancellationToken cancellationToken)
        {
			 DocBruteModel doc = await _docBruteReadRepository.GetDocBruteById(DocBruteID, cancellationToken);
			 IEnumerable <PacketModel> Packets = await GetAllPackets(cancellationToken);
			 PacketModel Result = null; 
			foreach (PacketModel p in Packets)
			{
				if (doc.LotPacketId.Equals(p.Id))
					Result = p;
				else
					continue;
			}
            return Result;
		}

        public async Task<string> CreateDirectories(CancellationToken cancellationToken)
        {

			string Result = null; 

			string ScanDirectory = Configuration["Dir:ScanDirectory"];
			string InputLocal = Configuration["Dir:InputLocal"];
			string OutputLocal = Configuration["Dir:OutputLocal"];
			string InputOCR = Configuration["Dir:InputOCR"];
			string TypageDirectory = Configuration["Dir:TypageDirectory"];
			string BackupOCR = Configuration["Dir:BackupOCR"];
			string OutputOCR = Configuration["Dir:OutputOCR"];

			Directory.CreateDirectory(OutputLocal);
			//ScanDirectories 
			if (Directory.Exists(ScanDirectory) == false)
			{
				Directory.CreateDirectory(ScanDirectory);
				Directory.CreateDirectory(InputLocal);
				Directory.CreateDirectory(OutputLocal);
				 
				Result = "Directories creadted  ";
			}
			else
			{
				while ((Directory.Exists(InputLocal) == false) || (Directory.Exists(OutputLocal) == false) )
				{
					if (Directory.Exists(InputLocal) == false)
					{
						Directory.CreateDirectory(InputLocal);
						continue;
					}
					else
					if (Directory.Exists(OutputLocal) == false)
					{
						Directory.CreateDirectory(OutputLocal);
						continue;
					}
					
				}

				Result = "Directories creadted  ";


			}
			//Typage dierctories 

			if (Directory.Exists(TypageDirectory) == false)
			{
				Directory.CreateDirectory(ScanDirectory);
				Directory.CreateDirectory(InputOCR);
				Directory.CreateDirectory(BackupOCR);
				Directory.CreateDirectory(OutputOCR);
				
				Result = Result + "& Typage Dierctories are created ";
			}
			else
			{
				while ((Directory.Exists(InputOCR) == false) || (Directory.Exists(BackupOCR) == false) || (Directory.Exists(OutputOCR) == false) )
				{
					if (Directory.Exists(InputOCR) == false) 
					{
						Directory.CreateDirectory(InputOCR);
						continue;
					}
					else
					if (Directory.Exists(BackupOCR) == false)
					{
						Directory.CreateDirectory(BackupOCR);
						continue;
					}
					else
					if (Directory.Exists(OutputOCR) == false)
					{
						Directory.CreateDirectory(OutputOCR);
						break;

					}

				}
				Result = Result + "& Typage Dierctories are created ";


			}

			return await Task.FromResult(Result);

		}

        public async Task<IEnumerable<PacketModel>> GetListPacketByEtatDocBrute(int Etat, CancellationToken cancellationToken)
        {
			List<PacketModel> Result = new List<PacketModel>();
			IEnumerable<PacketModel> Packets = await GetAllPackets(cancellationToken);
			foreach (PacketModel p in Packets )
			{
				var x = (await _amenBankContext.DocBrute
				.Where(x => x.Etat == Etat)
				.AsNoTracking()
				.Select(x => new DocBruteModel()
				{
					Id = x.Id,
					NomDoc = x.NomDoc,
					Commentaire = x.Commentaire,
					Etat = x.Etat,
					LotPacketId = (Guid)x.LotPacketId

				})
				.ToListAsync(cancellationToken)).Count();

				var nbDocsEtat = (await _amenBankContext.DocBrute
				.Where(x => x.Etat == Etat && x.LotPacketId == p.Id )
				.AsNoTracking()
				.Select(x => new DocBruteModel()
				{
					Id = x.Id,
					NomDoc = x.NomDoc,
					Commentaire = x.Commentaire,
					Etat = x.Etat,
					LotPacketId = (Guid)x.LotPacketId

				})
				.ToListAsync(cancellationToken)).Count();



				if (x != 0 && nbDocsEtat !=0 )
				{
					Result.Add( new PacketModel(p.Id,p.NomPAcket,p.EtatLotPacket, nbDocsEtat));
				}
			}
			return Result;
        }



        #endregion

    }
}
