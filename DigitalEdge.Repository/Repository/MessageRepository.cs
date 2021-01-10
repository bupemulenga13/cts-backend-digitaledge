using System.Collections.Generic;
using System.Linq;

namespace DigitalEdge.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IBaseRepository<Messages> _messageRepository;
        private readonly IBaseRepository<BulkMessages> _bulkMessageRepository;
        private readonly DigitalEdgeContext _DigitalEdgeContext;
        public MessageRepository(IBaseRepository<Messages> messagesRepository, IBaseRepository<BulkMessages> bulkMessageRepository,DigitalEdgeContext DigitalEdgeContext)
        {
            this._bulkMessageRepository = bulkMessageRepository;
            this._messageRepository = messagesRepository;
            this._DigitalEdgeContext = DigitalEdgeContext;
        }

        public void CreateMessage(Messages addMessage)
        {
            _messageRepository.Insert(addMessage);
        }
        public void CreateBulkMessage(BulkMessages addMessage)
        {
            _bulkMessageRepository.Insert(addMessage);
        }

        public void DeleteMessage(Messages deleteMessage)
        {
            _messageRepository.Delete(deleteMessage);
        }

        public List<Messages> GetMessages()
        {
            List<Messages> messages= this._messageRepository.GetAll().ToList();
            return messages;
        }

        public Messages GetMessage(long id)
        {
            Messages message = this._messageRepository.Get().Where(x => x.MessagesId == id).FirstOrDefault();
            return message;
        }

        public void UpdateMessage(Messages updateMessage)
        {
            _messageRepository.Update(updateMessage);
        }
    }
}
