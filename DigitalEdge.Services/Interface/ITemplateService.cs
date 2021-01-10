using System;
using System.Collections.Generic;
using System.Text;
using DigitalEdge.Repository;

namespace DigitalEdge.Services
{
    public interface ITemplateService
    {
        List<MessageTemplate> GetMessageTemplates();
        void DeleteMessageTemplate(MessageTemplate deleteMessageTemplate);
        void AddMessageTemplate(MessageTemplate addMessageTemplate);
        MessageTemplate GetMessageTemplate(long id);
    }
}
