using System.Collections.ObjectModel;
using System.Xml;
using Utilitaires;

namespace Equipes
{
    /// <summary>
    /// Classe représentant une équipe.
    /// </summary>
    public class Equipe : IConversionXML
    {
        /// <summary>
        /// Les joueurs de l'équipe.
        /// </summary>
        public ObservableCollection<string> Joueurs
        {
            set;
            get;
        }

        /// <summary>
        /// Le nom de l'équipe.
        /// </summary>
        public string Nom
        {
            set;
            get;
        }

        /// <summary>
        /// Constructeur de l'équipe. Il faut un  nom.
        /// </summary>
        /// <param name="nom">Le nom de l'équipe</param>
        public Equipe(string nom)
        {
            Nom = nom;
            Joueurs = new ObservableCollection<string>();
        }

        /// <summary>
        /// Constructeur de l'équipe à partir d'un élément XML.
        /// </summary>
        /// <param name="element">L'élément XML qui représente l'équipe</param>
        public Equipe(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            Joueurs = new ObservableCollection<string>();
            DeXML(element);
        }

        public override string ToString()
        {
            return Nom;
        }

        /// <summary>
        /// Ajoute un joueur à l'équipe.
        /// </summary>
        /// <param name="nom">Le nom du joueur</param>
        public void AjouterJoueur(string nom)
        {
            Joueurs.Add(nom);
        }

        /// <summary>
        /// Retire un joueur de l'équipe.
        /// </summary>
        /// <param name="nom">Le nom du joueur</param>
        public void RetirerJoueur(string nom)
        {
            Joueurs.Remove(nom);
        }
        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement elementEquipe = doc.CreateElement("Equipe");
            elementEquipe.SetAttribute("nom", Nom);
            foreach (string nomJoueur in Joueurs)
            {
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
                Joueurs.Add(elementJoueur.InnerText);
            }
        }
    }
}
