using System;
using System.Collections.Generic;
using System.Linq;
using DigitalEdge.Repository;

namespace DigitalEdge.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly AppSettings _appSettings;

        public TemplateService(ITemplateRepository templateRepository, Microsoft.Extensions.Options.IOptions<AppSettings> appSettings)
        {
            this._templateRepository = templateRepository;
            _appSettings = appSettings.Value;
        }
        public List<MessageTemplate> GetMessageTemplates()
        {
            List<MessageTemplate> messageTemplate = _templateRepository.GetMessageTemplates().ToList();


            if (messageTemplate == null)
                return null;
            return (messageTemplate);
        }
        public MessageTemplate GetMessageTemplate(long id)
        {
            MessageTemplate messageTemplate = _templateRepository.GetMessageTemplate(id);
            if (messageTemplate == null)
                return null;
            return (messageTemplate);
        }

        public void AddMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate.MessageTemplateId != 0)
            {
                messageTemplate.DateEdited = DateTime.UtcNow;
                this._templateRepository.UpdateMessageTemplate(messageTemplate);
            }
            else 
            {
                messageTemplate.DateCreated = DateTime.UtcNow;
                this._templateRepository.CreateMessageTemplate(messageTemplate);
            }
            
        }
        public void DeleteMessageTemplate(MessageTemplate messageTemplate)
        {
            this._templateRepository.DeleteMessageTemplate(messageTemplate);
        }
    }
}
