using Microsoft.EntityFrameworkCore;
using FIlmsDataBaseWEBApiApplication.Models;

namespace FIlmsDataBaseWEBApiApplication.Data
{
  public class EFFilmsDbContext : DbContext
  {
    public EFFilmsDbContext(DbContextOptions<EFFilmsDbContext> options) : base(options)
    { }
    public DbSet<Film> Film { get; set; }
  }
}
