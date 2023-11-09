 using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    // Le ViewModel pour les équipes
    public class ViewModelEquipes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModelEquipes _model;  // Le modèle
        private string? _nomFichier; // Le nom du fichier

        // L'équipe courante
        public Equipe? EquipeActive
        {
            set;
            get;
        }

        // Les propriétés qui sont liées à la vue
        public ObservableCollection<Equipe>? LesEquipes
        {
            // Si on ne veut pas exposer directement la liste du modèle, il y a d'autres possibilités.
            // Par exemple, on pourrait utiliser une List dans le modèle et ici retourner
            // une ObservableCollection construit à partir de la List.
            // Par exemple:
            //      return new ObservableCollection<Equipe>(_model.LesEquipes);
            // Il faudrait alors ajouter un événement pour que la vue soit notifiée des changements
            get
            {
                return _model.LesEquipes;
            }
        }

        // Les joueurs de l'équipe courante
        public ObservableCollection<Joueur>? LesJoueurs
        {
            get
            {
                if (EquipeActive == null)
                {
                    return null;
                }
                else
                {
                    return EquipeActive.LesJoueurs;
                }
            }
        }

        public ViewModelEquipes()
        {
            _model = new ModelEquipes();
            EquipeActive = null;
            _nomFichier = null;
        }

        public void ChargerEquipes(string nomFichier)
        {
            _nomFichier = nomFichier;
            _model.ChargerFichierXml(_nomFichier);
            if (LesEquipes != null && LesEquipes.Count > 0)
            {
                EquipeActive = LesEquipes[0];
            }
            OnPropertyChange("");
        }

        public void ChangerEquipe(Object obj)
        {
            EquipeActive = obj as Equipe;
            OnPropertyChange("");
        }

        public bool EquipeExiste(string nomEquipe)
        {
            return _model.EquipeExiste(nomEquipe);
        }

        public object CreerEquipe(string nomEquipe)
        {
            Equipe nouvelleEquipe = _model.CreerEquipe(nomEquipe);
            SauvegarderDonnees();
            EquipeActive = nouvelleEquipe;
            OnPropertyChange("LesJoueurs");
            return nouvelleEquipe;
        }

        public void CreerFichier(string pathFichier)
        {
            _nomFichier = pathFichier;
            SauvegarderDonnees();
        }

        public void SauvegarderDonnees()
        {
            if (_nomFichier != null)
            {
                _model.SauvegarderFichierXml(_nomFichier);
            }
        }

        // L'ajout et le retrait de joueur aurait pu se faire dans le modèle.
        // Dans ce cas, il aurait fallu passer par une méthode prenant en paramètre une équipe
        // et un nom de joueur.
        public void AjouterJoueurEquipe(string nomJoueur)
        {
            if (EquipeActive != null)
            {
                EquipeActive.AjouterJoueur(nomJoueur);
                SauvegarderDonnees();
            }
        }

        public void RetirerJoueurEquipe(object joueur)
        {
            if (EquipeActive != null)
            {
                Joueur leJoueur = joueur as Joueur;
                EquipeActive.RetirerJoueur(leJoueur.Nom);
                SauvegarderDonnees();
            }
        }

        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}