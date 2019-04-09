using System;
using System.Security.Cryptography;
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
        }
    }
}
