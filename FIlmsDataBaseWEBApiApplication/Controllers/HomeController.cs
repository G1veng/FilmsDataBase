using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing;
using System.Runtime.Versioning;
using FilmsDataBase.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIlmsDataBaseWEBApiApplication.Controllers
{
  [SupportedOSPlatform("windows")]
  [Route("[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    IRepository EFFilmREpository;
    public HomeController(IRepository eFFilmREpository) => EFFilmREpository = eFFilmREpository;
    [HttpGet]
    public string Get()
    {
      return "string";
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    [HttpPost]
    public RedirectResult Post([FromForm] FilmFromForm film)
    {
      string ImagePath = string.Empty;
      string MediaPath = string.Empty;
      if (film.icon != null)
      {
        string filePath = film.icon.FileName;
        ImagePath = "wwwroot\\Files\\Images\\" + filePath;
        film.icon.CopyTo(new FileStream(ImagePath, FileMode.Create));
      }
      if (film.trailer != null)
      {
        string filePath = film.trailer.FileName;
        MediaPath = "wwwroot\\Files\\Media\\" + filePath;
        film.trailer.CopyTo(new FileStream(MediaPath, FileMode.Create));
      }
      EFFilmREpository.AddToBase(new Film()
      {
        Id = new int(),
        Description = film.description,
        Icon = film.icon.FileName,
        Trailer = film.trailer.FileName,
        Title = film.title,
        Year = film.year
      });
      return Redirect("/index.html");
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
