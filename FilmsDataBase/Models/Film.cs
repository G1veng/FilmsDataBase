using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsDataBase.Models
{
  internal class Film
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string Trailer { get; set; }
    public int Year { get; set; }
    public Film(string Title, string Description, string Icon, string Trailer, int Year)
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
