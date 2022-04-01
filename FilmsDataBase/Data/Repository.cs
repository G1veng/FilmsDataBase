using Microsoft.Data.Sqlite;
using System;
using System.Configuration;
using System.Threading.Tasks;
using FilmsDataBase.Models;
using FilmsDataBase.Infrastructure;

namespace FilmsDataBase.Data
{
  internal class Repository : IRepository
  {
    public async Task UpdateBase(RawFilm film, string oldTitle)
    {
      string sqlExpression = $"UPDATE Films SET Title = @Title, Description = @Description" +
        $", Icon = @Icon, Trailer = @Trailer, Year = @Year WHERE Title = @OldTitle";
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@Title", film.Title));
        command.Parameters.Add(new SqliteParameter("@Description", film.Description));
        command.Parameters.Add(new SqliteParameter("@Icon", film.Icon));
        command.Parameters.Add(new SqliteParameter("@Trailer", film.Trailer));
        command.Parameters.Add(new SqliteParameter("@Year", film.Year));
        command.Parameters.Add(new SqliteParameter("@OldTitle", oldTitle));
        await command.ExecuteNonQueryAsync();
        connection.Close();
      }
    }
    public async Task AddToBase(RawFilm film)
    {
      string sqlExpression = "INSERT INTO Films (Title, Description, Icon, Trailer, Year) VALUES (@Title, @Description, @PathToTrailer, @PathToImage, @Year)";
      SqliteConnection connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
      connection.Open();
      SqliteCommand command = new SqliteCommand(sqlExpression, connection);
      command.Parameters.Add(new SqliteParameter("@Title", film.Title));
      command.Parameters.Add(new SqliteParameter("@Description", film.Description));
      command.Parameters.Add(new SqliteParameter("@PathToImage", film.Icon));
      command.Parameters.Add(new SqliteParameter("@PathToTrailer", film.Trailer));
      command.Parameters.Add(new SqliteParameter("@Year", film.Year));
      await command.ExecuteNonQueryAsync();
      connection.Close();
    }
    public RawFilm FindInBase(RawFilm film, int id = -1)
    {
      film.Title = null;
      film.Description = null;
      film.Icon = null;
      film.Trailer = null;
      film.Year = new DateTime();
      string sqlExpression = $"SELECT * FROM Films WHERE (id = @Id)";
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        if (id == -1)
          command.Parameters.Add(new SqliteParameter("@Id", GetFirstId()));
        else
          command.Parameters.Add(new SqliteParameter("@Id", id));
        using (SqliteDataReader reader = command.ExecuteReader())
        {
          if (!reader.HasRows) return film;
          while (reader.Read())
          {
            int innerId = Convert.ToInt32(reader.GetValue(0));
            film.Title = (string)reader.GetValue(1);
            film.Description = (string)reader.GetValue(2);
            film.Icon = (string)reader.GetValue(3);
            film.Trailer = (string)reader.GetValue(4);
            film.Year = Convert.ToDateTime(reader.GetValue(5));
            if (innerId == id) break;
          }
        }
        connection.Close();
      }
      return film;
    }
    public bool Exist(string Title)
    {
      if(Title == string.Empty || Title == null) return false;
      string sqlExpression = $"SELECT * FROM Films WHERE (Title = @Title)";
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@Title", Title));
        if (Convert.ToInt32(command.ExecuteScalar()) == 0) return false;
        return true;
      }
    }
    public int GetCountOfRows()
    {
      string sqlExpression = @"SELECT COUNT(*) FROM Films";
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        return Convert.ToInt32(command.ExecuteScalar());
      }
    }
    public int GetFirstId()
    {
      string sqlExpression = @"SELECT MIN(id) FROM Films";
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        return Convert.ToInt32(command.ExecuteScalar());
      }
    }
    public async Task DeleteFromBase(string title)
    {
      using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
      {
        connection.Open();
        string sqlExpression = $"DELETE FROM Films where (Title = @Title)";
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@Title", title));
        await command.ExecuteNonQueryAsync();
        connection.Close();
      }
    }
  }
}
