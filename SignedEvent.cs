// <copyright file="SignedEvent.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent
{
    public class SignedEvent
    {
        public MailgunSignature Signature { get; set; }

        public MailgunEvent EventData { get; set; }
    }
}
