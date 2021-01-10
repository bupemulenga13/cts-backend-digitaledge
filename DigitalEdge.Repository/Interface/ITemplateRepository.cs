using System.Collections.Generic;

namespace DigitalEdge.Repository
{
    public interface ITemplateRepository
    {
        List<MessageTemplate> GetMessageTemplates();
        MessageTemplate GetMessageTemplate(long id);
        void DeleteMessageTemplate(MessageTemplate deleteMessageTemplate);
        void CreateMessageTemplate(MessageTemplate addMessageTemplate);
        void UpdateMessageTemplate(MessageTemplate addMessageTemplate);
    }
}
