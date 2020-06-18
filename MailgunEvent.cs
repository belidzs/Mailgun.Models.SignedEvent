// <copyright file="MailgunEvent.cs" company="Balazs Keresztury">
// Copyright (c) Balazs Keresztury. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using Mailgun.Models.SignedEvent.EventDetails;

namespace Mailgun.Models.SignedEvent
{
    /// <summary>
    /// Mailgun tracks all of the events that occur throughout the system. Below are listed the events that you can retrieve using this API.
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// Mailgun accepted the request to send/forward the email and the message has been placed in queue.
        /// </summary>
        Accepted,

        /// <summary>
        /// Mailgun rejected the request to send/forward the email.
        /// </summary>
        Rejected,

        /// <summary>
        /// Mailgun sent the email and it was accepted by the recipient email server.
        /// </summary>
        Delivered,

        /// <summary>
        /// Mailgun could not deliver the email to the recipient email server.
        /// severity = permanent when a message is not delivered. There are several reasons why Mailgun stops attempting to deliver messages and drops them including: hard bounces, messages that reached their retry limit, previously unsubscribed/bounced/complained addresses, or addresses rejected by an ESP.
        /// severity = temporary when a message is temporary rejected by an ESP.
        /// </summary>
        Failed,

        /// <summary>
        /// The email recipient opened the email and enabled image viewing. Open tracking must be enabled in the Mailgun control panel, and the CNAME record must be pointing to mailgun.org.
        /// </summary>
        Opened,

        /// <summary>
        /// The email recipient clicked on a link in the email. Click tracking must be enabled in the Mailgun control panel, and the CNAME record must be pointing to mailgun.org.
        /// </summary>
        Clicked,

        /// <summary>
        /// The email recipient clicked on the unsubscribe link. Unsubscribe tracking must be enabled in the Mailgun control panel.
        /// </summary>
        Unsubscribed,

        /// <summary>
        /// The email recipient clicked on the spam complaint button within their email client. Feedback loops enable the notification to be received by Mailgun.
        /// </summary>
        Complained,

        /// <summary>
        /// Mailgun has stored an incoming message
        /// </summary>
        Stored,
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
    }

    /// <summary>
    /// Events are represented as loosely structured JSON documents. The exact event structure depends on the event type.
    /// </summary>
    public class MailgunEvent
    {
        /// <summary>
        /// Gets or sets the type of the event. Events of a particular type have an identical structure, though some fields may be optional.
        /// </summary>
        public Event Event { get; set; }

        /// <summary>
        /// Gets or sets the event ID. It is guaranteed to be unique within a day. It can be used to distinguish events that have already been retrieved when requests with overlapping time ranges are made.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the time the event was generated in the system provided as Unix epoch seconds.
        /// </summary>
        public double Timestamp { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Severity { get; set; }

        public string Reason { get; set; }

        public string Method { get; set; }

        public List<Route> Routes { get; set; }

        public Envelope Envelope { get; set; }

        public Flags Flags { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; }

        public Message Message { get; set; }

        public Storage Storage { get; set; }

        public string Recipient { get; set; }

        public string RecipientDomain { get; set; }

        public List<string> Campaigns { get; set; }

        public List<string> Tags { get; set; }

        public Dictionary<string, string> UserVariables { get; set; }

        public Geolocation Geolocation { get; set; }

        public string Ip { get; set; }

        public ClientInfo ClientInfo { get; set; }

        public string Url { get; set; }

        public Reject Reject { get; set; }
    }
}
