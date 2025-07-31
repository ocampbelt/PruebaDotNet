using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Persona> Personas { get; set; } = null!;

    }
}