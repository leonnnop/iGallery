
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class User_Moment
    {
        public string SenderID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
        public int LikeNum { get; set; }
        public int ForwardNum { get; set; }
        public int CollectNum { get; set; }
        public int CommentNum { get; set; }
        public string Time { get; set; }
        public string MomentID { get; set; }
        public string LikeState { get; set; }
    }
}