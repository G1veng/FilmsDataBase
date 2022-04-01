﻿

namespace FilmsDataBase.Models
{
  public class Film
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string Trailer { get; set; }
    public System.DateTime Year { get; set; }
    public Film(string Title, string Description, string Icon, string Trailer, System.DateTime Year)
    {
      this.Year = Year;
      this.Title = Title;
      this.Description = Description;
      this.Icon = Icon;
      this.Trailer = Trailer;
    }
    public Film() { }
    public object Clone()
    {
      return new Film(Title, Description, Icon, Trailer, Year);
    }
  }
}
