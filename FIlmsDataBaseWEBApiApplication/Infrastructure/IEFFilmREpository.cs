using System.Collections.Generic;
using FIlmsDataBaseWEBApiApplication.Models;

namespace FIlmsDataBaseWEBApiApplication.Infrastructure
{
  public interface IEFFilmREpository
  {
    List<Film> Get();
    Film Get(int id);
    void Create(Film item);
    void Update(Film item);
    void Delete(int id);
  }
}
