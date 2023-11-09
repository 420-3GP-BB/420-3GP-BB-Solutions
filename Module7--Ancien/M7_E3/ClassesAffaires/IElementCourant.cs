namespace ClassesAffaire
{
    public interface IElementCourant<T>
    {
        // Retourne l'élément courant ou null s'il n'y en a pas
        public T? ContactCourant { get; }
        // Retourne vrai si un élément précédent existe
        public bool PrecedentExiste { get; }
        // Retourne vrai si un élément suivant existe
        public bool ProchainExiste { get; }
        // Ajoute un élément à la fin de la collection
        // L'élément courant devient le nouvel élément
        public void Ajouter(T element);
        // Retire l'élément courant
        public void RetirerCourant();
        /// Va au premier élément
        public void AllerAuPremier();
        /// Va au dernier élément
        public void AllerAuDernier();
        /// Va à l'élément suivant s'il existe
        public void AllerAuProchain();
        /// Va à l'élément précédent s'il existe
        public void AllerAuPrecedent();
    }
}
