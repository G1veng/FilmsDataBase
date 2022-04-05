using FilmsDataBase.Models;
using System.Collections.Generic;
using FilmsDataBase.Data;
using FilmsDataBase.Infrastructure;
using System;

namespace FilmsDataBase.Services
{
  internal class FilmService : IFilmService
  {
    private List<Film> innerFilms;
    private static IRepository _concentrationService = null;
    public FilmService(IRepository concentrationService) =>
      _concentrationService = concentrationService ?? throw new ArgumentNullException(nameof(concentrationService));
    public FilmService() { }
    public List<Film> GetData()
    {
      if (innerFilms == null)
        innerFilms = new List<Film>();
      else
        innerFilms.Clear();
      int counter = 0;
      for (int i = _concentrationService.GetFirstId(); counter < _concentrationService.GetCountOfRows(); i++)
      {
        RawFilm film = new RawFilm();
        if (!(_concentrationService.FindInBase(film, i).Title != null))
        {
          continue;
        }
        innerFilms.Add(new Film
        {
          Id = i,
          Title = film.Title,
          Description = film.Description,
          Icon = film.Icon,
          Trailer = film.Trailer,
          Year = film.Year,
        });
        counter++;
      }
      return innerFilms;
    }
    public void SetData(string title, string description, string icon, string trailer, System.DateTime year) =>
      _concentrationService.AddToBase(new RawFilm() { Title = title, Description = description, Icon = icon, Trailer = trailer, Year = year }).Wait();
    public void DeleteData(int id) => _concentrationService.DeleteFromBase(id).Wait();
    public void UpdataDataBase(int id, string newTitle, string newDescription, string newIcon, string newTrailer, System.DateTime newYear) =>
      _concentrationService.UpdateBase(new RawFilm() { Title = newTitle, Description = newDescription, Icon = newIcon, Trailer = newTrailer, Year = newYear }, id).Wait();
    public bool Exist(int id) => _concentrationService.Exist(id);
    public bool IsEmpty() => _concentrationService.GetCountOfRows() == 0;
  }
}
