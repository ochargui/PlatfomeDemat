using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class JourneOperationModel
    {
        public string Journe { get; set; }
        public int  EnAttente { get; set; }
        public int  totale { get; set; }
        public int  Saisie { get; set; }

        public JourneOperationModel()
        {
        }

        public JourneOperationModel(string journe, int enAttente, int enCours, int saisie)
        {
            Journe = journe;
            EnAttente = enAttente;
            totale = enCours;
            Saisie = saisie;
        }
    }
}
