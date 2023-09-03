// DbContext para persistir os resultados
using Microsoft.EntityFrameworkCore;

public class AluraDbContext : DbContext
    {
        public DbSet<AluraCourse> Courses { get; set; }

        public AluraDbContext(DbContextOptions<AluraDbContext> options)
            : base(options)
        {
        }
    }