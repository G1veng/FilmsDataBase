using FilmsDataBase.Models;
using System.Collections.Generic;

namespace FilmsDataBase.Infrastructure
{
  public interface IFilmService
  {
    List<Film> GetData();
    void SetData(string title, string description, string icon, string trailer, System.DateTime year);
    void DeleteData(int id);
    void UpdataDataBase(int id, string newTitle, string newDescription, string newIcon, string newTrailer, System.DateTime newYear);
    bool Exist(int id);
    bool IsEmpty();
    bool SaveToFile();
  }
}
