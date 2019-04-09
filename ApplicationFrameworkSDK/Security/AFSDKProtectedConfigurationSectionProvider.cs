using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ApplicationFrameworkSDK.Security
{
    /// <summary>
    /// Application Framework SDK (AFSDK) Protected Configuration Section Provider
    /// </summary>
    public class AFSDKProtectedConfigurationSectionProvider : ProtectedConfigurationProvider
    {
        private SymmetricEncryptionProvider<Aes> _crypto = SymmetricEncryptionProvider<Aes>.Create();
        private byte[] _key = null;

        /// <summary>
        /// Initialize provider
        /// </summary>
        /// <param name="name">Provider name</param>
        /// <param name="config">Value collection</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            _key = CryptographicDigest.Create().GetDigest("", _crypto.ExpectedKeySize);
            base.Initialize(name, config);
        }

        /// <summary>
        /// Decrypt configuration section
        /// </summary>
        /// <param name="encryptedNode">Encrypted value</param>
        /// <returns><see cref="XmlNode"/> containing decrypted values</returns>
        public override XmlNode Decrypt(XmlNode encryptedNode)
        {
            var element = encryptedNode.ToXElement();
            if(element != null)
            {
                var decryptedBytes = _crypto.DecryptBytes(Convert.FromBase64String(element.Value), _key);
                return XElement.Parse(new UTF8Encoding().GetString(decryptedBytes)).ToXmlNode();
            }

            return default;
        }

        /// <summary>
        /// Encrypt configuration section
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> containing config section to encrypt</param>
        /// <returns>Encrypted <see cref="XmlNode"/></returns>
        public override XmlNode Encrypt(XmlNode node)
        {
            var element = node.ToXElement();
            if(element != null)
            {
                var encryptedBytes = _crypto.EncryptBytes(new UTF8Encoding().GetBytes(element.ToString()), _key);
                return new XElement("EncryptedData", Convert.ToBase64String(encryptedBytes)).ToXmlNode();
            }

            return default;
        }
    }
}
