using System.Collections.Generic;
using System.Linq;

namespace DigitalEdge.Repository
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IBaseRepository<MessageTemplate> _messageTemplateRepository;

        public TemplateRepository(IBaseRepository<MessageTemplate> messageTemplateRepository)
        {
            this._messageTemplateRepository = messageTemplateRepository;
        }
        public List<MessageTemplate> GetMessageTemplates()
        {
            List<MessageTemplate> messageTemplate = this._messageTemplateRepository.GetAll().ToList();
            return messageTemplate;
        }

        public MessageTemplate GetMessageTemplate(long id)
        {
            MessageTemplate messageTemplate = this._messageTemplateRepository.Get().Where(x => x.MessageTemplateId == id).FirstOrDefault();
            return messageTemplate;
        }
        public void CreateMessageTemplate(MessageTemplate messageTemplate)
        {
            this._messageTemplateRepository.Insert(messageTemplate);
        }
        public void UpdateMessageTemplate(MessageTemplate messageTemplate)
        {
            this._messageTemplateRepository.Update(messageTemplate);
        }
        public void DeleteMessageTemplate(MessageTemplate deleteMessageTemplate)
        {
            this._messageTemplateRepository.Delete(deleteMessageTemplate);
        }
    }
}
