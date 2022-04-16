using Microsoft.EntityFrameworkCore;

namespace FIlmsDataBaseWEBApiApplication
{
  public class EFFilmsDbContext : DbContext
  {
    public EFFilmsDbContext(DbContextOptions<EFFilmsDbContext> options) : base(options)
    { }
    public DbSet<Film> FilmTable { get; set; }
  }
}
