using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{

  public class Stats
  {
    private const string Query =
        " SELECT "
      + "   DB_ID() AS N'db_id'"
      + " , DB_NAME() AS N'db_name'"
      + " , o.schema_id "
      + " , SCHEMA_NAME(o.schema_id) AS schema_name"
      + " , ss.object_id "
      + " , OBJECT_NAME(dsp.object_id) as table_name"
      + " , ss.stats_id "
      + " , ss.name as stats_name"
      + " , dsp.last_updated "
      + " , dsp.rows"
      + " , dsp.rows_sampled "
      + " , CONVERT(decimal(5,2),CONVERT(decimal(20,3), dsp.rows_sampled) / CONVERT(decimal(20,3), dsp.rows) * 100) As sampling_rate"
      + " , dsp.steps "
      + " , dsp.unfiltered_rows "
      + " , dsp.modification_counter "
      + " FROM"
      + " sys.stats AS ss "
      + " CROSS APPLY "
      + " sys.dm_db_stats_properties(ss.object_id, ss.stats_id) AS dsp "
      + " INNER JOIN"
      + " sys.objects as o"
      + " on ss.object_id = o.object_id"
      + " WHERE"
      + " OBJECT_SCHEMA_NAME(dsp.object_id, DB_ID()) <> N'SYS'";

    public int DatabaseId { set; get; }
    public string DatabaseName { set; get; }
    public int SchemaId { set; get; }
    public string SchemaName { set; get; }
    public int ObjectId { set; get; }
    public string TableName { set; get; }
    public int StatsId { set; get; }
    public string StatsName { set; get; }
    public DateTime LastUpdated { set; get; }
    public long Rows { set; get; }
    public long RowsSampled { set; get; }
    public decimal SamplingRate { set; get; }
    public int Steps { set; get; }
    public long UnfilteredRows { set; get; }
    public long ModificationCounter { set; get; }

    public static IEnumerable<Stats> GetStats(string connectionString, string catalog)
    {
      SqlConnectionStringBuilder builder;
      try 
      {
        builder = new SqlConnectionStringBuilder(connectionString);
        builder.InitialCatalog = catalog;
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

      if (ds.Tables.Count == 1)
      {
        var dt = ds.Tables[0];
        return GetStats(dt);
      }
      else
      {
        // TODO
        throw new Exception("data not found");
      }
    }

    public static IEnumerable<Stats> GetStats(DataTable dt)
    {
      IList<Stats> statsList = new List<Stats>();
      foreach (DataRow row in dt.Rows)
      {
        var s = new Stats()
        {
          ObjectId = (int)row["object_id"],
          DatabaseId = (short)row["db_id"],
          DatabaseName = (string)row["db_name"],
          SchemaId = (int)row["schema_id"],
          SchemaName = (string)row["schema_name"],
          StatsId = (int)row["stats_id"],
          StatsName = (string)row["stats_name"],
          LastUpdated = row.Value<DateTime>("last_updated"),
          Rows = (long)row["rows"],
          RowsSampled = (long)row["rows_sampled"],
          SamplingRate = (decimal)row["sampling_rate"],
          Steps = (int)row["steps"],
          UnfilteredRows = (long)row["unfiltered_rows"],
          ModificationCounter = (long)row["modification_counter"]
        };
        statsList.Add(s);
      }
      return statsList;
    }
  }
}


