using FIlmsDataBaseWEBApiApplication.Infrastructure;
using System.Collections.Generic;
using FilmsDataBase.Infrastructure;
using FilmsDataBase.Models;
using System.Threading.Tasks;

namespace FIlmsDataBaseWEBApiApplication.Data
{
  public class EFFilmRepository : IRepository
  {
    private EFFilmsDbContext _context;
    public EFFilmRepository(EFFilmsDbContext context) => _context = context;
    public bool Exist(int id) => new bool();
    public int GetFirstId() => new int();
    public int GetCountOfRows() => new int();
    public List<Film> GetAll() 
    { 
      List<Film> list = new List<Film>();
      var films = _context.FilmTable;
      foreach(var film in films) 
        list.Add(film);
      return list;
    }
    public Film FindInBase(Film useLess,int id) => _context.FilmTable.Find(id);
    public void Create(Film item) 
    {
      _context.Add(item);
      _context.SaveChanges();
    }
    public async Task AddToBase(Film item) 
    {
      await _context.AddAsync(item);
      _context.SaveChanges();
    }
    public async Task UpdateBase(Film item, int id)
    {
      Film currentItem = FindInBase(new Film(), item.Id);
      currentItem.Title = item.Title;
      currentItem.Description = item.Description;
      currentItem.Year = item.Year;
      currentItem.Trailer = item.Trailer;
      currentItem.Icon = item.Icon;
      _context.FilmTable.Update(currentItem);
      await _context.SaveChangesAsync();
    }
    public async Task DeleteFromBase(int id) 
    {
      Film film = FindInBase(new Film(), id);
      if (film != null)
      {
        _context.FilmTable.Remove(film);
        await _context.SaveChangesAsync();
      }
    }
  }
}
