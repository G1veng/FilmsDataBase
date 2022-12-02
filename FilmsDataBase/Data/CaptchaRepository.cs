using FilmsDataBase.Models;
using FilmsDataBase.Data.DBForge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FilmsDataBase.Data
{
  public class CaptchaRepository
  {
    static public Captcha GetRandomCaptcha()
    {
      using (var connection = DBUtils.GetDBConnection())
      {
        int countOfCapthcas = 0;
        string sqlExpression = "select count(id) from capthas";
        connection.Open();
        MySqlCommand command = new MySqlCommand(sqlExpression, connection);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          if (!reader.HasRows)
          {
            connection.Close();
            return null;
          }
          while (reader.Read())
          {
            countOfCapthcas = reader.GetInt32(0);
          }
        }
        sqlExpression = "select capthas.path, capthas.answer from capthas where id = @id";
        command = new MySqlCommand(sqlExpression, connection);
        Random random = new Random();
        int id = random.Next(1, countOfCapthcas + 1);
        command.Parameters.Add(new MySqlParameter("@id", id));
        using (MySqlDataReader reader = command.ExecuteReader())
        {
          if (!reader.HasRows)
          {
            connection.Close();
            return null;
          }
          while (reader.Read())
          {
            var captcha = new Captcha()
            {
              Path = reader.GetString(0),
              Answer = reader.GetString(1),
            };
            connection.Close();
            return captcha;
          }
        }
        connection.Close();
        return null;
      }
    }
  }
}
