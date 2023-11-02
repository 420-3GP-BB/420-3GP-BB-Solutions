using System.Collections.ObjectModel;
using System.Xml;
using Utilitaires;

namespace Model
{
    public class Equipe : IXMLSerializable
    {
        public List<Joueur> LesJoueurs
        {
            private set;
            get;
        }

        public string Nom
        {
            private set;
            get;
        }

        public Equipe(string nom)
        {
            Nom = nom;
            LesJoueurs = new List<Joueur>();
        }


        public Equipe(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            LesJoueurs = new List<Joueur>();
            FromXML(element);
        }

        public override string ToString()
        {
            return Nom;
        }

        public void AjouterJoueur(string nom)
        {
            LesJoueurs.Add(new Joueur(nom));
        }

        public void RetirerJoueur(string nom)
        {
            int indice = 0;
            bool trouve = false;

            while(indice < LesJoueurs.Count && ! trouve)
            {
                if (LesJoueurs[indice].Nom.Equals(nom))
                {
                    LesJoueurs.RemoveAt(indice);
                    trouve = true;
                }
                indice++;
            }
        }

        public XmlElement ToXML(XmlDocument doc)
        {
            XmlElement elementEquipe = doc.CreateElement("Equipe");
            elementEquipe.SetAttribute("nom", Nom);
            foreach (Joueur joueur in LesJoueurs)
            {
                string nomJoueur = joueur.Nom;
                XmlElement nouveauJoueur = doc.CreateElement("Joueur");
                nouveauJoueur.InnerText = nomJoueur;
                elementEquipe.AppendChild(nouveauJoueur);
            }
            return elementEquipe;

        }

        public void FromXML(XmlElement elem)
        {
            XmlNodeList lesJoueurs = elem.GetElementsByTagName("Joueur");
            foreach (XmlElement elementJoueur in lesJoueurs)
            {
                LesJoueurs.Add(new Joueur(elementJoueur.InnerText));
            }
        }
    }
}