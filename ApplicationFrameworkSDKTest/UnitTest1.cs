using System.Security.Cryptography;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationFrameworkSDK.Common;
using ApplicationFrameworkSDK.Security;

namespace ApplicationFrameworkSDKTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var crypto = SymmetricEncryptionProvider<Aes>.Create();
            var enumTestValue = DataProviders.SqlServer;
            var enumTestDesc = enumTestValue.GetDescription();
            var test = crypto.ExpectedKeySize;
            var test2 = CryptographicDigest.Create().GetDigestFromEmbedded(crypto.ExpectedKeySize);

            var test3 = CryptographicDigest.Create().GetDigestFromEmbedded(
                Assembly.GetExecutingAssembly().Location, 
                "ApplicationFrameworkSDKTest.crypto2.jpg",
                crypto.ExpectedKeySize);
        }
    }
}
