using System.Xml;

namespace Utilitaires
{
    public interface IConversionXML
    {
        public XmlElement VersXML(XmlDocument doc);
        public void DeXML(XmlElement elem);
    }
}


