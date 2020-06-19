// <copyright file="MailgunSignature.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Mailgun.Models.SignedEvent
{
    public class MailgunSignature : IMailgunSignature
    {
        [Required]
        public string Timestamp { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Signature { get; set; }

        /// <inheritdoc />
        public bool IsValid(string apiKey, TimeSpan tolerance)
        {
            // parse timestamp as a DateTime object
            var timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + TimeSpan.FromSeconds(Convert.ToDouble(Timestamp));
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
            var calculated_signature = hasher.ComputeHash(Encoding.ASCII.GetBytes(Timestamp + Token));

            // convert calculated signature to hexdigest format
            string hash_hex = string.Empty;
            foreach (var b in calculated_signature)
            {
                hash_hex += string.Format("{0:x2}", b);
            }

            // compare
            return Signature.Equals(hash_hex);
        }

        /// <inheritdoc />
        public bool IsValid(string apiKey)
        {
            return IsValid(apiKey, new TimeSpan(0, 10, 0));
        }
    }
}
