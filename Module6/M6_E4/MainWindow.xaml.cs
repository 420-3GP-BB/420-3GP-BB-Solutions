﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using Contacts;
using System.IO;
using System.Xml;

namespace M6_E4
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

        // Commandes pour les boutons
        public static RoutedCommand AllerProchain = new RoutedCommand();
        public static RoutedCommand AllerPrecedent = new RoutedCommand();

        // Objets pour la gestion des contacts

        private CollectionContacts _lesContacts;
        private string _pathFichier;
        private string _dossierBase;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        //private string pathFichier;

        public MainWindow()
        {
            _pathFichier = "";
            _dossierBase = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                          $"Fichiers-3GP";
            //pathFichier = dossierBase + DIR_SEPARATOR + "contacts.xml";
            _lesContacts = new CollectionContacts(); // La collection doit être créée avant
                                                     // l'initialisation des composants
            InitializeComponent();
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
                ChargerContacts(_pathFichier);
            }
        }

        private void ChargerContacts(string nomFichier)
        {
            if (!File.Exists(nomFichier))
            {
                return;
            }

            _lesContacts = new CollectionContacts();
            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlNodeList contacts = doc.DocumentElement.GetElementsByTagName("contact");
            foreach (XmlElement c in contacts)
            {
                _lesContacts.Ajouter(new Contact(c));
            }
            _lesContacts.AllerAuPremier();
        }

        // Enregistrer fichier
        private void EnregisterFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SauvegarderContacts(_pathFichier);
        }

        private void EnregisterFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _lesContacts.Count > 0;
        }

        private void SauvegarderContacts(string nomFichier)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement racine = doc.CreateElement("contact");
            doc.AppendChild(racine);
            foreach (Contact c in _lesContacts)
            {
                racine.AppendChild(c.VersXML(doc));
            }
            doc.Save(nomFichier);
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
                SauvegarderContacts(_pathFichier);
            }
        }

        private void EnregisterSous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _lesContacts.Count > 0;
        }

        // Aller au prochain contact
        private void AllerProchain_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _lesContacts.AllerAuProchain();
        }

        private void AllerProchain_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _lesContacts.ProchainExiste;
        }

        // Aller au contact précédent
        private void AllerPrecedent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _lesContacts.AllerAuPrecedent();
        }

        private void AllerPrecedent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _lesContacts.PrecedentExiste;
        }
    }
}