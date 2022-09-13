using DEMAT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using DEMAT.Domain.Entities.Documents;

namespace DEMAT.Infrastructure
{
    public interface IDematContext : IContext
    {
         DbSet<LotArchive> LotArchive { get; set; }
         DbSet<Agence> Agence { get; set; }
         DbSet<AllPonderation> AllPonderation { get; set; }
         DbSet<Archive> Archive { get; set; }
         DbSet<Controle> Controle { get; set; }
         DbSet<Data> Data { get; set; }
         DbSet<DocBrute> DocBrute { get; set; }
         DbSet<GroupeControle> GroupeControle { get; set; }
         DbSet<LotPacket> LotPacket { get; set; }
         DbSet<Operateur> Operateur { get; set; }
         DbSet<Operation> Operation { get; set; }
         DbSet<Ponderate> Ponderate { get; set; }
         DbSet<Typologies> Typologie { get; set; }
         DbSet<ZoneAgence> ZoneAgence { get; set; }
         DbSet<RawDocument> RawDocuments { get; set; }
         DbSet<DocumentPicture> DocumentPictures { get; set; }
         DbSet<DocumentFields> DocumentsFields { get; set; }
         DbSet<Lot> Lots { get; set; }
         DbSet<Control> Controls { get; set; }





    }
}
