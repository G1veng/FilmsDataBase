using FIlmsDataBaseWEBApiApplication.Infrastructure;
using System.Collections.Generic;
using FIlmsDataBaseWEBApiApplication.Data;
using System.Threading.Tasks;
using FIlmsDataBaseWEBApiApplication.Models;

namespace FIlmsDataBaseWEBApiApplication.Services
{
  public class EFFilmRepository : IEFFilmREpository 
  {
    private EFFilmsDbContext _context;
    public EFFilmRepository(EFFilmsDbContext context) => _context = context;
    public List<Film> Get() 
    { 
      List<Film> list = new List<Film>();
      var films = _context.Film;
      foreach (var film in films)
      {
        string temp = film.Trailer;
        film.Trailer = "Files/Media/" + temp;
        temp = film.Icon;
        film.Icon = "Files/Images/" + temp;
        list.Add(film);
      }
      return list;
    }
    public Film Get(int id)
    {
      var film = _context.Film.Find(id);
      string temp = film.Trailer;
      film.Trailer = "Files/Media/" + temp;
      temp = film.Icon;
      film.Icon = "Files/Images/" + temp;
      return film;
    }
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
      _context.Film.Update(currentItem);
      _context.SaveChangesAsync();
    }
    public void Delete(int id) 
    {
      Film film = Get(id);
      if (film != null)
      {
        _context.Film.Remove(film);
        _context.SaveChanges();
      }
    }
  }
}
