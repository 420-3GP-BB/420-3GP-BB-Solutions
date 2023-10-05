using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Equipes;

namespace Exercice4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Equipe> lesEquipes;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private string pathFichier;

        public static RoutedCommand AjouterEquipeCmd = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            lesEquipes = new ObservableCollection<Equipe>();
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "equipes.xml"; 
            ChargerFichierXml();
            ComboBoxEquipes.ItemsSource = lesEquipes;
        }

        private void ChargerFichierXml()
        {
            XmlDocument document = new XmlDocument();
                document.Load(pathFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["Equipes"];
            XmlNodeList lesEquipesXML = unNoeud.GetElementsByTagName("Equipe");

            foreach (XmlElement unElement in lesEquipesXML)
            {
                lesEquipes.Add(new Equipe(unElement));
            }

        }

        private void ComboBoxEquipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Equipe equipe = ComboBoxEquipes.SelectedItem as Equipe;
            ListBoxJoueurs.ItemsSource = equipe.Joueurs;
        }

        private void AjouterJoueur_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxEquipes.SelectedItem != null)
            {
                Equipe equipe = ComboBoxEquipes.SelectedItem as Equipe;
                string nom = InputNouveauJoueur.Text;
                equipe.AjouterJoueur(nom);
                SauvegarderXML();
            }           
        }
        private void RetirerJoueur_Click(object sender, RoutedEventArgs e)
        {
            Equipe equipe = ComboBoxEquipes.SelectedItem as Equipe;
            string nomJoueur = ListBoxJoueurs.SelectedItem as string;
            equipe.RetirerJoueur(nomJoueur);
            SauvegarderXML();
        }

        private void SauvegarderXML()
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("Ligue");
            document.AppendChild(racine);

            XmlElement elementEquipe = document.CreateElement("Equipes");
            racine.AppendChild(elementEquipe);

            foreach (Equipe uneEquipe in lesEquipes)
            {
                XmlElement element= uneEquipe.VersXML(document);
                elementEquipe.AppendChild(element);
            }
            document.Save(pathFichier);
        }

        private void AjouterEquipe_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = InputNouvelleEquipe.Text != "";

        }

        private void AjouterEquipe_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Equipe nouvelleEquipe = new Equipe(InputNouvelleEquipe.Text);
            lesEquipes.Add(nouvelleEquipe);
        }
    }
}
