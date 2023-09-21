using System.Xml;
using System.Xml.Linq;
using Contacts;
using Utilitaires;

char DIR_SEPARATOR = Path.DirectorySeparatorChar;

string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                     $"Fichiers-3GP{DIR_SEPARATOR}contacts.xml";

// On vérifie si le fichier contacts.xml existe 
if (!File.Exists(pathFichier))
{
    Console.Error.WriteLine($"Le fichier {pathFichier} n'existe pas");
    System.Environment.Exit(1);
}

List<Contact> contacts = LireContacts(pathFichier);
AfficherContacts(contacts);
SauvegarderContacts(contacts, pathFichier);


// Lit le fichier XML et met les contacts dans la liste
List<Contact> LireContacts(string pathFichier) 
{
    // Chargement du fichier
    XmlDocument doc = new XmlDocument();
    doc.Load(pathFichier);

    XmlElement root = doc.DocumentElement;

    List<Contact> lesContacts = new List<Contact>();
    XmlNodeList noeudsContacts = root.GetElementsByTagName("contact");
    foreach(XmlNode noeud in noeudsContacts)
    {
        XmlElement elementContact = noeud as XmlElement;
        lesContacts.Add(new Contact(elementContact));
    }
    return lesContacts;
}

// Affiche les contacts dans la liste
void AfficherContacts(List<Contact> lesContacts)
{
    foreach(Contact contact in lesContacts)
    {
        Console.WriteLine(contact.ToString());
        Console.WriteLine();
    }
}

// Sauvegarde la liste de contacts de la liste dans un fichier xml
void SauvegarderContacts(List<Contact> lesContacts, string pathFichier)
{
    XmlDocument document = new XmlDocument();
    XmlElement racine = document.CreateElement("contacts");
    document.AppendChild(racine);
    
    foreach(Contact c in lesContacts)
    {
        XmlElement element = c.VersXML(document);
        racine.AppendChild(element);
    }
    document.Save(pathFichier);
}
