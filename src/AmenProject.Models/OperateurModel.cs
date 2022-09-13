using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class OperateurModel
    {  
        public Guid id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string DateRecrutement { get; set; }
        public string Discipline { get; set; }
        public string mail { get; set; }
        public string NumTel { get; set; }
        public string Role { get; set; }
        public string Equipe { get; set; }
        public Guid AgenceID { get; set; }
        public string AgenceName { get; set; }

    }
}
