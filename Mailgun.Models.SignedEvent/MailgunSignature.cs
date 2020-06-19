// <copyright file="MailgunSignature.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Security.Cryptography;
using System.Text;

namespace Mailgun.Models.SignedEvent
{
    public class MailgunSignature : IMailgunSignature
    {
        public string Timestamp { get; set; }

        public string Token { get; set; }

        public string Signature { get; set; }

        /// <summary>
        /// Checks if the signature is valid based on the provided API key.
        /// https://documentation.mailgun.com/en/latest/user_manual.html#webhooks.
        /// </summary>
        /// <param name="apiKey">mailgun API key.</param>
        /// <param name="tolerance">Maximum acceptable time difference between the creation of the signature and now.</param>
        /// <returns>True if the signature is valid.</returns>
        public bool IsValid(string apiKey, TimeSpan tolerance)
        {
            // parse timestamp as a DateTime object
            var timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + TimeSpan.FromSeconds(Convert.ToDouble(this.Timestamp));
            if (DateTime.UtcNow - timestamp > tolerance)
            {
                // if the signature was made too long ago, return false
                return false;
            }

            // concatenate timestamp and token then sign it with the API key
            var hasher = new HMACSHA256()
            {
                Key = Encoding.ASCII.GetBytes(apiKey),
            };
            var calculated_signature = hasher.ComputeHash(Encoding.ASCII.GetBytes(this.Timestamp + this.Token));

            // convert calculated signature to hexdigest format
            string hash_hex = string.Empty;
            foreach (var b in calculated_signature)
            {
                hash_hex += string.Format("{0:x2}", b);
            }

            // compare
            return this.Signature.Equals(hash_hex);
        }

        /// <summary>
        /// Checks if the signature is valid based on the provided API key with a maximum allowed time skew of 10 minutes.
        /// https://documentation.mailgun.com/en/latest/user_manual.html#webhooks.
        /// </summary>
        /// <param name="apiKey">malgun API key.</param>
        /// <returns>True if the signature is valid.</returns>
        public bool IsValid(string apiKey)
        {
            return this.IsValid(apiKey, new TimeSpan(0, 10, 0));
        }
    }
}
