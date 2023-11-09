using System.Collections.ObjectModel;
using System.Xml;

namespace Model
{
    // La classe qui représente le modèle de données.
    // Elle contient les données et les méthodes pour les manipuler.
    // Elle sait comment charger et sauvegarder les données.

    public class ModelEquipes
    {
        // La liste des équipes
       public ObservableCollection<Equipe> LesEquipes
        {
            private set;
            get;
        }

        // Constructeur
        public ModelEquipes()
        {
            LesEquipes = new ObservableCollection<Equipe>();
        }

        // Charge les données
        public void ChargerFichierXml(string nomFichier)
        {
            LesEquipes = new ObservableCollection<Equipe>();
            
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

        // Sauvegarde les données
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

        // Ajoute un joueur à une équipe
        public void AjouterJoueur(Equipe equipe, string nomJoueur)
        {
            equipe.AjouterJoueur(nomJoueur);
        }

        // Retire un joueur d'une équipe
        public void RetirerJoueur(Equipe equipe, string nomJoueur)
        {
            equipe.RetirerJoueur(nomJoueur);
        }

        // Vérifie si une équipe existe déjà
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

        // Crée une équipe
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
