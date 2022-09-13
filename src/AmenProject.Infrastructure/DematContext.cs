using DEMAT.Core;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;

namespace DEMAT.Infrastructure
{
    public class DematContext : DbContextBase , IDematContext
    {
        public virtual DbSet<Agence> Agence { get; set; }
        public virtual DbSet<AllPonderation> AllPonderation { get; set; }
      
        public virtual DbSet<Controle> Controle { get; set; }
        public virtual DbSet<Data> Data { get; set; }
        public virtual DbSet<DocBrute> DocBrute { get; set; }
        public virtual DbSet<GroupeControle> GroupeControle { get; set; }
        public virtual DbSet<LotPacket> LotPacket { get; set; }
        public virtual DbSet<Operateur> Operateur { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Ponderate> Ponderate { get; set; }
        public virtual DbSet<Typologies> Typologie { get; set; }
        public virtual DbSet<ZoneAgence> ZoneAgence { get; set; }
        public virtual DbSet<LotArchive> LotArchive { get; set; }
        public virtual DbSet<Archive> Archive { get; set; }
        public virtual DbSet<RawDocument> RawDocuments { get; set; }
        public virtual DbSet<DocumentPicture> DocumentPictures { get; set; }
        public virtual DbSet<DocumentFields> DocumentsFields { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public DbSet<Control> Controls { get; set; }

        public DematContext(DbContextOptions<DematContext> options, ILogger<DbContextBase> loggerService)
          : base(options, loggerService)
        {
           //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
         

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DematContext).Assembly);
           // Archive -- DocBrute  one to one 
           /* modelBuilder.Entity<DocBrute>()
            .HasOne(b => b.Archive)
            .WithOne(i => i.DocBrute)
            .HasForeignKey<Archive>(b => b.DocBruteId);*/

          
        }

        /// <summary>
        /// Called before save changes.
        /// </summary>
        protected override void OnBeforeSaveChanges()
        {
            SetAuditable();
            SetSoftDelete();
            base.OnBeforeSaveChanges();
        }

        private void SetAuditable()
        {
            //string userId = _timeContext?.GetSubjectId();
            //string userName = _timeContext?.GetName();

            // Change Created date & Modified date
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.Entity is IAuditable entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        //if (!string.IsNullOrEmpty(userName))
                        //{
                        //    entity.CreatedBy = userName;
                        //}

                        //if (!string.IsNullOrEmpty(userId))
                        //{
                        //    entity.CreatedById = userId;
                        //}

                        entity.CreatedDate = DateTimeOffset.Now; // _dateTimeService.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        //if (!string.IsNullOrEmpty(userName))
                        //{
                        //    entity.ModifiedBy = userName;
                        //}

                        //if (!string.IsNullOrEmpty(userId))
                        //{
                        //    entity.ModifiedById = userId;
                        //}

                        entity.ModifiedDate = DateTimeOffset.Now; //_dateTimeService.Now;
                    }
                }
            }

        }

        /// <summary>
        /// Sets the soft delete behavior.
        /// </summary>
        private void SetSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
                {
                    softDelete.IsDeleted = true;
                    softDelete.DeletedDate = DateTimeOffset.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
