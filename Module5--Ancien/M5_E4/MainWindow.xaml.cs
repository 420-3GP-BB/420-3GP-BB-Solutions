using System;
using System.Windows;

namespace M5_E4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_champNom.Text))
            {
                MessageBox.Show("Veuillez saisir un nom");
            }
            else
            {
                MessageBox.Show("Bonjour " + _champNom.Text);
            }
        }
    }
}
