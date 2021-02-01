using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{
  public class Database
  {
    private const string Query =
      "use master "
      + " SELECT "
      + "  name                 as Name"
      + " ,database_id          as DatabaseId"
      + " ,create_date          as CreateDate"
      + " ,state_desc           as Status"
      + " ,recovery_model_desc  as RecoveryModel"
      + " FROM sys.databases";

    public string Name { get; set; }
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Status { get; set; }
    public string RecoveryModel { get; set; }

    public static IEnumerable<Database> GetDatabases(string connectionString)
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
      IList<Database> databases = new List<Database>();
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

      if (ds.Tables.Count == 1)
      {
        var dt = ds.Tables[0];
        foreach (DataRow row in dt.Rows)
        {
          var db = new Database()
          {
            Name = (string)row["Name"],
            Id = (int)row["DatabaseId"],
            CreateDate = (DateTime)row["CreateDate"],
            Status = (string)row["Status"],
            RecoveryModel = (string)row["RecoveryModel"]
          };
          databases.Add(db);
        }
      }

      return databases;
    }
  }

}

