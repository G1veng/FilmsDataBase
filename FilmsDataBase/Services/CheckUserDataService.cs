using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace FilmsDataBase.Services
{
  public class CheckUserDataService
  {
    static public string CheckUserData(string password, string login)
    {
      var formContent = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("username", login),
        new KeyValuePair<string, string>("password", password),
      });
      HttpClient client = new HttpClient();
      HttpResponseMessage response = client.PostAsync(ConfigurationManager.ConnectionStrings["Host"].ConnectionString
        + "/account/token", formContent).Result;
      if (response.StatusCode == HttpStatusCode.OK)
      {
        HttpContent responseContent = response.Content;
        var jsonString = responseContent.ReadAsStringAsync().Result;
        return jsonString;
      }
      return string.Empty;
    }
  }
}
