using FilmsDataBase.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using FIlmsDataBaseWEBApiApplication;

namespace FilmsDataBase.Infrastructure
{
  public interface IRepository
  {
     List<Film> GetAll();
     Task UpdateBase(Film film, int id);
     Task AddToBase(Film film);
     Task DeleteFromBase(int id);
     Film FindInBase(Film film, int id);
     bool Exist(int id);
     int GetCountOfRows();
     int GetFirstId();
  }
}
