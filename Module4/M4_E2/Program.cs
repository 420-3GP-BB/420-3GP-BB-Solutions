using System.Xml.Linq;

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
XElement racine = XElement.Load(pathFichier);

int nombreContacts = racine.Elements("contact").Count();
Console.WriteLine($"Il y a {nombreContacts} contacts dans le fichier.");

int nombreSimpsons = racine.Elements("contact")
                           .Where(c => c.Attribute("nom").Value == "Simpson")
                           .Count();
Console.WriteLine($"Il y a {nombreSimpsons} Simpson dans les contacts.");

XElement? elementNed = racine.Elements("contact")
                            .Where(c => c.Attribute("nom").Value == "Flanders"
                                        && c.Attribute("prenom").Value == "Ned")
                            .FirstOrDefault();
if (elementNed != null)
{
    string numero = elementNed.Element("adresse").Attribute("numero").Value;
    string rue = elementNed.Element("adresse").Attribute("rue").Value;
    Console.WriteLine($"L'adresse de Ned Flanders est {numero} {rue}.");
}

XElement? elementBurns= racine.Elements("contact")
                             .Where(c => c.Attribute("nom").Value == "Burns"
                                         && c.Attribute("prenom").Value == "Charles Montgomery")
                             .FirstOrDefault();
if (elementBurns != null)
{     string description = elementBurns.Element("description").Value;
    Console.WriteLine($"La description de monsier Burns est:\n{description.Trim()}");
}

