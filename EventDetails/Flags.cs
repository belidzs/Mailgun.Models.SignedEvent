// <copyright file="Flags.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class Flags
    {
        public bool IsRouted { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsSystemTest { get; set; }

        public bool IsTestMode { get; set; }

        public bool IsDelayedBounce { get; set; }
    }
}
