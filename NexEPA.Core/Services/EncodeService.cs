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
    using Constants;
    using IBCCrypt;
    using NexEPA.Core.Services.Interface;

    /// <summary>
    /// The encode service.
    /// </summary>
    public class EncodeService : IEncodeService
    {
        /// <summary>
        /// Encode data.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The cipher text.</returns>
        public string Encode(string plainText)
        {
            string cipherText = string.Empty;

            if (!string.IsNullOrWhiteSpace(plainText))
            {
                cipherText = IBCCrypt.Encrypt(plainText, Constants.KeyCryptography);
            }

            return cipherText;
        }

        /// <summary>
        /// Decode data.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>The plain text.</returns>
        public string Decode(string cipherText)
        {
            string plainText = string.Empty;

            if (!string.IsNullOrWhiteSpace(cipherText))
            {
                 plainText = IBCCrypt.Decrypt(cipherText, Constants.KeyCryptography);
            }

            return plainText;
        }
    }
}
