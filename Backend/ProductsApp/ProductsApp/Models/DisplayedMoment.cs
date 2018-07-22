using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class DisplayedMoment
    {
        public string user_email { get; set; }
        public string  user_username { get; set; }
        public string user_bio { get; set; }
        public string forwarded_email { get; set; }
        public string forwarded_username { get; set; }
        public Moment moment { get; set; }
        public List<string> tags { get; set; }
        public int liked { get; set; }
        public int collected { get; set; }
        public List<DisplayedComment> comments { get; set; }
        public bool more_comments { get; set; }
    }
}