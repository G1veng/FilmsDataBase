using FIlmsDataBaseWEBApiApplication.Infrastructure;
using System.Collections.Generic;

namespace FIlmsDataBaseWEBApiApplication.Data
{
  public class EFFilmRepository : IEFFilmREpository
  {
    private EFFilmsDbContext _context;
    public EFFilmRepository(EFFilmsDbContext context) => _context = context;
    public IEnumerable<Film> Get() => _context.FilmTable;
    public Film Get(int id) => _context.FilmTable.Find(id);

    public void Create(Film item) 
    {
      _context.Add(item);
      _context.SaveChanges();
    }
    public void Update(Film item) 
    {
      Film currentItem = Get(item.Id);
      currentItem.Title = item.Title;
      currentItem.Description = item.Description;
      currentItem.Year = item.Year;
      currentItem.Trailer = item.Trailer;
      currentItem.Icon = item.Icon;
      _context.FilmTable.Update(currentItem);
      _context.SaveChanges();
    }
    public Film Delete(int id) 
    {
      Film film = Get(id);
      if (film != null)
      {
        _context.FilmTable.Remove(film);
        _context.SaveChanges();
      }
      return film;
    }
  }
}
