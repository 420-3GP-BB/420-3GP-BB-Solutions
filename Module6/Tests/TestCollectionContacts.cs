using Contacts;
namespace Tests
{
    public class TestCollectionContacts
    {
        private CollectionContacts lesContacts;

        [SetUp]
        public void Setup()
        {
            lesContacts = new CollectionContacts();
        }

        [Test]
        public void TestAjouterContact()
        {
            // ARRANGE
            Contact c = new Contact();
            c.Nom = "Tremblay";
            c.Prenom = "Jean";
            c.NumeroCivique = 123;
            c.Rue = "Rue des Érables";
            
            // ACT
            lesContacts.Ajouter(c);

            // ASSERT
            Assert.AreEqual(1, lesContacts.Count);
        }

        public void TestSupprimerContact()
        {
            // ARRANGE
            Contact c = new Contact();
            c.Nom = "Tremblay";
            c.Prenom = "Jean";
            c.NumeroCivique = 123;
            c.Rue = "Rue des Érables";
            lesContacts.Ajouter(c);

            // ACT
//            lesContacts.Supprimer(c);

            // ASSERT
//            Assert.AreEqual(0, lesContacts.Count);
        }
    }
}
