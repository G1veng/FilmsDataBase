using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FilmsDataBase.Data.Tests
{
  [TestClass()]
  public class RepositoryTests
  {
    [TestMethod()]
    public async void AddToBaseTest()
    {
      Repository repository = new Repository();
      string description = "test";
      string icon = "test";
      int id = 101010;
      string trailer = "test";
      System.DateTime year = DateTime.Now;
      string title = "test";
      await repository.AddToBase(new Models.RawFilm() { Description = description, Icon = icon, Id = id, Trailer = trailer, Year = year, Title = title});
      var result = repository.FindInBase(new Models.RawFilm(), id);
      Assert.AreEqual(description, result.Description);
      Assert.AreEqual(icon, result.Icon);
      Assert.AreEqual(id, result.Id);
      Assert.AreEqual(trailer, result.Trailer);
      Assert.AreEqual(year, result.Year);
      Assert.AreEqual(title, result.Title);
      await repository.DeleteFromBase(id);
    }

    [TestMethod()]
    public async void DeleteFromBaseTest()
    {
      Repository repository = new Repository();
      string description = "test";
      string icon = "test";
      int id = 101010;
      string trailer = "test";
      System.DateTime year = DateTime.Now;
      string title = "test";
      await repository.AddToBase(new Models.RawFilm() { Description = description, Icon = icon, Id = id, Trailer = trailer, Year = year, Title = title });
      await repository.DeleteFromBase(id);
      var result = repository.FindInBase(new Models.RawFilm(), id);
      if (result != null)
        Assert.Fail();
    }
  }
}