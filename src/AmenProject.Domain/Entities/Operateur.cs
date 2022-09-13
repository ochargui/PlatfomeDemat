using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Domain.Entities
{
    public class Operateur : Entity, IAuditable
    {
        public Guid id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public DateTimeOffset DateRecrutement { get; set; }
        public string mail { get; set; }
        public string NumTel { get; set; }
        public string Role { get; set; }
        public string Equipe { get; set; } // jour OU nuit
        public string Discipline { get; set; }
        public Guid? AgenceId { get; set; }

        #region Relation 
        public virtual Agence Agence { get; set; }
        public virtual ICollection<Archive> Archives { get; set; } // one to many with Archive 
        #endregion

        #region Auditable
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
        #endregion
    }
}
