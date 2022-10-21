﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Contacts;
using System.Xml;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

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
        public static RoutedCommand FermerFichierCmd = new RoutedCommand();

        // Commandes pour les boutons
        public static RoutedCommand AllerProchain = new RoutedCommand();
        public static RoutedCommand AllerPrecedent = new RoutedCommand();

        // Objets pour la gestion des contacts
        private List<Contact> lesContacts;
        private string dossierBase;
        private string pathFichier;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private int indiceCourant;
        private List<TextBox> champsTexte;

        public MainWindow()
        {
            champsTexte = new List<TextBox>();
            lesContacts = new List<Contact>();
            InitializeComponent();
            dossierBase = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                          $"Fichiers-3GP";
            pathFichier = dossierBase + DIR_SEPARATOR + "contacts.xml";

            // Ajout des champs texte pour pouvoir les activer et les désactiver
            champsTexte.Add(Nom);
            champsTexte.Add(Prenom);
            champsTexte.Add(NoCivique);
            champsTexte.Add(Rue);

            ChargerContacts(pathFichier);
            indiceCourant = 0;
            if (lesContacts.Count > 0)
            {
                DataContext = lesContacts[0];
                ActiverChampsTexte(false);
            }
        }

        private void ActiverChampsTexte(bool actif)
        {
            foreach(TextBox t in champsTexte)
            {
                t.IsReadOnly = ! actif;
            }
        }

        // À propos...
        private void APropos_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Carnet adresses\n Version 0.9");
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
                ChargerContacts(pathFichier);
            }
        }

        private void ChargerContacts(string nomFichier)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlNodeList contacts = doc.DocumentElement.GetElementsByTagName("contact");
            lesContacts = new List<Contact>();
            foreach (XmlElement c in contacts)
            {
                lesContacts.Add(new Contact(c));
            }
        }

        // Enregistrer fichier
        private void EnregisterFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SauvegarderContacts(pathFichier);
        }

        private void EnregisterFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lesContacts.Count > 0;
        }

        private void SauvegarderContacts(string nomFichier)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement racine = doc.CreateElement("contact");
            doc.AppendChild(racine);
            foreach (Contact c in lesContacts)
            {
                racine.AppendChild(c.ToXML(doc));
            }
            doc.Save(nomFichier);
        }

        // Enregistrer sous...
        private void EnregisterSous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = dossierBase;
            bool? resultat = saveFileDialog.ShowDialog();
            if (resultat.HasValue && resultat.Value)
            {
                pathFichier = saveFileDialog.FileName;
                SauvegarderContacts(pathFichier);
            }

        }

        private void EnregisterSous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lesContacts.Count > 0;
        }

        // Fermer fichier
        private void FermerFichier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lesContacts.Clear();
        }

        private void FermerFichier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lesContacts.Count > 0;
        }

        // Aller au prochain contact
        private void AllerProchain_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            indiceCourant++;
            DataContext = lesContacts[indiceCourant];
        }

        private void AllerProchain_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lesContacts.Count > 0 &&
                indiceCourant < lesContacts.Count - 1;
        }

        // Aller au contact précédent
        private void AllerPrecedent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            indiceCourant--;
            DataContext = lesContacts[indiceCourant];
        }

        private void AllerPrecedent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lesContacts.Count > 0 && 
                indiceCourant > 0;
        }
    }
}