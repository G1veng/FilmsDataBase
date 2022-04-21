using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using FilmsDataBase.Infrastructure;
using FilmsDataBase.Models;
using FilmDataAccess;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace FilmsDataBase.Data
{
  public class ApiRepository : IRepository
  {
    public Task UpdateBase(RawFilm film, int id)
    {
      var formContent = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("id", id.ToString()),
        new KeyValuePair<string, string>("title", film.Title),
        new KeyValuePair<string, string>("description", film.Description),
        new KeyValuePair<string, string>("icon", film.Icon),
        new KeyValuePair<string, string>("trailer", film.Trailer),
        new KeyValuePair<string, string>("year", film.Year.ToString())
      });
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.PutAsync("https://localhost:44353/film/Update/" + id.ToString(), formContent).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "Done") return Task.CompletedTask;
      }
      throw new ArgumentNullException(id.ToString());
    }
    public Task AddToBase(RawFilm film)
    {
      var formContent = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("title", film.Title),
        new KeyValuePair<string, string>("description", film.Description),
        new KeyValuePair<string, string>("icon", film.Icon),
        new KeyValuePair<string, string>("trailer", film.Trailer),
        new KeyValuePair<string, string>("year", film.Year.ToString())
      });
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.PostAsync("https://localhost:44353/film/Create", formContent).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "Done") return Task.CompletedTask;
      }
      throw new AccessViolationException();
    }
    public Task DeleteFromBase(int id)
    {
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.DeleteAsync("https://localhost:44353/film/delete/" + id.ToString()).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "Done") return Task.CompletedTask;
      }
      throw new ArgumentNullException(id.ToString());
    }
    public RawFilm FindInBase(RawFilm film, int id)
    {
      string jsonString = string.Empty;
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.GetAsync("https://localhost:44353/film/get/" + id.ToString()).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        jsonString = responseContent.ReadAsStringAsync().Result;
      }
      if (jsonString == "We have not these film") return null;
      dynamic json = JsonConvert.DeserializeObject(jsonString);
      film.Id = json["id"];
      film.Title = json["title"];
      film.Description = json["description"];
      film.Icon = json["icon"];
      film.Trailer = json["trailer"];
      film.Year = json["year"];
      return film;
    }
    public bool Exist(int id)
    {
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.GetAsync("https://localhost:44353/film/get/" + id.ToString()).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "We have not these film") return false;
        return true;
      }
      throw new AccessViolationException();
    }
    public int GetCountOfRows()
    {
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.GetAsync("https://localhost:44353/film/getall").Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        dynamic jsonString = responseContent.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject(jsonString).Count;
      }
      throw new AccessViolationException();
    }
    public int GetFirstId()
    {
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.GetAsync("https://localhost:44353/film/getall").Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        dynamic jsonString = responseContent.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject(jsonString)[0]["id"];
      }
      throw new AccessViolationException();
    }
  }
}
