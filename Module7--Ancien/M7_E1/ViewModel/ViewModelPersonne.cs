using Model;
using System.ComponentModel;

namespace ViewModel
{
    public class ViewModelPersonne : INotifyPropertyChanged
    {
        private Personne _personne;

        // On implémente l'interface INotifyPropertyChanged pour que la vue soit notifiée
        // des changements de propriétés du ViewModel.
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Nom 
        { 
            get => _personne.Nom;
            private set
            {
                _personne.Nom = value;
                // IMPORTANT: On notifie la vue que la propriété a changé. Sinon, la vue ne sera pas mise à jour.
                OnPropertyChanged(nameof(Nom));
            }
        }

        // Dans le constructeur, on crée le modèle. Dans cet exemple, le modèle est une classe Personne.
        // On pourrait aussi avoir un modèle qui contient plusieurs classes.
        public ViewModelPersonne()
        {
            _personne = new Personne();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangerNom(string nouveauNom)
        {
            // On vérifie que le nouveau nom est différent de l'ancien 
            // avant de modifier la propriété. Il ne sert à rien de notifier
            // la vue si la valeur n'a pas changé.
            if (nouveauNom != Nom)
            {
                Nom = nouveauNom;
            }
        }
    }
}
 