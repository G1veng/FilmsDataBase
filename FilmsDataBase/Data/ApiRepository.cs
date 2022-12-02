using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmsDataBase.Infrastructure;
using FilmsDataBase.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Configuration;
using FilmsDataBase.ViewModels;

namespace FilmsDataBase.Data
{
  public class ApiRepository : IRepository
  {
    //private readonly string token = Properties.Settings.Default.token;
    public async Task UpdateBase(RawFilm film, int id)
    {
      var formContent = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("id", id.ToString()),
        new KeyValuePair<string, string>("title", film.Title),
        new KeyValuePair<string, string>("description", film.Description),
        new KeyValuePair<string, string>("icon", film.Icon),
        new KeyValuePair<string, string>("trailer", film.Trailer),
        new KeyValuePair<string, string>("year", film.Year.ToString())
      }); ;
      HttpClient client = new HttpClient();
      HttpResponseMessage response = await client.PutAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString
        + "/film/Update/" + id.ToString() + "?token=" + Properties.Settings.Default.token, formContent);
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "Done")
        {
          return;
        }
      }
      throw new ArgumentNullException(id.ToString());
    }
    public async Task AddToBase(RawFilm film)
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
      HttpResponseMessage response = await client.PostAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString
        + "/film/Create" + "?token=" + Properties.Settings.Default.token, formContent);
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = await responseContent.ReadAsStringAsync();
        if (jsonString == "Done") { 
          return; 
        }
      }
      throw new AccessViolationException();
    }
    public async Task DeleteFromBase(int id)
    {
      HttpClient client = new HttpClient();
      HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString
        + "/film/delete/" + id.ToString() + "?token=" + Properties.Settings.Default.token);
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        if (jsonString == "Done")
        {
          return;
        }
      }
      throw new ArgumentNullException(id.ToString());
    }
    public RawFilm FindInBase(RawFilm film, int id)
    {
      string jsonString = string.Empty;
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.GetAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString + "/film/get/" + id.ToString()
        + "?token=" + Properties.Settings.Default.token).Result;
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
      HttpResponseMessage response = client.GetAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString + "/film/get/" + id.ToString()
        + "?token=" + Properties.Settings.Default.token).Result;
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
      HttpResponseMessage response = client.GetAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString + "/film/getall"
        + "?token=" + Properties.Settings.Default.token).Result;
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
      HttpResponseMessage response = client.GetAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString + "/film/getall"
        + "?token=" + Properties.Settings.Default.token).Result;
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
