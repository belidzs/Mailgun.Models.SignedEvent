// <copyright file="DeliveryStatus.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Mailgun.Models.SignedEvent.EventDetails
{
    public class DeliveryStatus
    {
        public bool Tls { get; set; }

        public string MxHost { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public float SessionSeconds { get; set; }

        public int RetrySeconds { get; set; }

        public bool Utf8 { get; set; }

        public int AttemptNo { get; set; }

        public string Message { get; set; }

        public bool CertificateVerified { get; set; }
    }
}
