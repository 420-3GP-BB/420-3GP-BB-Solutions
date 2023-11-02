using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Joueur
    {
        public string Nom
        {
            private set;
            get;
        }

        public Joueur(string nom)
        {
            Nom = nom;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
