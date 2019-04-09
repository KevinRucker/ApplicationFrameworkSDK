using System.Xml;
using System.Xml.Linq;

namespace ApplicationFrameworkSDK.Security
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Convert <see cref="XmlNode"/> to <see cref="XElement"/>.
        /// </summary>
        /// <param name="value"><see cref="XmlNode"/> to convert</param>
        /// <returns><see cref="XElement"/></returns>
        public static XElement ToXElement(this XmlNode value)
        {
            var doc = new XDocument();
            using(var writer = doc.CreateWriter()) { value.WriteTo(writer); }
            return doc.Root;
        }

        /// <summary>
        /// Convert <see cref="XElement"/> to <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="value"><see cref="XElement"/> to convert</param>
        /// <returns><see cref="XmlNode"/></returns>
        public static XmlNode ToXmlNode(this XElement value)
        {
            using (var reader = value.CreateReader())
            {
                var doc = new XmlDocument();
                doc.Load(reader);
                return doc;
            }
        }
    }
}
