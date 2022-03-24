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
  }
}
