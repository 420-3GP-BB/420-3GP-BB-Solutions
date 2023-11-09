using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using Contacts;
using System;

namespace View
{

    /// <summary>
    /// Logique d'interaction pour FenetreCreationContact.xaml
    /// </summary>
    public partial class FenetreCreationContact : Window
    {
        public static RoutedCommand NouveauContact = new RoutedCommand();

        private List<TextBox> _lesTextBox;

        public FenetreCreationContact(Contact contactVide)
        {
            _lesTextBox = new List<TextBox>();
            InitializeComponent();
            DataContext = contactVide;
            _lesTextBox.Add(Nom);
            _lesTextBox.Add(Prenom);
            _lesTextBox.Add(Numero);
            _lesTextBox.Add(Rue);
        }

        private void NouveauContact_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NouveauContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ChampsTextePleins();
        }

        private bool ChampsTextePleins()
        {
            bool reponse = true;
            foreach (TextBox textBox in _lesTextBox)
            {
                if (textBox.Text.Equals(""))
                {
                    reponse = false;
                    break;
                }
            }
            return reponse;
        }

        private void Numero_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texte = Numero.Text;
            if (! Int32.TryParse(texte, out int nombre))
            {
                Numero.Text = "";
            }
        }
    }
}
