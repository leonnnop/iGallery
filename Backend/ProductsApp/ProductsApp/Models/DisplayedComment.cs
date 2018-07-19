using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class DisplayedComment
    {
        public Users sender { get; set; }
        public  Coment comment { get; set; }
        public DisplayedComment quote{ get; set; }
    }
}