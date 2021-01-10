using System;
using System.Collections.Generic;
using System.Linq;
using DigitalEdge.Repository;
using DigitalEdge.Service;

namespace DigitalEdge.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messagesRepository;
        private readonly AppSettings _appSettings;

        public MessageService(IMessageRepository messagesRepository, Microsoft.Extensions.Options.IOptions<AppSettings> appSettings)
        {
            this._messagesRepository = messagesRepository;
            _appSettings = appSettings.Value;

        }
        public void AddMessage(Messages addMessage)
        {
            if (addMessage.MessagesId != 0)
            {
                addMessage.DateEdited = DateTime.UtcNow;
                this._messagesRepository.UpdateMessage(addMessage);
            }
            else
            {
                addMessage.DateCreated = DateTime.UtcNow;
                this._messagesRepository.CreateMessage(addMessage);
            }
        }

        public void DeleteMessage(Messages deleteMessage)
        {
            _messagesRepository.DeleteMessage(deleteMessage);
        }

        public List<Messages> GetMessage()
        {
            List<Messages> messages = _messagesRepository.GetMessages().ToList();
            if (messages == null)
                return null;
            return (messages);
        }

        public Messages GetMessage(long id)
        {
            Messages messages = _messagesRepository.GetMessage(id);
            if (messages == null)
                return null;
            return (messages);
        }
    }
}
