// Models/UserContext.cs
using System.Data.Entity;
using UserManagementCrudApp.Models;

namespace UserManagementCrudApp.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
    }
}

//Entity Framework 6 (EF6).
//UserContext inherits from DbContext, the base class for working with EF.
//The constructor : base("DefaultConnection") tells EF to use the connection string named DefaultConnection, typically defined in Web.config.
//DbSet<User> Users maps to your Users table in the database.