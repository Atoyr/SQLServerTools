using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{

  public class Status
  {
    private const string Query =
        " SELECT "
      + "   DB_ID() AS N'db_id'"
      + " , DB_NAME() AS N'db_name'"
      + " , SCHEMA_ID() AS N'schema_id'"
      + " , SCHEMA_NAME() AS N'schema_name'"
      + " , USER_NAME() AS N'user_name'"
      + " , SUSER_NAME() AS N'suser_name'";

    public int DatabaseId { set; get; }
    public string DatabaseName { set; get; }
    public int SchemaId { set; get; }
    public string SchemaName { set; get; }
    public string UserName { set; get; }
    public string SuserName { set; get; }

    public static Status GetStatus(string connectionString)
    {
      SqlConnectionStringBuilder builder;
      try 
      {
        builder = new SqlConnectionStringBuilder(connectionString);
      }
      catch (Exception e)
      {
        // TODO 
        throw e;
      }

      DataSet ds = new DataSet();
      try
      {
        using(var conn = new SqlConnection(builder.ConnectionString))
          using(var cmd = new SqlCommand(Query))
          using(var adapter = new SqlDataAdapter())
          {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
          }
      }
      catch (Exception e)
      {
        // TODO
        throw e;
      }

      if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
      {
        var dr = ds.Tables[0].Rows[0];
        var s = new Status()
        {
          DatabaseId = dr.Value<int>("db_id")
          ,DatabaseName = dr.Value<string>("db_name")
          ,SchemaId = dr.Value<int>("schema_id")
          ,SchemaName = dr.Value<string>("schema_name")
          ,UserName = dr.Value<string>("user_name")
          ,SuserName = dr.Value<string>("suser_name")
        };
        return s;
      }
      else
      {
        // TODO
        throw new Exception("data not found");
      }
    }
  }
}



