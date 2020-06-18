// <copyright file="Reject.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class Reject
    {
        public string Reason { get; set; }

        public string Description { get; set; }
    }
}
