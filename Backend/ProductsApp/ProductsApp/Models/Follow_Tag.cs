using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Follow_Tag
    {
        public string UserId { get; set; }
        public string Tag { get; set; }
        public string FollowState { get; set; }
        public string Pic { get; set; }
    }
}