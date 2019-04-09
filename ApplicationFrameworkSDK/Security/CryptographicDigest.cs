﻿// Author: Kevin Rucker
// License: BSD 3-Clause
// Copyright (c) 2019, Kevin Rucker
// All rights reserved.

// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// 3. Neither the name of the copyright holder nor the names of its contributors
//    may be used to endorse or promote products derived from this software without
//    specific prior written permission.
//
// Disclaimer:
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Linq;

namespace ApplicationFrameworkSDK.Security
{
    /// <summary>
    /// Instances of this class encapsulate generation of a cryptographic digest value.
    /// </summary>
    public class CryptographicDigest
    {
        private CryptographicDigest()
        {

        }

        /// <summary>
        /// Get cryptographic digest of specific length derived from embedded resource
        /// </summary>
        /// <param name="resourcePath">Path of embedded resource</param>
        /// <param name="digestLength">Desired length of returned encryption digest</param>
        /// <returns>Cryptographic digest value</returns>
        public byte[] GetDigest(string resourcePath, int digestLength)
        {
            return GetDigest(GetEmbeddedResource(resourcePath), digestLength);
        }

        /// <summary>
        /// Get cryptographic digest of specific length
        /// </summary>
        /// <param name="value"><code>System.byte[]</code> containing passphrase</param>
        /// <param name="digestLength">Desired length of returned encryption digest</param>
        /// <returns>Cryptographic digest value</returns>
        public byte[] GetDigest(byte[] value, int digestLength)
        {
            var iterations = 0;
            if (value.Length == 0)
            {
                iterations = byte.MaxValue;
            }
            else
            {
                iterations = value.AsQueryable().First(x => x != 0);
            }

            var deriveBytes = new Rfc2898DeriveBytes(
                value,
                SHA512.Create().ComputeHash(value),
                iterations * 100);

            return deriveBytes.GetBytes(digestLength);
        }

        /// <summary>
        /// Factory method creates an instance of a <see cref="CryptographicDigest"/>
        /// </summary>
        /// <returns>Instance of <see cref="CryptographicDigest"/></returns>
        public static CryptographicDigest Create()
        {
            return new CryptographicDigest();
        }

        private byte[] GetEmbeddedResource(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resource = assembly.GetManifestResourceStream(path);
            var buffer = new byte[0x10000];
            var eos = false;
            using (var ms = new MemoryStream())
            {
                while(!eos)
                {
                    int bytesRead = resource.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0) { ms.Write(buffer, 0, bytesRead); } else { eos = true; }
                }

                return ms.ToArray();
            }
        }
    }
}
