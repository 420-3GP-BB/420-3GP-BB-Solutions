using System;
using System.Windows;
using System.Windows.Input;
using Model;
using System.Xml;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        // Commande pour affiche le À propos...
        public static RoutedCommand AProprosCmd = new RoutedCommand();

        // Commandes pour le menu Fichier
        public static RoutedCommand OuvrirFichierCmd = new RoutedCommand();
        public static RoutedCommand EnregistrerFichierCmd = new RoutedCommand();
        public static RoutedCommand EnregistrerSousFichierCmd = new RoutedCommand();

        // Commandes pour les boutons
        public static RoutedCommand AllerProchain = new RoutedCommand();
        public static RoutedCommand AllerPrecedent = new RoutedCommand();
        public static RoutedCommand AjouterContact = new RoutedCommand();
        public static RoutedCommand RetirerContact = new RoutedCommand();

        // Objets pour la gestion des contacts
        private ViewModelContacts _viewModel;

        private string _pathFichier;
        private string _dossierImages;
        private string _dossierBase;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        //private string pathFichier;

        public MainWindow()
        {
            _dossierBase = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                          $"Fichiers-3GP";
            _dossierImages = $"{_dossierBase}{DIR_SEPARATOR}Images";

            if (!Directory.Exists(_dossierImages))
            {
                Directory.CreateDirectory(_dossierImages);
            }

            if (!File.Exists($"{_dossierImages}{DIR_SEPARATOR}NoImage.png"))
            {
                File.Copy($"assets{DIR_SEPARATOR}NoImage.png", $"{_dossierImages}{DIR_SEPARATOR}NoImage.png");
            }

            _viewModel = new ViewModelContacts(_dossierImages);



            InitializeComponent();
            DataContext = _viewModel;
        }

        // À propos...
        private void APropos_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Carnet adresses\n Version 1.0");
        }

        private void APropos_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Ouvrir fichier
        private void OuvrirFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OuvrirFichier();
        }

        private void OuvrirFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OuvrirFichier()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = _dossierBase;
            bool? resultat = openFileDialog.ShowDialog();

            if (resultat.HasValue && resultat.Value)
            {
                _pathFichier = openFileDialog.FileName;
                _viewModel.ChargerContacts(_pathFichier);
            }
        }

        // Enregistrer fichier
        private void EnregisterFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.SauvegarderContacts(_pathFichier);
        }

        private void EnregisterFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutEnregistrer;
        }


        // Enregistrer sous...
        private void EnregisterSous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = _dossierBase;
            bool? resultat = saveFileDialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                _pathFichier = saveFileDialog.FileName;
                _viewModel.SauvegarderContacts(_pathFichier);
            }
        }

        private void EnregisterSous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutEnregistrer;
        }

        // Aller au prochain contact
        private void AllerProchain_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AllerAuProchain();
        }

        private void AllerProchain_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.ProchainExiste;
        }

        // Aller au contact précédent
        private void AllerPrecedent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AllerAuPrecedent();
        }

        private void AllerPrecedent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PrecedentExiste;
        }

        // Ajouter un contact
        private void AjouterContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Contact leContact = new Contact();
            FenetreCreationContact fenetre = new FenetreCreationContact(leContact);
            fenetre.Owner = this;
            fenetre.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bool? resultat = fenetre.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                _viewModel.AjouterContact(leContact);            }
        }

        private void AjouterContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Retirer un contact
        private void RetirerContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.RetirerCourant();
        }

        private void RetirerContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.CourantExiste;
        }

        private void BoutonChangerPhoto_Click(object sender, RoutedEventArgs e)
        {
            string dossierImages = $"{_dossierBase}{DIR_SEPARATOR}Images";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.InitialDirectory = dossierImages;
            bool? resultat = openFileDialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                string nomFichier = openFileDialog.FileName;
                string nomFichierSansChemin = Path.GetFileName(nomFichier);
                string nomFichierDestination = $"{dossierImages}{DIR_SEPARATOR}{nomFichierSansChemin}";
                if (! File.Exists(nomFichierDestination))
                {
                    File.Copy(nomFichier, nomFichierDestination, true);
                }
                _viewModel.FichierImage = nomFichierSansChemin;
            }

        }

        // Pour mettre à jour les données du textBox quand on change les valeurs
        private void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }


    }
}
