using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("ConfectioneryEntities")
        { }

        public DbSet<worker> Workers { get; set; }
        public DbSet<post> Roles { get; set; }
    }
}