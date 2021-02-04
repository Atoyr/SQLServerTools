using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SQLServerTools.Config
{
  public class DbConnection
  {
    [JsonProperty("ServerName")]
    internal List<string> ServerNames { get; private set; } = new List<string>();
    [JsonProperty("HistSize")]
    public int HistSize { get; set; } = 10;

    internal DbConnection() { }

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
  }
}

