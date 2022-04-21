using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FilmsDataBase.Infrastructure;
//httpclient
namespace FIlmsDataBaseWEBApiApplication.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmController : ControllerBase
  {
    IRepository EFFilmREpository;
    public FilmController(IRepository eFFilmREpository) => EFFilmREpository = eFFilmREpository;
    [HttpGet(Name = "GetAll")]
    public string GetAll()
    {
      var films = EFFilmREpository.GetAll();
      string JSON = string.Empty;
      foreach (var film in films)
      {
        var json = JsonConvert.SerializeObject(new
        {
          id = film.Id,
          title = film.Title,
          description = film.Description,
          icon = film.Icon,
          traler = film.Trailer,
          year = film.Year
        });
        JSON += json;
        JSON += ", ";
      }
      return JSON;
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
      Film film = EFFilmREpository.FindInBase(new Film(), id);
      if(film == null)
        return "We have not these film";
      return JsonConvert.SerializeObject(new
      {
        id = film.Id,
        title = film.Title,
        description = film.Description,
        icon = film.Icon,
        traler = film.Trailer,
        year = film.Year
      });
    }

    [HttpPost]
    public IActionResult Create(Film film) { 
      EFFilmREpository.AddToBase(film);
      return RedirectToRoute("GetAll");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Film updateFilm)
    {
      if(updateFilm == null || updateFilm.Id != id) return RedirectToRoute("GetAll");
      var film = EFFilmREpository.FindInBase(new Film(), id);
      if(film == null) return RedirectToRoute("GetAll");
      EFFilmREpository.UpdateBase(updateFilm, new int());
      return RedirectToRoute("GetAll");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if(EFFilmREpository.FindInBase(new Film(), id) == null) return RedirectToAction("GetAll");
      var deletedFilm = EFFilmREpository.DeleteFromBase(id);
      return RedirectToRoute("GetAll");
    }
  }
}
