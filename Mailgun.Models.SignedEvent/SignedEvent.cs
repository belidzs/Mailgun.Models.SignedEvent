// <copyright file="SignedEvent.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace Mailgun.Models.SignedEvent
{
    public class SignedEvent
    {
        [Required]
        public IMailgunSignature Signature { get; set; }

        [Required]
        public MailgunEvent EventData { get; set; }
    }
}
