using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class RapportFacturationModel
    {
        public string AgenceName { get; set; }
        public int AgenceCode { get; set; }
        public string  JourneeComptable { get; set; }
        public int NombreDocumentsTraitees { get; set; }
        public int DiversRejets { get; set; }

        public RapportFacturationModel(string agenceName, int agenceCode, string journeeComptable, int nombreDocumentsTraitees, int diversRejets)
        {
            AgenceName = agenceName;
            AgenceCode = agenceCode;
            JourneeComptable = journeeComptable;
            NombreDocumentsTraitees = nombreDocumentsTraitees;
            DiversRejets = diversRejets;
        }
    }
}
