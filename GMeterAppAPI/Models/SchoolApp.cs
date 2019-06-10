using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MobWebApp.Models
{
    public class SchoolApp : DbContext
    {
        public SchoolApp()
                : base("name=SchoolApp")
        {

        }
        public DbSet<People> People { get; set; }
    }
}