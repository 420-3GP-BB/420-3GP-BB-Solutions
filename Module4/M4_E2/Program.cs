using System.Xml;

// Programme qui permet de lire le fichier contacts.xml et d'afficher des informations

char DIR_SEPARATOR = Path.DirectorySeparatorChar;

string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                     $"Fichiers-3GP{DIR_SEPARATOR}contacts.xml";

// On vérifie si le fichier contacts.xml existe 
if (! File.Exists(pathFichier))
{
    Console.Error.WriteLine($"Le fichier {pathFichier} n'existe pas");
    System.Environment.Exit(1);   // On quitte le programme avec un code d'erreur
}


// Chargement du fichier
XmlDocument document = new XmlDocument();
document.Load(pathFichier);


// Récupération de la racine
XmlElement root = document.DocumentElement;

root.AppendChild(exemple);

// Affichage du nombre d'éléments avec la balise contact
XmlNodeList elements = root.GetElementsByTagName("contact");
Console.WriteLine($"Il y a {elements.Count} contacts dans le fichier.");

// Affichage du nombre de contacts qui ont Simpson comme nom de famille
int nombreSimpsons = 0;
foreach (XmlElement element in elements)
{
    if (element.GetAttribute("nom").Equals("Simpson"))
    {
        nombreSimpsons++;
    }
}

Console.WriteLine($"Il y a {nombreSimpsons} Simpson dans les contacts.");

// On affiche l'adresse de Ned Flanders
XmlElement? elementNed = null;
foreach (XmlElement element in elements)
{
    if (element.GetAttribute("nom").Equals("Flanders")
        && element.GetAttribute("prenom").Equals("Ned"))
    {
        elementNed = element;
        break;
    }
}

if (elementNed != null)
{
    XmlElement adresse = elementNed["adresse"];
    string numero = adresse.GetAttribute("numero");
    string rue = adresse.GetAttribute("rue");
    Console.WriteLine($"L'adresse de Ned Flanders est {numero} {rue}.");
}

// Affichage de la description de monsieur Burns
XmlElement? elementBurns = null;
foreach (XmlElement element in elements)
{
    if (element.GetAttribute("nom").Equals("Burns")
        && element.GetAttribute("prenom").Equals("Charles Montgomery"))
    {
        elementBurns = element;
        break;
    }
}

if (elementBurns != null)
{
    XmlElement description = elementBurns["description"];
    Console.WriteLine($"La description de monsier Burns est:\n{description.InnerText.Trim()}");
}
