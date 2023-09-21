using System.Xml;

namespace Contacts
{
    public class TestContact
    {
        private string nom = "Simpson";
        private string prenom = "Homer";
        private string numero = "742";
        private string rue = "Evergreen Terrace";
        private string description = "Homer Jay Simpson est le principal personnage fictif de la s�rie t�l�vis�e d'animation Les Simpson et le p�re de la famille du m�me nom.";

        // Le contact qu'on va tester
        private string contact;
            

        private Contact? leContact;

        [SetUp]
        public void Setup()
        {
            // On essaie autant que possible d'�viter l'ouverture de fichiers dans le cadre des tests.
            // Le @ devant la chaine de caract�res permet d'�viter d'avoir � �chapper les guillemets
            // et de faire une chaine de caract�res sur plusieurs lignes
            contact = @$"<contact nom=""{nom}"" prenom=""{prenom}"">
                            <adresse numero = ""{numero}"" rue=""{rue}"" />
                            <description>{description}</description>
                         </contact>";

        }

        void ChargerContactXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(contact);
            leContact = new Contact(doc.DocumentElement);
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
            Assert.AreEqual(Int32.Parse(numero), leContact.NumeroCivique);

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