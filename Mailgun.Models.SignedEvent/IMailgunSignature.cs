// <copyright file="MailgunSignature.cs" company="Balazs Keresztury">
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

        bool IsValid(string apiKey);
        bool IsValid(string apiKey, TimeSpan tolerance);
    }
}