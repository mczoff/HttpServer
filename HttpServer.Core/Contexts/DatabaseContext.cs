using System.Data.Entity;
using HTTPServer.Core.Repositories.Models;

namespace HTTPServer.Core.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("connectionString")
        {
        }

        public DbSet<Country> Countries { get; set; }
    }
}