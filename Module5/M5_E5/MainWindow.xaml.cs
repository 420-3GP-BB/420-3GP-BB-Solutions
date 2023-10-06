using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace M5_E5
{
    public partial class MainWindow : Window
    {
        private string _pathFichier;  // Chemin du fichier ouvert

        public MainWindow()
        {
            InitializeComponent();
            _pathFichier = "";
        }

        // Vide le contenu du fichier
        private void BoutonVider_Click(object sender, RoutedEventArgs e)
        {
            _contenuFichier.Text = "";
        }

        // Ouvre un fichier
        private void ChargerFichier_Click(object sender, RoutedEventArgs e)
        {
            bool? result;
            OpenFileDialog fileDialog = new OpenFileDialog();

            result = fileDialog.ShowDialog();

            if (result.Value)
            {
                _pathFichier = fileDialog.FileName;
                _contenuFichier.Text = File.ReadAllText(_pathFichier);
            }
        }

        // Sauvegarde le fichier. Permet de donner un nouveau nom
        private void SauvegarderSous_Click(object sender, RoutedEventArgs e)
        {
            bool? result;
            SaveFileDialog fileDialog = new SaveFileDialog();
            result = fileDialog.ShowDialog();

            if(result.Value)
            {
                _pathFichier = fileDialog.FileName;
                SauvegarderFichier();
            }
        }

        // Sauvegarde le fichier. Utilise le nom du fichier ouvert.
        // Si aucun fichier n'est ouvert, appelle SauvegarderSous_Click
        private void Sauvegarder_Click(object sender, RoutedEventArgs e)
        {
            if (_pathFichier.Equals(""))
            {
                SauvegarderSous_Click(sender, e);
            }
            else
            {
                SauvegarderFichier();
            }
        }

        // Sauvegarde le fichier
        private void SauvegarderFichier()
        {
            File.WriteAllText(_contenuFichier.Text, _pathFichier);
        }
    }
}
