using Microsoft.AspNetCore.Http;
using System;

namespace FIlmsDataBaseWEBApiApplication
{
  public class FilmToSaveData
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Trailer { get; set; }
    public string Icon { get; set; }
    public DateTime Year { get; set; }
  }
}