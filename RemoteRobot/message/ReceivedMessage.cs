using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteRobot.message
{
    internal class ReceivedMessage
    {
        private DataType dataType;
        private MessageData messageData;

        public MessageData GetMessageData()
        { return messageData; }

        public void SetMessageData(MessageData messageData)
        { this.messageData = messageData; }

        public DataType GetDataType()
        { return dataType; }

        public void SetDataType(DataType dataType)
        { this.dataType = dataType; }

        public ReceivedMessage()
        { messageData = new MessageData(); }
    }
}