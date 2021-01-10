using System.Collections.Generic;

namespace DigitalEdge.Domain
{
    public class SMSApi
    {
        public Auth auth { get; set; }
        public IList<Messages> messages { get; set; }

    }
    public class Auth
    {
        public string username { get; set; }
        public string password { get; set; }
        public string sender_id { get; set; }
        public string short_code { get; set; }

    }
    public class Messages
    {
        public string phone { get; set; }
        public string message { get; set; }

    }
 
}
