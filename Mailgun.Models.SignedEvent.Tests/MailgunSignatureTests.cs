// <copyright file="MailgunSignatureTests.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using NUnit.Framework;

namespace Mailgun.Models.SignedEvent.Tests
{
    /// <summary>
    /// Tests MailgunSignature.
    /// </summary>
    public class MailgunSignatureTests
    {
        private DateTime originalTimestamp;
        private TimeSpan timeSinceOriginalTimestamp;
        private MailgunSignature validSignature;
        private string apiKey;

        /// <summary>
        /// Sets up test fixture with a know valid signature.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.originalTimestamp = new DateTime(2020, 6, 18, 11, 55, 0).ToUniversalTime();
            var originalTimestampAsUnixEpoch = (this.originalTimestamp - DateTime.UnixEpoch).TotalSeconds.ToString();
            this.timeSinceOriginalTimestamp = DateTime.UtcNow - this.originalTimestamp;
            this.apiKey = "ffffffffffffffffffffffffffffffff-ffffffff-ffffffff";

            this.validSignature = new MailgunSignature()
            {
                Signature = "de4b938580bb4d84f710cbb8bfa7d224bb2262c8f644f558c2901c1ae645bb03",
                Token = "ffffffffffffffffffffffffffffffffffffffffffffffffff",
                Timestamp = originalTimestampAsUnixEpoch,
            };
        }

        /// <summary>
        /// Checks if a valid signature returns as valid.
        /// </summary>
        [Test]
        public void Valid()
        {
            Assert.That(this.validSignature.IsValid(this.apiKey, this.timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.True);
        }

        /// <summary>
        /// Checks if a slightly old signature returns invalid.
        /// </summary>
        [Test]
        public void TooOld()
        {
            Assert.That(this.validSignature.IsValid(this.apiKey, this.timeSinceOriginalTimestamp - new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the signature invalidates it.
        /// </summary>
        [Test]
        public void BadSignature()
        {
            this.validSignature.Signature += "x";

            Assert.That(this.validSignature.IsValid(this.apiKey, this.timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the token invalidates the signature.
        /// </summary>
        [Test]
        public void BadToken()
        {
            this.validSignature.Token += "x";

            Assert.That(this.validSignature.IsValid(this.apiKey, this.timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the API key invalidates the signature.
        /// </summary>
        [Test]
        public void BadApiKey()
        {
            this.apiKey += "x";

            Assert.That(this.validSignature.IsValid(this.apiKey, this.timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }
    }
}