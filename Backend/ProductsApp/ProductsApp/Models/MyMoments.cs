using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class MyMoments
    {
        public string M_ID { get; set; }
        public string Content { get; set; }
        public int LikeNum { get; set; }
        public int ForwardNum { get; set; }
        public int CollectNum { get; set; }
        public int CommentNum { get; set; }
        public string Time { get; set; }
        public string P_ID { get; set; }
        public string URL { get; set; }

    }
}
