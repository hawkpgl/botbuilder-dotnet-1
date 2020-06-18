// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Microsoft.Bot.Streaming.PayloadTransport
{
    internal class TransportDisconnectedException : Exception
    {
        public TransportDisconnectedException()
        {
        }

        public TransportDisconnectedException(string reason)
        {
            Reason = reason;
        }

        public TransportDisconnectedException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public string Reason { get; set; }
    }
}
