using System.Collections.Generic;
using DigitalEdge.Repository;

namespace DigitalEdge.Service
{
    public interface IMessageService
    {
        List<Messages> GetMessage();
        void DeleteMessage(Messages deleteMessage);
        void AddMessage(Messages addMessage);
        Messages GetMessage(long id);

    }
}
