using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobWebApp.Models
{
    public class People
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public int Place_ID { get; set; }
    }
}