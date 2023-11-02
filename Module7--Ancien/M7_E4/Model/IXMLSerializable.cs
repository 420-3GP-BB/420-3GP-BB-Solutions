using System.Xml;

namespace Model
{
    public interface IXMLSerializable
    {
        public XmlElement ToXML(XmlDocument doc);
        public void FromXML(XmlElement elem);
    }
}
