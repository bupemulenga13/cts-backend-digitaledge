using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Repository
{
    public interface IMessageRepository
    {
        List<Messages> GetMessages();
        Messages GetMessage(long id);
        void DeleteMessage(Messages deleteMessage);
        void CreateMessage(Messages addMessage);
        void CreateBulkMessage(BulkMessages addMessage);
        void UpdateMessage(Messages addMessage);
    }
}
