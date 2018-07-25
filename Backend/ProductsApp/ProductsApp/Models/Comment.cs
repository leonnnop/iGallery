using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Comment : IComparable<Comment>
    {
        public string Mid { get; set; }
        public string Sender_id { get; set; }
        public string Sender_username { get; set; }
        public string Cid { get; set; }
        public string Content { get; set; }
        public string Quote_username { get; set; }
        public string Send_time { get; set; }
        public string Quote_id { get; set; }
        public string Type { get; set; }
        public string Quote_content { get; set; }

        
        public int CompareTo(Comment other)
        {
            return other.Send_time.CompareTo(this.Send_time);
        }
    }
}