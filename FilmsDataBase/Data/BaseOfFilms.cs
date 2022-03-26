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
    public void AddToBase(string title, string description = null, string pathToImage = null, string pathToTrailer = null, int year = new int())
    {
      string sqlExpression = $"INSERT INTO Films (Title, Description, Icon, Trailer, Year) VALUES ('{title}','{description}','{pathToImage}'," +
        $"'{pathToTrailer}'," +
        $"{year})";
      using (SqliteConnection connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
      }
    }
    public bool FindInBase(int id, out string outTitle, out string outDescription, out string outPathToImage, out string outPathToTrailer, out int outYear)
    {
      outTitle = null;
      outDescription = null;
      outPathToImage = null;
      outPathToTrailer = null;
      outYear = new int();
      string sqlExpression = @"SELECT * FROM Films where (id = @id)";
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));
        using (SqliteDataReader reader = command.ExecuteReader())
        {
          if (!reader.HasRows) return false;
          while (reader.Read())
          {
            outTitle = (string)reader.GetValue(1);
            outDescription = (string)reader.GetValue(2);
            outPathToImage = (string)reader.GetValue(3);
            outPathToTrailer = (string)reader.GetValue(4);
            outYear = Convert.ToInt32(reader.GetValue(5));
          }
        }
      }
      return true;
    }
    public void DeleteFromBase(int id)
    {
      using (var connection = new SqliteConnection(_connectionString))
      {
        connection.Open();
        string sqlExpression = $"DELETE FROM Films where (id = @id)";
        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));
        command.ExecuteNonQuery();
      }
    }
  }
}
