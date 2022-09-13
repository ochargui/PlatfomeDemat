using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class DocumentsModel
    {
        public DocumentsModel()
        {
        }

        public DocumentsModel(string journe, string agenceName, int nombreDocumentsTotale, int nombreDocumentsValide, int nombreDocumentsInValide)
        {
            Journe = journe;
            AgenceName = agenceName;
            NombreDocumentsTotale = nombreDocumentsTotale;
            NombreDocumentsValide = nombreDocumentsValide;
            NombreDocumentsInValide = nombreDocumentsInValide;
        }

        public string Journe { get; set; }
        public string AgenceName { get; set; }
        public int NombreDocumentsTotale { get; set; }
        public int NombreDocumentsValide { get; set; }
        public int NombreDocumentsInValide { get; set; }

    }
}
