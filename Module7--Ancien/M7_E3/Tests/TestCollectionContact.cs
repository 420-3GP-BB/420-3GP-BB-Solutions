using Model;
using ViewModel;

namespace Tests
{
    public class TestCollectionContacts
    {
        private CollectionContacts _contacts;

        [SetUp]
        public void Setup()
        {
            _contacts = new CollectionContacts();
        }

        private Contact[] CreerBanqueContacts(int nombre)
        {
            Contact[] banqueContacts = new Contact[nombre];
            for (int i = 0; i < banqueContacts.Length; i++)
            {
                banqueContacts[i] = new Contact();
            }
            return banqueContacts;
        }

        [Test]
        public void TestCourantInitialementNull()
        {
            // ARRANGE

            // ACT
            Contact? courant = _contacts.ContactCourant;

            // ASSERT
            Assert.IsNull(courant);
        }

        [Test]
        public void TestAjouterUnElement()
        {
            // ARRANGE
            Contact nouveau = new Contact();
            _contacts.Ajouter(nouveau);

            // ACT
            Contact? courant = _contacts.ContactCourant;

            // ASSERT
            Assert.AreSame(nouveau, courant);
        }

        [Test]
        public void TestAjouterDeuxElements()
        {
            // ARRANGE
            Contact premier = new Contact();
            Contact deuxieme = new Contact();
            _contacts.Ajouter(premier);
            _contacts.Ajouter(deuxieme);

            // ACT
            Contact? courant = _contacts.ContactCourant;

            // ASSERT
            Assert.AreSame(deuxieme, courant);
        }

        [Test]
        public void TestRetirerTous()
        {
            // ARRANGE
            Contact[] banque = CreerBanqueContacts(4);
            foreach (Contact contact in banque)
            {
                _contacts.Ajouter(contact);
            }


            // ACT
            for (int i = 0; i < banque.Length; i++)
            {
                _contacts.RetirerCourant();

            }

            Contact courant = _contacts.ContactCourant;

            // ASSERT
            Assert.IsNull(courant);
        }

        [Test]
        public void TestRetirerDernier()
        {
            // ARRANGE
            Contact[] banque = CreerBanqueContacts(4);
            foreach (Contact contact in banque)
            {
                _contacts.Ajouter(contact);
            }


            // ACT
            _contacts.RetirerCourant();

            Contact courant = _contacts.ContactCourant;

            // ASSERT
            Assert.AreSame(banque[2], courant);
        }

        [Test]
        public void TestRetirerMilieu()
        {
            // ARRANGE
            Contact[] banque = CreerBanqueContacts(4);
            foreach (Contact contact in banque)
            {
                _contacts.Ajouter(contact);
            }


            // ACT
            _contacts.AllerAuPremier();
            _contacts.AllerAuProchain();
            _contacts.RetirerCourant();
            Contact courant = _contacts.ContactCourant;

            // ASSERT
            Assert.AreSame(banque[2], courant);
        }

        [Test]
        public void TestAllerAuProchain()
        {
            // ARRANGE
            Contact[] banque = CreerBanqueContacts(4);
            foreach (Contact contact in banque)
            {
                _contacts.Ajouter(contact);
            }
            _contacts.AllerAuPremier();
            Assert.IsFalse(_contacts.PrecedentExiste);

            // ACT
            Contact courant = _contacts.ContactCourant;
            // ASSERT
            Assert.AreSame(banque[0], courant);
            for (int i = 1; i < banque.Length; i++)
            {
                _contacts.AllerAuProchain();
                courant = _contacts.ContactCourant;
                Assert.AreSame(banque[i], courant);
            }

            Assert.IsFalse(_contacts.ProchainExiste);

        }


    }
}