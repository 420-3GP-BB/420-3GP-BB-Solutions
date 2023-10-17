using Contacts;

namespace TestsAffaire
{
    public class TestCollectionContacts
    {
        private CollectionContacts _contacts;


        [SetUp]
        public void Setup()
        {
            _contacts = new CollectionContacts();
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



    }
}