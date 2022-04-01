using FilmsDataBase.Models;
using System.Threading.Tasks;

namespace FilmsDataBase.Infrastructure
{
  public interface IRepository
  {
     Task UpdateBase(RawFilm film, string oldTitle);
     Task AddToBase(RawFilm film);
     Task DeleteFromBase(string title);
     RawFilm FindInBase(RawFilm film, int id);
     bool Exist(string Title);
     int GetCountOfRows();
     int GetFirstId();
  }
}
