using System;
using System.Collections.Generic;
using System.Text;

namespace APIActiviteVisiteur.ClassesMetier
{
    public class Rapport
    {
        public int Num { get; set; }
        public string Date { get; set; }
        public string Motif { get; set; }
        public string Bilan { get; set; }
        public Praticien PraNum { get; set; }
        public Visiteur VisMatricule { get; set; }

    }
}
