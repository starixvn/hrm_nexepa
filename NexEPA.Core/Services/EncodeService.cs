// -----------------------------------------------------------------------
// <copyright file="EncodeService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.08.05">Created</change>
// </history>

namespace NexEPA.Core.Services
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Serialization;
    using NexEPA.Core.Services.Interface;

    /// <summary>
    /// The encode service.
    /// </summary>
    public class EncodeService : IEncodeService
    {
        /// <summary>
        /// Encrypt data use RSA and Base64.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The cipher text.</returns>
        public string Encode(string plainText)
        {
            string publicKeyText = File.ReadAllText(@"RSAKey\publicKey.txt");
            RSAParameters publicKey = ConvertStringToKey(publicKeyText);

            //// Encode use RSA
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(publicKey);

            byte[] bytesPlainText = Encoding.Unicode.GetBytes(plainText);
            byte[] bytesCipherText = csp.Encrypt(bytesPlainText, false);

            //// Encode use Base64
            string cipherText = Convert.ToBase64String(bytesCipherText);

            return cipherText;
        }

        /// <summary>
        /// Decode data.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>The plain text.</returns>
        public string Decode(string cipherText)
        {
            string privateKeyText = File.ReadAllText(@"RSAKey\privateKey.txt");
            RSAParameters privateKey = ConvertStringToKey(privateKeyText);

            //// Decode use Base64
            byte[] bytesCipherText = Convert.FromBase64String(cipherText);

            //// Decode use RSA
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privateKey);

            byte[] bytesPlainText = csp.Decrypt(bytesCipherText, false);
            string plainText = Encoding.Unicode.GetString(bytesPlainText);

            return plainText;
        }

        private RSAParameters ConvertStringToKey(string keyString)
        {
            //// get a stream from the string
            var stringReader = new StringReader(keyString);
            //// a deserializer
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            //// get the object back from the stream
            RSAParameters key = (RSAParameters)xmlSerializer.Deserialize(stringReader);
            return key;
        }
    }
}
