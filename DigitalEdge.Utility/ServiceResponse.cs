using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalEdge.Utility
{
    public class ServiceResponse : BaseResponse
    {
        public Object Data { get; set; }

        public List<Object> MultipleData { get; set; }

        public object identity { get; set; }
    }
}

