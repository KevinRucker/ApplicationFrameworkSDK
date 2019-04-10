// Author: Kevin Rucker
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

using System;
using System.IO;
using System.Reflection;

namespace ApplicationFrameworkSDK.Common
{
    /// <summary>
    /// Common helper functions
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Function to create and return a binary buffer
        /// </summary>
        public static Func<int, byte[]> CreateBuffer = x => (byte[])Array.CreateInstance(typeof(byte), x);

        /// <summary>
        /// Get embedded resource
        /// </summary>
        /// <param name="resourcePath">Resource path</param>
        /// <returns><see cref="byte[]"/> containing resource</returns>
        public static byte[] GetEmbeddedResource(string resourcePath)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(resourcePath)
                .BinaryReadToEnd();
        }

        /// <summary>
        /// Get embedded resource from specified assembly
        /// </summary>
        /// <param name="assemblyPath">Path of assembly containing embedded resource</param>
        /// <param name="resourcePath">Resource path</param>
        /// <returns><see cref="byte[]"/> containing resource</returns>
        public static byte[] GetEmbeddedResource(string assemblyPath, string resourcePath)
        {
            return Assembly
                .LoadFrom(assemblyPath)
                .GetManifestResourceStream(resourcePath)
                .BinaryReadToEnd();
        }
    }
}
