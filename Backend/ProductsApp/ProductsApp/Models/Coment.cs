using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ProductsApp.Models
{
    public class Coment
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public string SendTime { get; set; }
        public string QuoteID { get; set; }
        public string Type { get; set; }//0-此评论不是一条回复；1-此评论是回复；2-此评论的引用评论已删除

    }

    /*测试
     
    "ID":"",
    "Content":"",
    "SendTime":"";
    "QuoteID":""

    */
}