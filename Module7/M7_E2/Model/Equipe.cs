using System.Collections.ObjectModel;
using System.Xml;
using Utilitaires;

namespace Model
{
    // Classe qui représente une équipe L'équipe
    // contient simplement une liste de joueurs
    public class Equipe : IConversionXML
    {
        // La liste des joueurs
        public ObservableCollection<Joueur> LesJoueurs
        {
            private set;
            get;
        }

        // Le nom de l'équipe
        public string Nom
        {
            private set;
            get;
        }

        // Constructeur
        // L'équipe doit avoir un nom
        public Equipe(string nom)
        {
            Nom = nom;
            LesJoueurs = new ObservableCollection<Joueur>();
        }

        // Constructeur qui fonctionne avec un élément XML.
        public Equipe(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            LesJoueurs = new ObservableCollection<Joueur>();
            DeXML(element);
        }

        public override string ToString()
        {
            return Nom;
        }

        // Ajoute un joueur à l'équipe
        public void AjouterJoueur(string nom)
        {
            LesJoueurs.Add(new Joueur(nom));
        }

        // Retire un joueur de l'équipe
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

        public XmlElement VersXML(XmlDocument doc)
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

        public void DeXML(XmlElement elem)
        {
            XmlNodeList lesJoueurs = elem.GetElementsByTagName("Joueur");
            foreach (XmlElement elementJoueur in lesJoueurs)
            {
                LesJoueurs.Add(new Joueur(elementJoueur.InnerText));
            }
        }
    }
}