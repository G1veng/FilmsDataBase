using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using FIlmsDataBaseWEBApiApplication.Infrastructure;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FIlmsDataBaseWEBApiApplication.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmController : ControllerBase
  {
    IEFFilmREpository EFFilmREpository;
    public FilmController(IEFFilmREpository eFFilmREpository) => EFFilmREpository = eFFilmREpository;
    [HttpGet(Name = "GetAllFilms")]
    public IEnumerable<Film> Get()
    {
      return EFFilmREpository.Get();
    }

    [HttpGet("{id}", Name = "GetFilm")]
    public IActionResult Get(int id)
    {
      Film film = EFFilmREpository.Get(id);
      if(film != null)
        return new ObjectResult(film);
      return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] Film film)
    {
      if (film == null) return BadRequest();
      EFFilmREpository.Create(film);
      return CreatedAtRoute("GetFilm", new { id = film.Id }, film);
    }

    /*[HttpPost]
    public async Task<IActionResult> AddFile(IFormFileCollection uploads)
    {
      foreach (var uploadedFile in uploads)
      {
        // путь к папке Files
        string path = "/Files/" + uploadedFile.FileName;
        // сохраняем файл в папку Files в каталоге wwwroot
        using (var fileStream = new FileStream(@"D:\4 семестр\РПС\FilmsDataBase\FIlmsDataBaseWEBApiApplication\wwwroot\" + path, FileMode.Create))
        {
          await uploadedFile.CopyToAsync(fileStream);
        }
        FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
        //_context.Files.Add(file);
      }
      //_context.SaveChanges();

      return RedirectToAction("GetAllFilms");
    }*/

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Film updateFilm)
    {
      if(updateFilm == null || updateFilm.Id != id) return BadRequest();
      var film = EFFilmREpository.Get(id);
      if(film == null) return NotFound();
      EFFilmREpository.Update(updateFilm);
      return RedirectToRoute("GetAllFilms");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var deletedFilm = EFFilmREpository.Delete(id);
      if (deletedFilm == null) return BadRequest();
      return new ObjectResult(deletedFilm);
    }
  }
}
