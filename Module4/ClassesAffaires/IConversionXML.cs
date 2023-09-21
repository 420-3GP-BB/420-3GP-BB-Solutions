using System.Xml;

namespace Utilitaires
{
    /// <summary>
    /// Classe qui permet de convertir un objet en XML et vice-versa
    /// </summary>
    public interface IConversionXML
    {
        // Retourne un élément XML qui représente l'objet
        public XmlElement VersXML(XmlDocument doc);        
        // Initialise l'objet à partir d'un élément XML
        public void DeXML(XmlElement elem);
    }
}


