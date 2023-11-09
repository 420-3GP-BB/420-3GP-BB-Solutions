using Model;
using System.ComponentModel;
using System.Xml;

namespace ViewModel
{
    public class ViewModelContacts : INotifyPropertyChanged
    {
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private string _dossierImages;

        public event PropertyChangedEventHandler? PropertyChanged;

        // La collection de contacts qui est maintenant cachée dans le ViewModel
        private CollectionContacts _lesContacts;

        // Redirection vers la collection de contacts
        // On cache ici le contact courant
        private Contact? ContactCourant
        {
            get => _lesContacts.ContactCourant;
        }

        // Le nom du contact courant
        public string Nom
        {
            get
            {
                return ContactCourant?.Nom ?? "";
            }

            set
            {
                if (ContactCourant != null)
                {
                    ContactCourant.Nom = value;
                }
            }
        }

        // Le prénom du contact courant
        public string Prenom
        {
            get
            {
                return ContactCourant?.Prenom ?? "";
            }

            set
            {
                if (ContactCourant != null)
                {
                    ContactCourant.Prenom = value;
                }
            }
        }

        // Le numéro de l'adresse du contact courant
        public int? NumeroCivique
        {
            get
            {
                return ContactCourant?.NumeroCivique;
            }

            set
            {
                if (ContactCourant != null)
                {
                    ContactCourant.NumeroCivique = value;
                }
            }
        }

        // La rue de l'adresse du contact courant
        public string Rue
        {
            get
            {
                return ContactCourant?.Rue ?? "";
            }

            set
            {
                if (ContactCourant != null)
                {
                    ContactCourant.Rue = value;
                }
            }
        }

        public string FichierImage
        {
            get
            {
                if (ContactCourant != null && ! String.IsNullOrEmpty(ContactCourant.FichierImage))
                {
                    return Path.Combine(_dossierImages,ContactCourant.FichierImage);
                }
                else
                {
                    return Path.Combine(_dossierImages, "NoImage.png");
                }
            }

            set
            {
                if (ContactCourant != null)
                {
                    ContactCourant.FichierImage = value;
                    OnPropertyChanged("FichierImage");
                }
            }
        }

        // Redirection vers la collection de contacts
        public bool PrecedentExiste
        {
            get => _lesContacts.PrecedentExiste;
        }

        // Redirection vers la collection de contacts
        public bool ProchainExiste
        {
            get => _lesContacts.ProchainExiste;
        }
        public bool PeutEnregistrer 
        { 
            get => _lesContacts.Count > 0;
        }
        public bool CourantExiste { get => ContactCourant != null; }

        public ViewModelContacts(string dossierImages)
        {
            _dossierImages = dossierImages;
            _lesContacts = new CollectionContacts();
        }

        public void ChargerContacts(string nomFichier)
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
            OnPropertyChanged("");
        }

        public void SauvegarderContacts(string nomFichier)
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

        public void AllerAuProchain()
        {
            _lesContacts.AllerAuProchain();
            OnPropertyChanged("");
        }

        public void AllerAuPrecedent()
        {             
            _lesContacts.AllerAuPrecedent();
            OnPropertyChanged("");
        }


        // Pour que la vue soit notifiée des changements
        private void OnPropertyChanged(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }

        public void RetirerCourant()
        {
            _lesContacts.RetirerCourant();
            OnPropertyChanged("");
        }

        public void AjouterContact(Contact leContact)
        {
            _lesContacts.Ajouter(leContact);
            OnPropertyChanged("");
        }
    }
}