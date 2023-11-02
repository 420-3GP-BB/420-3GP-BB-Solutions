using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class ViewModelEquipes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModelEquipes _model;
        private Equipe? _equipeCourante;
        private string? _nomFichier;

        public ObservableCollection<Equipe>? LesEquipes
        {
            get
            {
                // Si on veut éviter la copie, on peut:
                // 1) Mettre une ObservableCollection dans le model et rediriger vers cette liste (pas l'idéal au nivau design)
                // 2) Conserver une copie dans le ViewModel et dans le Model (prend plus d'espace, plus de gestion)
                // 3) Faire une liste qui implémente l'interface INotifyCollectionChanged (code plus complexe)
                //
                // L'important est de remarquer que peu importe la décision prise, c'est transparent pour
                // la vue.
                return new ObservableCollection<Equipe>(_model.LesEquipes);
            }
        }

        public ObservableCollection<Joueur>? LesJoueurs
        {
            get
            {
                if (_equipeCourante == null)
                {
                    return null;
                }
                else
                {
                    return new ObservableCollection<Joueur>(_equipeCourante.LesJoueurs);
                }
            }
        }


        public ViewModelEquipes()
        {
            _model = new ModelEquipes();
            _equipeCourante = null;
            _nomFichier = null;
        }

        public void ChargerEquipes(string nomFichier)
        {
            _nomFichier = nomFichier;
            _model.ChargerFichierXml(_nomFichier);
            OnPropertyChange("LesEquipes");
        }

        public void ChangerEquipe(Object obj)
        {
            _equipeCourante = obj as Equipe;
            OnPropertyChange("LesJoueurs");
        }

        public bool EquipeExiste(string nomEquipe)
        {
            return _model.EquipeExiste(nomEquipe);
        }

        public object CreerEquipe(string nomEquipe)
        {
            Equipe nouvelleEquipe = _model.CreerEquipe(nomEquipe);
            SauvegarderDonnees();
            OnPropertyChange("LesEquipes");
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
            if (_equipeCourante != null)
            {
                _equipeCourante.AjouterJoueur(nomJoueur);
                SauvegarderDonnees();
                OnPropertyChange("LesJoueurs");
            }
        }

        public void RetirerJoueurEquipe(object joueur)
        {
            if (_equipeCourante != null)
            {
                Joueur leJoueur = joueur as Joueur;
                _equipeCourante.RetirerJoueur(leJoueur.Nom);
                SauvegarderDonnees();
                OnPropertyChange("LesJoueurs");
            }
        }


        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}