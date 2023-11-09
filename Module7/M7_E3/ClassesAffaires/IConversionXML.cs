using System.Xml;

namespace Model
{
    public interface IConversionXML
    {
        public XmlElement VersXML(XmlDocument doc);
        public void DeXML(XmlElement elem);
    }
}


