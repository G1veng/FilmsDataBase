using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsDataBase.Models
{
  public class RawFilm
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string Trailer { get; set; }
    public System.DateTime Year { get; set; }
  }
}
