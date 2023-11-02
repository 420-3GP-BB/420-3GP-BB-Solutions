using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilitaires
{
    public interface IXMLSerializable
    {
        public XmlElement ToXML(XmlDocument doc);
        public void FromXML(XmlElement elem);
    }
}
