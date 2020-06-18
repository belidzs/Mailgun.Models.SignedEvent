// <copyright file="Route.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class Route
    {
        public string Expression { get; set; }

        public string Id { get; set; }

        public Dictionary<string, string> Match { get; set; }
    }
}
