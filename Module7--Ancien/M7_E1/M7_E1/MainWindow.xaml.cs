using System.Windows;
using ViewModel;

namespace M7_E1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelPersonne _viewModel;

        public MainWindow()
        {
            _viewModel = new ViewModelPersonne();
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_saisieNom.Text != "")
            {
                _viewModel.Nom = _saisieNom.Text;
                _saisieNom.Text = "";
            }
        }
    }
}
