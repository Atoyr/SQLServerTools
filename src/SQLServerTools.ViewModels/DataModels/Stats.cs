using System;
using SQLServerTools.Database;

namespace SQLServerTools.ViewModels.DataModels
{
  public class Stats
  {
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

    public static Stats ConvertStats(SQLServerTools.Database.Stats stats) 
    {
      return new Stats()
      {
        DatabaseId = stats.DatabaseId,
        DatabaseName = stats.DatabaseName,
        SchemaId = stats.SchemaId,
        SchemaName = stats.SchemaName,
        ObjectId = stats.ObjectId,
        TableName = stats.TableName,
        StatsId = stats.StatsId,
        StatsName = stats.StatsName,
        LastUpdated = stats.LastUpdated,
        Rows = stats.Rows,
        RowsSampled = stats.RowsSampled,
        SamplingRate = stats.SamplingRate,
        Steps = stats.Steps,
        UnfilteredRows = stats.UnfilteredRows,
        ModificationCounter = stats.ModificationCounter
      };
    }
  }
}

