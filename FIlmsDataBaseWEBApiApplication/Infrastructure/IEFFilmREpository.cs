using System.Collections.Generic;

namespace FIlmsDataBaseWEBApiApplication.Infrastructure
{
  public interface IEFFilmREpository
  {
    IEnumerable<Film> Get();
    Film Get(int id);
    void Create(Film item);
    void Update(Film item);
    Film Delete(int id);
  }
}
