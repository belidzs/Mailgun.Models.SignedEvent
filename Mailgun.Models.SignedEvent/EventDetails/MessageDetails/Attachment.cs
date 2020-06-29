// <copyright file="Attachment.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails.MessageDetails
{
    public class Attachment
    {
        public int Size { get; set; }

        public string ContentType { get; set; }

        public string Filename { get; set; }
    }
}
