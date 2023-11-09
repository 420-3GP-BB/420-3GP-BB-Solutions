using System.Xml;

namespace Utilitaires
{
    public interface IXMLSerializable
    {
        public XmlElement ToXML(XmlDocument doc);
        public void FromXML(XmlElement elem);
    }
}
