namespace Model
{
    // Classe qui représente un joueur
    // Le joueur n'a qu'un nom. Ce qui fait que cette
    // classe est très simple.
    public class Joueur
    {
        // Le nom du joueur
        public string Nom
        {
            private set;
            get;
        }

        // Constructeur
        public Joueur(string nom)
        {
            Nom = nom;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
