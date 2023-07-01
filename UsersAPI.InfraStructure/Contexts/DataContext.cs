using Microsoft.EntityFrameworkCore;
using UsersAPi.Domain.Models;

namespace UsersAPI.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<User>  Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "bd_users");
        }

    }
}
