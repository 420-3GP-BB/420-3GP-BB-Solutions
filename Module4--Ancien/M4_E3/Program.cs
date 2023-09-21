using System.Xml;

// Programme qui illustre l'ajout d'un élément dans un fichier XML

char DIR_SEPARATOR = Path.DirectorySeparatorChar;

string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                     $"Fichiers-3GP{DIR_SEPARATOR}contacts.xml";

// On vérifie si le fichier contacts.xml existe 
if (!File.Exists(pathFichier))
{
    Console.Error.WriteLine($"Le fichier {pathFichier} n'existe pas");
    System.Environment.Exit(1);
}

// Chargement du fichier
XmlDocument doc = new XmlDocument();
doc.Load(pathFichier);

// Récupération de la racine
XmlElement root = doc.DocumentElement;

// Création des éléments
XmlElement elementContact = doc.CreateElement("contact");
elementContact.SetAttribute("nom", "Van Houten");
elementContact.SetAttribute("prenom", "Milhouse");

XmlElement elementAdresse = doc.CreateElement("adresse");
elementAdresse.SetAttribute("numero", "316");
elementAdresse.SetAttribute("rue", "Pikeland Ave.");

XmlElement elementDescription = doc.CreateElement("description");
elementDescription.InnerText = "Milhouse est le meilleur ami de Bart Simpson et est désespérément amoureux de sa sœur Lisa Simpson.";

// Construction de la structure
elementContact.AppendChild(elementAdresse);
elementContact.AppendChild(elementDescription);
root.AppendChild(elementContact);

// Sauvegarde du fichier
doc.Save(pathFichier);