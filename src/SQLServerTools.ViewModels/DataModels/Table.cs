using System;
using SQLServerTools.Database;

namespace SQLServerTools.ViewModels.DataModels
{
  public class Table
  {
    public int ObjectId { set; get; }
    public int DatabaseId { set; get; }
    public string DatabaseName { set; get; }
    public int SchemaId { set; get; }
    public string SchemaName { set; get; }
    public string Name { set; get; }
    public DateTime CreateDate { set; get; }
    public DateTime ModifyDate { set; get; }
    public long ReservedSizeMb { set; get; }
    public long DataSizeMb { set; get; }
    public long IndexSizeMb { set; get; }
    public long UnusedSizeMb { set; get; }
    public long RowCount { set; get; }
    public int ColumnCount { set; get; }
    public int IndexCount { set; get; }
    public int TriggerCount { set; get; }
    public int StatsCount { set; get; }

    public string SchemaTableName 
    {
      get => SchemaName + "." + Name;
    }

    public string DatabaseSchemaTableName 
    {
      get => DatabaseName + "." + SchemaName + "." + Name;
    }

    public static Table ConvertTable(SQLServerTools.Database.Table table) 
    {
      return new Table()
      {
        ObjectId = table.ObjectId,
        DatabaseId = table.DatabaseId,
        DatabaseName = table.DatabaseName,
        SchemaId = table.SchemaId,
        SchemaName = table.SchemaName,
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

