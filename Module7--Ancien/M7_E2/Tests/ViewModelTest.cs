using Model;
using System.Collections.ObjectModel;
using ViewModel;

namespace Tests
{
    public class Tests
    {
        private ViewModelEquipes _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new ViewModelEquipes();
        }

        [Test]
        public void AjoutJoueur()
        {
            // ARRANGE
            string nomJoueur = "JoueurTest 1";
            Equipe nouvelleEquipe = _viewModel.CreerEquipe("EquipeTest 1") as Equipe;
            _viewModel.ChangerEquipe(nouvelleEquipe);

            // ACT
            _viewModel.AjouterJoueurEquipe(nomJoueur);
            Joueur? leJoueur = TrouverJoueur(_viewModel.LesJoueurs, nomJoueur);

            // ASSERT
            Assert.IsNotNull(leJoueur);
        }

        [Test]
        public void EquipeExiste()
        {
            // ARRANGE
            string nomEquipe = "EquipeTest 1";
            _viewModel.CreerEquipe(nomEquipe);

            // ACT
            bool existe = _viewModel.EquipeExiste(nomEquipe);

            // ASSERT
            Assert.IsTrue(existe);
        }

        [Test]
        public void TestPeutPasAjouterEquipeExistante()
        {
            // ARRANGE
            string nomEquipe = "EquipeTest 1";
            _viewModel.CreerEquipe(nomEquipe);
            int nombreEquipes = _viewModel.LesEquipes.Count;

            // ACT
            _viewModel.CreerEquipe(nomEquipe);

            // ASSERT
            Assert.AreEqual(nombreEquipes, _viewModel.LesEquipes.Count);
        }



        private Joueur? TrouverJoueur(ObservableCollection<Joueur> listeJoueurs, string nomJoueur)
        {
            Joueur? joueur = null;
            foreach(Joueur leJoueur in listeJoueurs)
            {
                if (leJoueur.Nom.Equals(nomJoueur))
                {
                    joueur = leJoueur;
                    break;
                }
            }
            return joueur;
        }
    }
}