using FilmsDataBase.Models;
using System.Collections.Generic;
using FilmsDataBase.Data;

namespace FilmsDataBase.Services
{
  internal class Functionality
  {
    private BaseOfFilms baseOfFilms = new BaseOfFilms();
    public List<Film> innerFilms;
    public void GetData()
    {
      if(innerFilms == null)
        innerFilms = new List<Film>();
      else
        innerFilms.Clear();
      int counter = 0;
      for (int i = baseOfFilms.GetFirstId(); counter < baseOfFilms.GetCountOfRows(); i++)
      {
        if (!baseOfFilms.FindInBase(out string title, out string description, out string icon, out string trailer, out System.DateTime year, i))
        {
          continue;
        }
        innerFilms.Add(new Film
        {
          Title = title,
          Description = description,
          Icon = icon,
          Trailer = trailer,
          Year = year,
        });
        counter++;
      }
    }
    public void SetData(string title, string description, string icon, string trailer, System.DateTime year) =>
      baseOfFilms.AddToBase(title, description, icon, trailer, year).Wait();
    public void DeleteData(string title) => baseOfFilms.DeleteFromBase(title).Wait();
    public void UpdataDataBase(string title, string newTitle, string newDescription, string newIcon, string newTrailer, System.DateTime newYear) =>
      baseOfFilms.UpdateBase(title, newTitle, newDescription, newIcon, newTrailer, newYear).Wait();
    public bool Exist(string title) => baseOfFilms.Exist(title);
    public bool IsEmpty() => baseOfFilms.GetCountOfRows() == 0;
  }
}
