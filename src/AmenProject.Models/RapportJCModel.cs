using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class RapportJCModel
    {


        public int CodeAgence { get; set; }
        public string NomAgence { get; set; }
        public int NbDocuments { get; set; }

        public RapportJCModel(int codeAgence, string nomAgence, int nbDocuments)
        {
            CodeAgence = codeAgence;
            NomAgence = nomAgence;
            NbDocuments = nbDocuments;
        }
    }
}