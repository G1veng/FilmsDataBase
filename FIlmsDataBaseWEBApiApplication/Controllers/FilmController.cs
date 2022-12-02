using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using FIlmsDataBaseWEBApiApplication.Models;
using FIlmsDataBaseWEBApiApplication.Infrastructure;
namespace FIlmsDataBaseWEBApiApplication.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmController : ControllerBase
  {
    private readonly string _mainPage = "main.html";
    private readonly string _iconPath = "wwwroot\\Files\\Images\\";
    private readonly string _trailerPath = "wwwroot\\Files\\Media\\";
    IEFFilmREpository EFFilmREpository;
    public FilmController(IEFFilmREpository eFFilmREpository) => EFFilmREpository = eFFilmREpository;
    [HttpGet(Name = "GetAll")]
    public List<Film> GetAll() => EFFilmREpository.Get();

    [HttpGet("{id}")]
    public Film Get(int id) => EFFilmREpository.Get(id);

    [HttpPost]
    public void Create([FromForm] FilmFromForm filmFromForm) {
      Film film = new Film()
      {
        Title = filmFromForm.title,
        Description = filmFromForm.description,
        Icon = filmFromForm.icon.FileName,
        Trailer = filmFromForm.trailer.FileName,
        Year = filmFromForm.year,
      };
      filmFromForm.icon.CopyTo(new FileStream(_iconPath + filmFromForm.icon.FileName, FileMode.Create));
      filmFromForm.trailer.CopyTo(new FileStream(_trailerPath + filmFromForm.trailer.FileName, FileMode.Create));
      EFFilmREpository.Create(film);
      Response.Redirect(_mainPage);
    }

    [HttpPut("{id}")]
    public void Update(int id,[FromForm] FilmFromForm filmFromForm)
    {
      var film = EFFilmREpository.Get(id);
      if(film == null) Response.Redirect(_mainPage);
      film.Title = filmFromForm.title;
      film.Description = filmFromForm.description;
      film.Year = filmFromForm.year;
      film.Id = id;
      EFFilmREpository.Update(film);
      Response.Redirect(_mainPage);
    }

    [HttpDelete("{id:int}")]
    public void Delete(int id)
    {
      if (EFFilmREpository.Get(id) == null)
      {
        Response.Redirect(_mainPage);
      }
      EFFilmREpository.Delete(id);
      Response.Redirect(_mainPage);
    }
  }
}
