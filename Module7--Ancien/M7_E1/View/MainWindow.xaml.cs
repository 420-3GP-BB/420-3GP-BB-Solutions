using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Le ViewModel est créé dans le constructeur de la vue.
        // Les valeurs de la vue sont liées aux propriétés du ViewModel.
        private ViewModelPersonne _viewModel;

        public MainWindow()
        {
            _viewModel = new ViewModelPersonne();
            InitializeComponent();

            // Le DataContext de la vue est lié au ViewModel.
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Dans cet exemple, le champ _saisieNom est utilisé simplement dans le but de saisir
            // une valeur. Il n'est pas lié à une propriété du ViewModel.

            // Il sert toutefois à modifier la propriété Nom du ViewModel.
            if (_saisieNom.Text != "")
            {
                _viewModel.ChangerNom(_saisieNom.Text);
                _saisieNom.Text = "";
            }
        }
    }
}
