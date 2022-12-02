using System;

namespace FIlmsDataBaseWEBApiApplication.Models
{
  public class Film
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public string Trailer { get; set; }
    public string Icon { get; set; }
    public DateTime Year { get; set; }
  }
}
