using System.Xml;

char DIR_SEPARATOR = Path.DirectorySeparatorChar;

string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                     $"Fichiers-3GP{DIR_SEPARATOR}nouveau.xml";

XmlDocument document = new XmlDocument();

XmlElement racine = document.CreateElement("racine");
document.AppendChild(racine);

XmlElement fils = document.CreateElement("fils");
fils.SetAttribute("attribut", "valeur");
fils.InnerText = "Un texte";

racine.AppendChild(fils);

document.Save(pathFichier);





