using System.Reflection.Metadata;
using System.Xml;

namespace Contacts
{
    public class TestContact
    {
        private string nom = "Simpson";
        private string prenom = "Homer";
        private int numero = 742;
        private string rue = "Evergreen Terrace";
        private string description = "Homer Jay Simpson est le principal personnage fictif de la série télévisée d'animation Les Simpson et le père de la famille du même nom.";

        // Le contact qu'on va tester
        private string contact;
            

        private Contact? leContact;

        [SetUp]
        public void Setup()
        {
            // On essaie autant que possible d'éviter l'ouverture de fichiers dans le cadre des tests.
            contact = @$"<contact nom=""{nom}"" prenom=""{prenom}"">
                <adresse numero = ""{numero}"" rue=""{rue}"" />
                <description>{description}</description>
              </contact>";
        }

        [Test]
        public void TestChargeNom()
        {
            // ARRANGE
            // ACT
            ChargerContactXML();

            // ASSERT
            Assert.AreEqual(nom, leContact.Nom);
        }

        void ChargerContactXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(contact);
            leContact = new Contact(doc.DocumentElement);
        }

        [Test]
        public void TestChargerPrenom()
        {
            // ARRANGE
            // ACT
            ChargerContactXML();

            // ASSERT
            Assert.AreEqual(prenom, leContact.Prenom);
        }

        [Test]
        public void TestChargerNumeroCivique()
        {
            // ARRANGE
            // ACT
            ChargerContactXML();

            // ASSERT
            Assert.AreEqual(numero, leContact.NumeroCivique.Value);

        }

        [Test]
        public void TestChargerRue()
        {
            // ARRANGE
            // ACT
            ChargerContactXML();

            // ASSERT
            Assert.AreEqual(rue, leContact.Rue);

        }

        [Test]
        public void TestChargerDescription()
        {
            // ARRANGE
            // ACT
            ChargerContactXML();

            // ASSERT
            Assert.AreEqual(description, leContact.Description);
        }
    }
}