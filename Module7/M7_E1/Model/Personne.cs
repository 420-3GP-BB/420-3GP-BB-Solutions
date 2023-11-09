namespace Model

// Le modèle. Dans cet exemple, ne contient qu'une seule classe, Personne.
// Elle a une seule propriété. Il faut voir comment cette valeur est modifiée
// à partir du ViewModel.

// Le modèle ne sait pas qu'il est utilisé par un ViewModel. Il ne sait pas
// non plus qu'il est utilisé par une vue.
{
    public class Personne
    {
        public string Nom { get; set; }
    }
}