using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerTools.Database
{
  public class ServerProperty
  {
    private const string ServerPropertyQuery = 
      "SELECT "
      + "  SERVERPROPERTY('MachineName')          as MachineName"
      + " ,SERVERPROPERTY('InstanceName')         as InstanceName"
      + " ,SERVERPROPERTY('ServerName')           as ServerName"
      + " ,SERVERPROPERTY('ProductVersion')       as ProductVersion"
      + " ,SERVERPROPERTY('ProductMajorVersion')  as ProductMajorVersion"
      + " ,SERVERPROPERTY('ProductLevel')         as ProductLevel"
      + " ,SERVERPROPERTY('Edition')              as Edition";

    public string MachineName { get; set; }
    public string InstanceName { get; set; }
    public string ServerName { get; set; }
    public string ProductVersion { get; set; }
    public string ProductMajorVersion { get; set; }
    public string ProductLevel { get; set; }
    public string Edition { get; set; }

    public string Version 
    {
      get 
      {
        if (string.IsNullOrEmpty(ProductVersion))
        {
          return string.Empty;
        }

        var sb = new StringBuilder();
        sb.Append("Microsoft SQL Server");
        sb.Append(" ");
        switch (ProductMajorVersion)
        {
          case "11":
            sb.Append("2012");
            break;
          case "12":
            sb.Append("2014");
            break;
          case "13":
            sb.Append("2016");
            break;
          case "14": 
            sb.Append("2017");
            break;
          case "15":
            sb.Append("2019");
            break;
        }
        if (!string.IsNullOrEmpty(ProductLevel))
        {
          sb.Append(" ");
          sb.Append("(").Append(ProductLevel).Append(")");
        }
        sb.Append(" ");
        sb.Append(Edition);
        sb.Append(" - ");
        sb.Append(ProductVersion);

        return sb.ToString();
      }
    }

    public static ServerProperty GetServerProperty(string connectionString)
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

      var sp = new ServerProperty();
      try
      {
        using(var conn = new SqlConnection(builder.ConnectionString))
        using(var cmd = new SqlCommand(ServerPropertyQuery))
        {
          cmd.Connection = conn;
          cmd.CommandType = CommandType.Text;
          conn.Open();
          using (var reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              sp.MachineName  = (string)reader["MachineName"];
              sp.InstanceName  = (string)reader["InstanceName"];
              sp.ServerName  = (string)reader["ServerName"];
              sp.ProductVersion  = (string)reader["ProductVersion"];
              sp.ProductMajorVersion  = (string)reader["ProductMajorVersion"];
              sp.ProductLevel  = (string)reader["ProductLevel"];
              sp.Edition  = (string)reader["Edition"];
            }
          }
          conn.Close();
        }
      }
      catch (Exception e)
      {
        // TODO
        throw e;
      }
      return sp;
    }
  }
}
