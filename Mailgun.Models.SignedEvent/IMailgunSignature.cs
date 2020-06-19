// <copyright file="IMailgunSignature.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Mailgun.Models.SignedEvent
{
    public interface IMailgunSignature
    {
        string Signature { get; set; }

        string Timestamp { get; set; }

        string Token { get; set; }

        /// <summary>
        /// Checks if the signature is valid based on the provided API key with a maximum allowed time skew of 10 minutes.
        /// https://documentation.mailgun.com/en/latest/user_manual.html#webhooks.
        /// </summary>
        /// <param name="apiKey">malgun API key.</param>
        /// <returns>True if the signature is valid.</returns>
        bool IsValid(string apiKey);

        /// <summary>
        /// Checks if the signature is valid based on the provided API key.
        /// https://documentation.mailgun.com/en/latest/user_manual.html#webhooks.
        /// </summary>
        /// <param name="apiKey">mailgun API key.</param>
        /// <param name="tolerance">Maximum acceptable time difference between the creation of the signature and now.</param>
        /// <returns>True if the signature is valid.</returns>
        bool IsValid(string apiKey, TimeSpan tolerance);
    }
}