using FilmsDataBase.Models;
using System.Collections.Generic;
using FilmsDataBase.Data;

namespace FilmsDataBase.Services
{
  internal class Functionality
  {
    List<Film> Films { get; set; }
    BaseOfFilms baseOfFilms = new BaseOfFilms();
    public void GetData()
    {
      if(Films == null)
        Films = new List<Film>();
      else
        Films.Clear();
      for(int i = 0; ; i++)
      {
        if (!baseOfFilms.FindInBase(i, out string title, out string description, out string icon, out string trailer, out int year)) break;
        Films.Add(new Film
        {
          Title = title,
          Description = description,
          Icon = icon,
          Trailer = trailer,
          Year = year,
        });
      }
    }
    public void SetData(string title, string description, string icon, string trailer, int year)
    {
      baseOfFilms.AddToBase(title, description, icon, trailer, year);
    }
    public void DeleteData(int id)
    {
      baseOfFilms.DeleteFromBase(id);
    }
  }
}
