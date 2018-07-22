using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Message
    {
        public string Sender_ID { get; set; }
        public string Receiver_ID { get; set; }
        public string Send_Time { get; set; }
        public string Content { get; set; }
    }
}