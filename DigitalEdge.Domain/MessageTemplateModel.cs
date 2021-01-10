using System;

namespace DigitalEdge.Domain
{
   public class MessageTemplateModel
    {
        public MessageTemplateModel()
        {
        }

        public MessageTemplateModel(long messageTemplateId, string message)
        {
            this.MessageTemplateId = messageTemplateId;
            this.Message = message;
        }

        public long MessageTemplateId { get; set; }
        public string Message { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public Boolean Timed { get; set; }
        public Boolean Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public long EditedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
