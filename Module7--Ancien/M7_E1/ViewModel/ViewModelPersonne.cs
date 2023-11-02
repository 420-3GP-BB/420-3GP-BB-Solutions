using Model;
using System.ComponentModel;

namespace ViewModel
{
    public class ViewModelPersonne : INotifyPropertyChanged
    {
        private Personne _personne;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Nom 
        { 
            get => _personne.Nom;
            set
            {
                _personne.Nom = value;
                OnPropertyChanged(nameof(Nom));
            }
        }

        public ViewModelPersonne()
        {
            _personne = new Personne();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
 