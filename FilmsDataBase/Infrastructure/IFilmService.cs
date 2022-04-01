using FilmsDataBase.Models;
using System.Collections.Generic;

namespace FilmsDataBase.Infrastructure
{
  public interface IFilmService
  {
    List<Film> GetData();
    void SetData(string title, string description, string icon, string trailer, System.DateTime year);
    void DeleteData(string title);
    void UpdataDataBase(string title, string newTitle, string newDescription, string newIcon, string newTrailer, System.DateTime newYear);
    bool Exist(string title);
    bool IsEmpty();
  }
}
