using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{

  public class Table
  {
    private const string Query =
      " select  "
      + " t.object_id as ObjectId "
      + " ,db_id() as DatabaseId "
      + " ,db_name() as DatabaseName "
      + " ,s.schema_id as ShemaId "
      + " ,s.name as ShemaName "
      + " ,t.name as Name "
      + " ,t.create_date as CreateDate "
      + " ,t.modify_date as ModifyDate "
      + " ,p.reserved as ReservedSize "
      + " ,p.data as DataSize "
      + " ,p.index_size as IndexSize "
      + " ,p.unused as UnusedSize "
      + "  "
      + " ,p.rows as N'RowCount' "
      + " ,c.count as ColumnCount "
      + " ,i.count as IndexCount "
      + " ,isnull(tr.count, 0) as TriggerCount "
      + " ,isnull(st.count, 0) as StatsCount "
      + " from sys.tables as t "
      + " inner join sys.schemas as s "
      + " on t.schema_id = s.schema_id "
      + " inner join ( "
      + "   select  "
      + "   dp.object_id as object_id "
      + "   ,row_count as rows "
      + "   ,sum(reserved_page_count) * 8 as reserved "
      + "   ,data_count * 8 as data "
      + "   ,(sum(used_page_count) - data_count) * 8 as  index_size "
      + "   ,case when sum(reserved_page_count) > sum(used_page_count) then (sum(reserved_page_count) - sum(used_page_count)) * 8 else 0 end as  unused "
      + "   from sys.dm_db_partition_stats AS DP "
      + "   join ( "
      + "     select  "
      + "     object_id "
      + "     ,in_row_data_page_count + lob_used_page_count + row_overflow_used_page_count as data_count "
      + "     from sys.dm_db_partition_stats "
      + "     where "
      + "     index_id < 2 "
      + "   ) as data_partition "
      + "   on DP.object_id = data_partition.object_id "
      + "   group by "
      + "   dp.object_id,row_count,data_count "
      + " ) as p "
      + " on t.object_id = p.object_id "
      + " inner join (select object_id, count(*) as count from sys.indexes group by object_id) as i "
      + " on t.object_id = i.object_id "
      + " inner join (select object_id, count(*) as count from sys.columns group by object_id) as c "
      + " on t.object_id = c.object_id "
      + " left join (select parent_id, count(*) as count from sys.triggers group by parent_id) as tr "
      + " on t.object_id = tr.parent_id "
      + " left join (select object_id, count(*) as count from sys.stats group by object_id) as st "
      + " on t.object_id = st.object_id ";

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

    public static IEnumerable<Table> GetTables(string connectionString, string catalog)
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
        return GetTables(dt);
      }
      else
      {
        // TODO
        throw new Exception("data not found");
      }
    }

    public static IEnumerable<Table> GetTables(DataTable dt)
    {
      IList<Table> tables = new List<Table>();
      foreach (DataRow row in dt.Rows)
      {
        var t = new Table()
        {
          ObjectId = (int)row["ObjectId"],
          DatabaseId = (int)row["DatabaseId"],
          DatabaseName = (string)row["DatabaseName"],
          SchemeId = (int)row["SchemeId"],
          SchemeName = (string)row["SchemeName"],
          Name = (string)row["Name"],
          CreateDate = (DateTime)row["CreateDate"],
          ModifyDate = (DateTime)row["ModifyDate"],
          ReservedSizeMb = (int)row["ReservedSizeMb"],
          DataSizeMb = (int)row["DataSizeMb"],
          IndexSizeMb = (int)row["IndexSizeMb"],
          UnusedSizeMb = (int)row["UnusedSizeMb"],
          RowCount = (int)row["RowCount"],
          ColumnCount = (int)row["ColumnCount"],
          IndexCount = (int)row["IndexCount"],
          TriggerCount = (int)row["TriggerCount"],
          StatsCount = (int)row["StatsCount"]
        };
        tables.Add(t);
      }
      return tables;
    }
  }
}

