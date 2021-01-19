using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerTools.Database
{
  public static class Common
  {
    public static void Ping(string connectionString)
    {
      SqlConnectionStringBuilder builder;
      try 
      {
        builder = new SqlConnectionStringBuilder(connectionString);
      }
      catch (Exception e)
      {
        throw e;
      }
      
      using(var conn = new SqlConnection(builder.ConnectionString))
      {
        try
        {
          conn.Open();
          conn.Close();
        }
        catch (Exception e)
        {
          throw e;
        }
      }
      return;
    }
  }
}

