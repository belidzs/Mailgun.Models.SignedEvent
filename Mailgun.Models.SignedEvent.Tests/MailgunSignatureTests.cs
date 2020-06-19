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
            _originalTimestamp = new DateTime(2020, 6, 18, 11, 55, 0).ToUniversalTime();
            var originalTimestampAsUnixEpoch = (_originalTimestamp - DateTime.UnixEpoch).TotalSeconds.ToString();
            _timeSinceOriginalTimestamp = DateTime.UtcNow - _originalTimestamp;
            _apiKey = "ffffffffffffffffffffffffffffffff-ffffffff-ffffffff";

            _validSignature = new MailgunSignature()
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
            Assert.That(_validSignature.IsValid(_apiKey, _timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.True);
        }

        /// <summary>
        /// Checks if a slightly old signature returns invalid.
        /// </summary>
        [Test]
        public void TooOld()
        {
            Assert.That(_validSignature.IsValid(_apiKey, _timeSinceOriginalTimestamp - new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the signature invalidates it.
        /// </summary>
        [Test]
        public void BadSignature()
        {
            _validSignature.Signature += "x";

            Assert.That(_validSignature.IsValid(_apiKey, _timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the token invalidates the signature.
        /// </summary>
        [Test]
        public void BadToken()
        {
            _validSignature.Token += "x";

            Assert.That(_validSignature.IsValid(_apiKey, _timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }

        /// <summary>
        /// Checks if changing the API key invalidates the signature.
        /// </summary>
        [Test]
        public void BadApiKey()
        {
            _apiKey += "x";

            Assert.That(_validSignature.IsValid(_apiKey, _timeSinceOriginalTimestamp + new TimeSpan(0, 1, 0)), Is.False);
        }
    }
}