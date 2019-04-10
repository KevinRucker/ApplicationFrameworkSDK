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
            var enumTestValue = DataProviders.SqlServer;
            var enumTestDesc = enumTestValue.GetDescription();
            var test = SymmetricEncryptionProvider<Aes>.Create().ExpectedKeySize;
            var test2 = CryptographicDigest.Create().GetDigestFromEmbedded(test);

            var test3 = CryptographicDigest.Create().GetDigestFromEmbedded(
                Assembly.GetExecutingAssembly().Location, 
                "ApplicationFrameworkSDKTest.crypto2.jpg",
                SymmetricEncryptionProvider<Aes>.Create().ExpectedKeySize);
        }
    }
}
