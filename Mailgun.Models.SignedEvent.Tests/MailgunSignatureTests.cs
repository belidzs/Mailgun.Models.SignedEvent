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
        private DateTime _originalTimestamp;
        private TimeSpan _timeSinceOriginalTimestamp;
        private MailgunSignature _validSignature;
        private string _apiKey;

        /// <summary>
        /// Sets up test fixture with a know valid signature.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this._originalTimestamp = new DateTime(2020, 6, 18, 11, 55, 0).ToUniversalTime();
            var originalTimestampAsUnixEpoch = (this._originalTimestamp - DateTime.UnixEpoch).TotalSeconds.ToString();
            this._timeSinceOriginalTimestamp = DateTime.UtcNow - this._originalTimestamp;
            this._apiKey = "ffffffffffffffffffffffffffffffff-ffffffff-ffffffff";

            this._validSignature = new MailgunSignature()
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
            Assert.That(this._validSignature.IsValid(this._apiKey, this._timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.True);
        }

        /// <summary>
        /// Checks if a slightly old signature returns invalid.
        /// </summary>
        [Test]
        public void TooOld()
        {
            Assert.That(this._validSignature.IsValid(this._apiKey, this._timeSinceOriginalTimestamp - new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the signature invalidates it.
        /// </summary>
        [Test]
        public void BadSignature()
        {
            this._validSignature.Signature += "x";

            Assert.That(this._validSignature.IsValid(this._apiKey, this._timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the token invalidates the signature.
        /// </summary>
        [Test]
        public void BadToken()
        {
            this._validSignature.Token += "x";

            Assert.That(this._validSignature.IsValid(this._apiKey, this._timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the API key invalidates the signature.
        /// </summary>
        [Test]
        public void BadApiKey()
        {
            this._apiKey += "x";

            Assert.That(this._validSignature.IsValid(this._apiKey, this._timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }
    }
}