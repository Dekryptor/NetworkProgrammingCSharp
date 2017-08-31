using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketAsyncClient
{
    class OutgoingMessageHolder
    {
        internal string[] arrayOfMessages;
        internal Int32 countOfConnectionsRetries = 0;

        public OutgoingMessageHolder(string[] theArrayOfMessages)
        {
            this.arrayOfMessages = theArrayOfMessages;
        }
    }
}
