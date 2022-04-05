using FilmsDataBase.Models;
using System.Threading.Tasks;

namespace FilmsDataBase.Infrastructure
{
  public interface IRepository
  {
     Task UpdateBase(RawFilm film, int id);
     Task AddToBase(RawFilm film);
     Task DeleteFromBase(int id);
     RawFilm FindInBase(RawFilm film, int id);
     bool Exist(int id);
     int GetCountOfRows();
     int GetFirstId();
  }
}
