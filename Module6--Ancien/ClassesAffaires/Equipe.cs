using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Utilitaires;

namespace Equipes
{
    public class Equipe : IConversionXML
    {
        public ObservableCollection<string> Joueurs
        {
            set;
            get;
        }

        public string Nom
        {
            set;
            get;
        }

        public Equipe(string nom)
        {
            Nom = nom;
            Joueurs = new ObservableCollection<string>();
        }


        public Equipe(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            Joueurs = new ObservableCollection<string>();
            FromXML(element);
        }

        public override string ToString()
        {
            return Nom;
        }

        private void LireJoueurs(XmlElement element)
        {
        }

        public void AjouterJoueur(string nom)
        {
            Joueurs.Add(nom);
        }

        public void RetirerJoueur(string nom)
        {
            Joueurs.Remove(nom);
        }
        public XmlElement ToXML(XmlDocument doc)
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

        public void FromXML(XmlElement elem)
        {
            XmlNodeList lesJoueurs = elem.GetElementsByTagName("Joueur");
            foreach (XmlElement elementJoueur in lesJoueurs)
            {
                Joueurs.Add(elementJoueur.InnerText);
            }
        }

    }
}
