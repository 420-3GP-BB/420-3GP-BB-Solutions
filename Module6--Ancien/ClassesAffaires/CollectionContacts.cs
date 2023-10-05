using ClassesAffaire;
using System.Collections;

namespace Contacts
{
    public class CollectionContacts : IElementCourant<Contact>, IEnumerable<Contact>
    {
        private List<Contact> _lesContacts;
        private int _indiceCourant;

        public Contact? ContactCourant
        {
            get => _indiceCourant >= 0 && _indiceCourant < _lesContacts.Count ? _lesContacts[_indiceCourant] : null;
        }

        public bool PrecedentExiste
        {
            get => _indiceCourant > 0;
        }

        public bool ProchainExiste
        {
            get => _indiceCourant < _lesContacts.Count - 1;
         }

        public int Count
        {
            get => _lesContacts.Count;
        }

        public CollectionContacts()
        {
            _lesContacts = new List<Contact>();
            _indiceCourant = -1;
        }

        public void Ajouter(Contact c)
        {
            _lesContacts.Add(c);
        }

        public void AllerAuPremier()
        {
            _indiceCourant = _lesContacts.Count > 0 ? 0 : -1;
        }

        public void AllerAuDernier()
        {
            _indiceCourant = _lesContacts.Count - 1;
        }

        public void AllerAuProchain()
        {
            if (ProchainExiste)
            {
                _indiceCourant++;
            }
        }

        public void AllerAuPrecedent()
        {
            if (PrecedentExiste)
            {
                _indiceCourant--;
            }
        }

        public void RetirerCourant()
        {
            // Le courant doit exister
            if (ContactCourant != null)
            {
                _lesContacts.RemoveAt(_indiceCourant);

                // On replace l'indice s'il est maintenant à l'extérieur de la liste
                if (_indiceCourant > _lesContacts.Count - 1)
                {
                    _indiceCourant = _lesContacts.Count - 1;
                }
            }
        }
        
        // On utilise l'énumérateur de la liste. Pour que les foreach fonctionnent
        public IEnumerator<Contact> GetEnumerator()
        {
            return _lesContacts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _lesContacts.GetEnumerator();
        }
    }
}
