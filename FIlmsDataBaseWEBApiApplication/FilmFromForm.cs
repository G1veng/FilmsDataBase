using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing;
using System.Runtime.Versioning;

namespace FIlmsDataBaseWEBApiApplication
{
  public class FilmFromForm
  {
    public string title { get; set; }
    public string description { get; set; }
    public IFormFile icon { get; set; }
    public IFormFile trailer { get; set; }
    public System.DateTime year { get; set; }
  }
}
