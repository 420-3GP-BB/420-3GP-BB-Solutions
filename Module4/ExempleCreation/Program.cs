using System.Xml;

XmlDocument doc = new XmlDocument();

char DIR_SEPARATOR = Path.DirectorySeparatorChar;

string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                     $"Fichiers-3GP{DIR_SEPARATOR}exemple.xml";



XmlElement racine = doc.CreateElement("racine");
doc.AppendChild(racine);

XmlElement fils = doc.CreateElement("fils");
fils.SetAttribute("attribut", "valeur");
fils.InnerText = "Un texte dans le fils";

racine.AppendChild(fils);

doc.Save(pathFichier);
