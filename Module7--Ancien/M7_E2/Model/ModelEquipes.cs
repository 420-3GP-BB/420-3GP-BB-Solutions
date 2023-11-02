using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Model
{



    public class ModelEquipes
    {

        public List<Equipe> LesEquipes
        {
            private set;
            get;
        }


        public ModelEquipes()
        {
            LesEquipes = new List<Equipe>();
        }

        public void ChargerFichierXml(string nomFichier)
        {
            LesEquipes = new List<Equipe>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["Equipes"];
            XmlNodeList lesEquipesXML = unNoeud.GetElementsByTagName("Equipe");

            foreach (XmlElement unElement in lesEquipesXML)
            {
                LesEquipes.Add(new Equipe(unElement));
            }
        }

        public void SauvegarderFichierXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("Ligue");
            document.AppendChild(racine);

            XmlElement elementEquipe = document.CreateElement("Equipes");
            racine.AppendChild(elementEquipe);

            foreach (Equipe uneEquipe in LesEquipes)
            {
                XmlElement element = uneEquipe.ToXML(document);
                elementEquipe.AppendChild(element);
            }
            document.Save(nomFichier);
        }

        public bool EquipeExiste(string nomEquipe)
        {
            bool equipePresente = false;
            foreach (Equipe equipe in LesEquipes)
            {
                if (equipe.Nom == nomEquipe)
                {
                    equipePresente = true;
                    break;
                }
            }
            return equipePresente;
        }

        public Equipe CreerEquipe(string nomEquipe)
        {
            Equipe? nouvelleEquipe = null;
            if (! EquipeExiste(nomEquipe))
            {
                nouvelleEquipe = new Equipe(nomEquipe);
                LesEquipes.Add(nouvelleEquipe);
            }
            return nouvelleEquipe;
        }
    }
}
