using FilmsDataBase.Models;
using System.Collections.Generic;
using FilmsDataBase.Data;
using FilmsDataBase.Infrastructure;
using System;
using System.Windows.Forms;
using System.IO;

namespace FilmsDataBase.Services
{
  internal class FilmService : IFilmService
  {
    private string pathToSaveIcons = "D:\\4 семестр\\РПС\\Base\\Films\\Icons\\";
    private string pathToSaveTrailers = "D:\\4 семестр\\РПС\\Base\\Films\\Trailers\\";
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
        if (!(_concentrationService.FindInBase(film, i) != null))
        {
          continue;
        }
        innerFilms.Add(new Film
        {
          Id = i,
          Title = film.Title,
          Description = film.Description,
          Icon = pathToSaveIcons + film.Icon,
          Trailer = pathToSaveTrailers + film.Trailer,
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
    public bool SaveToFile()
    {
      var films = GetData();
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.InitialDirectory = "SavedFiles";
      saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          var filePath = saveFileDialog.FileName;
          StreamWriter file = new StreamWriter(filePath, false);
          foreach(var film in films)
          {
            file.WriteLine(film.Title);
            file.WriteLine(film.Description);
            file.WriteLine(film.Icon);
            file.WriteLine(film.Trailer);
            file.WriteLine(film.Year);
            file.WriteLine("----------------------------");
          }
          file.Close();
        }
        catch
        {
          return false;
        }
      }
      return true;
    }
  }
}
