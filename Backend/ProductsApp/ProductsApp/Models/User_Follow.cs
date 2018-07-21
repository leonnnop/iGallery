using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class User_Follow
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public string FollowState { get; set; }
    }
}