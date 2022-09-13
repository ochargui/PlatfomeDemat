using DEMAT.Domain.Entities;
using DEMAT.Models;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DEMAT.Infrastructure.Identity.Models;

namespace DEMAT.Domain.Interfaces
{
    public interface IOperateurReadRepository
    {
        Task<OperateurModel> GetOperatorById(Guid IdOperator, CancellationToken cancellationToken);

        Task<IEnumerable<OperateurModel>>GetAllOperators(CancellationToken cancellationToken);
       
        Task<IEnumerable<OperateurModel>>GetOnlineOperators(CancellationToken cancellationToken);
        Task<IEnumerable<OperateurModel>> GetOperateurrsEquipe(string Equipe, CancellationToken cancellationToken); 
     
        Task<IEnumerable<OperateurArchiveSatModel>>GetListArchiveOperateur(DateTimeOffset DateDebut, DateTimeOffset DateFIn, string Equipe, CancellationToken cancellationToken);
        Task<string>UpdateDiscipline(Guid operatorId, string Dicsipline, CancellationToken cancellationToken);
        Task<OperateurModel> SinIn(string login, string pwd, CancellationToken cancellationToken);
        Task<OperateurModel> GetOperatorByLogin(string login ,CancellationToken cancellationToken);


    }
}
