﻿using MySql.Data.MySqlClient;

namespace FilmsDataBase.Data.MySQL_DBForge.Connection
{
  public class DBUtils
  {
    public static MySqlConnection GetDBConnection()
    {
      string host = "localhost";
      int port = 3306;
      string database = "accounts";
      string username = "Giveng";
      string password = "Fybcbvjdf2002";
      return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
    }
  }
}