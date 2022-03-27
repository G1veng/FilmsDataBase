using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Windows;

namespace FilmsDataBase.Data
{
  internal class BaseOfFilms
  {
    readonly private string _connectionString = "Data Source=D:\\4 семестр\\РПС\\FilmsDataBase\\FilmsDataBase\\Films.db";
    public void UpdateBase(string oldTitle, string title, string description = null, string pathToImage = null, string pathToTrailer = null, int year = new int())
    {
      string sqlExpression = $"UPDATE Films SET Title = @Title, Description = @Description" +
        $", Icon = @Icon, Trailer = @Trailer, Year = @Year WHERE Title = @NewTitle";
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@Title", title));
        command.Parameters.Add(new SqliteParameter("@Description", description));
        command.Parameters.Add(new SqliteParameter("@Icon", pathToImage));
        command.Parameters.Add(new SqliteParameter("@Trailer", pathToTrailer));
        command.Parameters.Add(new SqliteParameter("@Year", year));
        command.Parameters.Add(new SqliteParameter("@NewTitle", oldTitle));
        command.ExecuteNonQuery();
        connection.Close();
      }
    }
    public void AddToBase(string title, string description = null, string pathToImage = null, string pathToTrailer = null, int year = new int())
    {
      string sqlExpression = $"INSERT INTO Films (Title, Description, Icon, Trailer, Year) VALUES ('{title}','{description}','{pathToImage}'," +
        $"'{pathToTrailer}'," +
        $"{year})";
      using (SqliteConnection connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.ExecuteNonQuery();
        connection.Close();
      }
    }
    public bool FindInBase(out string outTitle, out string outDescription, out string outPathToImage, out string outPathToTrailer, out int outYear, int id = -1)
    {
      outTitle = null;
      outDescription = null;
      outPathToImage = null;
      outPathToTrailer = null;
      string sqlExpression = null;
      outYear = new int();
      if (id  == -1)
        sqlExpression = $"SELECT * FROM Films WHERE (id = {GetFirstId()}";
      else
        sqlExpression = $"SELECT * FROM Films WHERE (id = {id})";
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        using (SqliteDataReader reader = command.ExecuteReader())
        {
          if (!reader.HasRows) return false;
          while (reader.Read())
          {
            int innerId = Convert.ToInt32(reader.GetValue(0));
            outTitle = (string)reader.GetValue(1);
            outDescription = (string)reader.GetValue(2);
            outPathToImage = (string)reader.GetValue(3);
            outPathToTrailer = (string)reader.GetValue(4);
            outYear = Convert.ToInt32(reader.GetValue(5));
            if (innerId == id) break;
          }
        }
        connection.Close();
      }
      return true;
    }
    public bool Exist(string Title)
    {
      if(Title == string.Empty || Title == null) return false;
      string sqlExpression = $"SELECT * FROM Films WHERE (Title = @Title)";
      using (var connection = new SqliteConnection(_connectionString))
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
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        return Convert.ToInt32(command.ExecuteScalar());
      }
    }
    public int GetFirstId()
    {
      string sqlExpression = @"SELECT MIN(id) FROM Films";
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        return Convert.ToInt32(command.ExecuteScalar());
      }
    }
    public void DeleteFromBase(string title)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string sqlExpression = $"DELETE FROM Films where (Title = @Title)";
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@Title", title));
        command.ExecuteNonQuery();
        connection.Close();
      }
    }
  }
}
