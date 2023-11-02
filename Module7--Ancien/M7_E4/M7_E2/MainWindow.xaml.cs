using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ViewModel;
using System.Xml;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;


namespace M7_E2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Commande pour affiche le À propos...
        public static RoutedCommand AProprosCmd = new RoutedCommand();

        // Commandes pour le menu Fichier
        public static RoutedCommand OuvrirFichierCmd = new RoutedCommand();
        public static RoutedCommand EnregistrerFichierCmd = new RoutedCommand();
        public static RoutedCommand EnregistrerSousFichierCmd = new RoutedCommand();

        // Les commandes pour la gestion de contacts
        public static RoutedCommand AjouterContactCmd = new RoutedCommand();
        public static RoutedCommand RetirerContactCmd = new RoutedCommand();
        public static RoutedCommand NouveauContactCmd = new RoutedCommand();
        public static RoutedCommand AnnulerNouveauCmd = new RoutedCommand();
        public static RoutedCommand AjouterPhotoCmd = new RoutedCommand();

        // Commandes pour les boutons
        public static RoutedCommand AllerProchainCmd = new RoutedCommand();
        public static RoutedCommand AllerPrecedentCmd = new RoutedCommand();

        

        // Objets pour la gestion des contacts

        //private ModelContacts lesContacts;
        private string dossierBase;
        private string pathFichier;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private List<TextBox> champsTexte;
        //private Contact? contactVide;

        private ViewModelContacts _viewModel;


        public MainWindow()
        {
            champsTexte = new List<TextBox>();
            _viewModel = new ViewModelContacts();

            pathFichier = _viewModel.DossierBase + "contacts.xml";
            _viewModel.ChargerContacts(pathFichier);

            InitializeComponent();
            DataContext = _viewModel;


            // Ajout des champs texte pour pouvoir les activer et les désactiver
            champsTexte.Add(Nom);
            champsTexte.Add(Prenom);
            champsTexte.Add(NoCivique);
            champsTexte.Add(Rue);
        }

        // À propos...
        private void APropos_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Carnet adresses\n Version 0.95");
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
            openFileDialog.InitialDirectory = dossierBase;
            bool? resultat = openFileDialog.ShowDialog();

            if (resultat.HasValue && resultat.Value)
            {
                pathFichier = openFileDialog.FileName;
                _viewModel.ChargerContacts(pathFichier);
            }
        }

        // Enregistrer fichier
        private void EnregisterFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutSauvegarder;
        }

        private void EnregisterFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.SauvegarderContacts();
        }

        // Enregistrer sous...
        private void EnregisterSous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutSauvegarderSous;
        }

        private void EnregisterSous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = dossierBase;
            bool? resultat = saveFileDialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                pathFichier = saveFileDialog.FileName;
                _viewModel.SauvegarderSousContacts(pathFichier);
            }
        }

        // Bouton de création de contact
        private void AjouterContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.ContactEnConstruction && ChampsTexteComplets();

        }

        private void AjouterContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AjouterContactConstruction();

        }


        private bool ChampsTexteComplets()
        {
            bool reponse = true;
            foreach (TextBox textBox in champsTexte)
            {
                if (textBox.Text.Equals(string.Empty))
                {
                    reponse = false;
                    break;
                }
            }
            return reponse;
        }

        private void RetirerContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutRetirerContact;
        }

        private void RetirerContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.RetirerContactCourant();
        }

        private void NouveauContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NouveauContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.NouveauContact();
        }

        private void AnnulerContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutAnnulerNouveauContact;
        }

        private void AnnulerContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AnnulerNouveauContact();
        }


        // Aller au prochain contact
        private void AllerProchain_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutAllerAuProchain();
        }

        private void AllerProchain_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AllerAuProchain();
        }

        // Aller au contact précédent
        private void AllerPrecedent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.PeutAllerAuPrecedent();
        }

        private void AllerPrecedent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.AllerAuPrecedent();
        }

        private void AjouterPhoto_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AjouterPhoto_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp, *.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.InitialDirectory = dossierBase;
            bool? resultat = openFileDialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                _viewModel.AjouterPhoto(openFileDialog.FileName);
            }
        }
    }
}
