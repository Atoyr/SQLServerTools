using System;
using SQLServerTools.Database;

namespace SQLServerTools.ViewModels.DataModels
{
  public class Table
  {
    public int ObjectId { set; get; }
    public int DatabaseId { set; get; }
    public string DatabaseName { set; get; }
    public int SchemeId { set; get; }
    public string SchemeName { set; get; }
    public string Name { set; get; }
    public DateTime CreateDate { set; get; }
    public DateTime ModifyDate { set; get; }
    public int ReservedSizeMb { set; get; }
    public int DataSizeMb { set; get; }
    public int IndexSizeMb { set; get; }
    public int UnusedSizeMb { set; get; }
    public int RowCount { set; get; }
    public int ColumnCount { set; get; }
    public int IndexCount { set; get; }
    public int TriggerCount { set; get; }
    public int StatsCount { set; get; }

    public static Table ConvertTable(SQLServerTools.Database.Table table) 
    {
      return new Table()
      {
        ObjectId = table.ObjectId,
        DatabaseId = table.DatabaseId,
        DatabaseName = table.DatabaseName,
        SchemeId = table.SchemeId,
        SchemeName = table.SchemeName,
        Name = table.Name,
        CreateDate = table.CreateDate,
        ModifyDate = table.ModifyDate,
        ReservedSizeMb = table.ReservedSizeMb,
        DataSizeMb = table.DataSizeMb,
        IndexSizeMb = table.IndexSizeMb,
        UnusedSizeMb = table.UnusedSizeMb,
        RowCount = table.RowCount,
        ColumnCount = table.ColumnCount,
        IndexCount = table.IndexCount,
        TriggerCount = table.TriggerCount,
        StatsCount = table.StatsCount
      };
    }
  }
}

