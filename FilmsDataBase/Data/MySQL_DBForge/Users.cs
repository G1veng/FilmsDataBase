using FilmsDataBase.Data.MySQL_DBForge.Connection;
using MySql.Data.MySqlClient;
using FilmsDataBase.Models;
using System.Data.Common;
using System.Collections.Generic;

namespace FilmsDataBase.Data.MySQL_DBForge
{
  public class Users
  {
    static public List<User> GetAllUsers()
    {
      var _connection = DBUtils.GetDBConnection();
      List<User> users = new List<User>();
      _connection.Open();
      try
      {
        string sqlQuery = "select * from accounts";
        MySqlCommand cmd = new MySqlCommand(sqlQuery, _connection);
        using (DbDataReader reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read())
            {
              users.Add(new User()
              {
                UserId = reader.GetInt32(0),
                UserMail = reader.GetString(1),
                UserPassword = reader.GetString(2),
                UserLogin = reader.GetString(3),
              });
            }
          }
        }
      }
      catch
      {
        return null;
      }
      _connection.Close();
      return users;
    }
  }
}
