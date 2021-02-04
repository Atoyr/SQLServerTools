using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SQLServerTools.Config
{
  public class DbConnection
  {
    [JsonProperty("ServerName")]
    public List<string> ServerNames { get; private set; } = new List<string>();
    [JsonProperty("HistSize")]
    public int HistSize { get; set; } = 10;

    private string folderName { set; get; }
    private string fileName { set; get; }

    public DbConnection() { }

    public void InsertServer(string serverName)
    {
      var sn = ServerNames.FirstOrDefault(x => x.ToUpper() == serverName.ToUpper());
      if (!string.IsNullOrEmpty(sn))
      {
        ServerNames.Remove(sn);
      }
      ServerNames.Insert(0, serverName);

      if (ServerNames.Count > HistSize)
      {
        ServerNames.RemoveRange(HistSize, ServerNames.Count);
      }
    }

    public static DbConnection Load()
    {
      var path = Util.CreateConfigDirectoryIfNotExists("SqlServerTools");
      var v = Util.Load<Config.DbConnection>(path, "dbConnection");
      v.folderName = path;
      v.fileName = "dbConnection";
      return v;
    }

    public void Save()
    {
      Util.Save(folderName, fileName, this);
    }
  }
}

