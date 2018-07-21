using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class DisplayedMoment
    {
        public Users user { get; set; }
        public Users forwarded { get; set; }
        public Moment moment { get; set; }
        public List<string> tags { get; set; }
        public int liked { get; set; }
        public int collected { get; set; }
        public List<DisplayedComment> comments { get; set; }
    }
}