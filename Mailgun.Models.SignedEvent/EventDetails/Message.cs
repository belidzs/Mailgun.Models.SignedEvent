// <copyright file="Message.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using Mailgun.Models.SignedEvent.EventDetails.MessageDetails;

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class Message
    {
        public Headers Headers { get; set; }

        public List<string> Attachments { get; set; }

        public List<string> Recipients { get; set; }

        public int Size { get; set; }
    }
}
