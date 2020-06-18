﻿// <copyright file="Envelope.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class Envelope
    {
        public string Transport { get; set; }

        public string Sender { get; set; }

        public string SendingIp { get; set; }

        public string Targets { get; set; }
    }
}
