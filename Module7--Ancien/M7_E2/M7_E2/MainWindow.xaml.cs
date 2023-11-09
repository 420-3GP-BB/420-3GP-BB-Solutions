using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using System.IO;
using ViewModel;

namespace M7_E2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private string pathFichier;   // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir



        public static RoutedCommand AjouterEquipeCmd = new RoutedCommand();
        public static RoutedCommand AjouterJoueurCmd = new RoutedCommand();
        public static RoutedCommand RetirerJoueurCmd = new RoutedCommand();


        private ViewModelEquipes _viewModel;

        public MainWindow()
        {
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "equipes.xml";

            _viewModel = new ViewModelEquipes();
            InitializeComponent();
            DataContext = _viewModel;

            if (File.Exists(pathFichier))
            {
                _viewModel.ChargerEquipes(pathFichier);
            }
            else
            {
                // On demande au ViewModel de créer le fichier
                _viewModel.CreerFichier(pathFichier);
            }
        }


        // Il faut noter que les actions de l'interface ne font qu'une redirection 
        // vers le ViewModel.

        // Aucune logique d'affaire ne doit être présente dans l'interface. Il n'y a que de la logique d'interface.

        private void ComboBoxEquipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.ChangerEquipe(ComboBoxEquipes.SelectedItem);
        }

        private void AjouterEquipe_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = InputNouvelleEquipe.Text != "" && ! _viewModel.EquipeExiste(InputNouvelleEquipe.Text);
        }

        private void AjouterEquipe_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            object equipe = _viewModel.CreerEquipe(InputNouvelleEquipe.Text);
            ComboBoxEquipes.SelectedItem = equipe;
            InputNouvelleEquipe.Text = "";
        }

        private void AjouterJoueur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ComboBoxEquipes.SelectedItem != null && InputNouveauJoueur.Text != "";
        }

        private void AjouterJoueur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AjouterJoueurEquipe(InputNouveauJoueur.Text);
            InputNouveauJoueur.Text = "";
        }

        private void RetirerJoueur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ListBoxJoueurs.SelectedItem != null;
        }

        private void RetirerJoueur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.RetirerJoueurEquipe(ListBoxJoueurs.SelectedItem);
        }
    }
}
