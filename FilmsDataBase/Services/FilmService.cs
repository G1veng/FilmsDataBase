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
    public void DeleteData(string title) => _concentrationService.DeleteFromBase(title).Wait();
    public void UpdataDataBase(string title, string newTitle, string newDescription, string newIcon, string newTrailer, System.DateTime newYear) =>
      _concentrationService.UpdateBase(new RawFilm() { Title = newTitle, Description = newDescription, Icon = newIcon, Trailer = newTrailer, Year = newYear }, title).Wait();
    public bool Exist(string title) => _concentrationService.Exist(title);
    public bool IsEmpty() => _concentrationService.GetCountOfRows() == 0;
  }
}
