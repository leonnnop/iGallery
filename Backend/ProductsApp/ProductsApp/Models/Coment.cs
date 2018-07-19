using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Coment
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public string QuoteID { get; set; }
    }

    /*测试
     
    "ID":"",
    "Content":"",
    "SendTime":"";
    "QuoteID":""

    */
}