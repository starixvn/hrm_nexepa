// --------------------------------------------------------------------------
// <copyright file="GravatarImage.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.26">Created</change>
// </history>

namespace NexEPA.Core.Utilities
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// GRAVATAR Image.
    /// </summary>
    public sealed class GravatarImage
    {
        private GravatarImage()
        {
        }

        /// <summary>
        /// Method get image from GRAVATAR site.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>Image code.</returns>
        public static string HashEmailForGravatar(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            using (MD5 md5Hasher = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.  
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

                // Create a new Stringbuilder to collect the bytes  
                // and create a string.  
                var stringBuilder = new StringBuilder();

                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string.  
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                stringBuilder.Append(Constants.Constants.DefaultGravatarUrl);

                return stringBuilder.ToString();  // Return the hexadecimal string. 
            }
        }
    }
}
