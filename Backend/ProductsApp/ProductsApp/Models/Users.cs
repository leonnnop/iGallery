using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApp.Models
{
    public class Users
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
    }
    /*测试
    
    "ID":"",
	"Email":"",
	"Password":"",
    "Username":"",
    "Bio":"",
    "Photo":""

    */
}