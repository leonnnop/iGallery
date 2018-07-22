using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class DisplayedComment
    {
        public string sender_email { get; set; }
        public string sender_username { get; set; }
        public string id { get; set; }
        public  string content { get; set; }
        public string send_time { get; set; }
        public DisplayedComment quote{ get; set; }
    }
}