// <copyright file="Headers.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails.MessageDetails
{
    public class Headers
    {
        public string To { get; set; }

        public string MessageId { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }
    }
}
