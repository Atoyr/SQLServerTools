using System;
using System.IO;
using Newtonsoft.Json;

namespace SQLServerTools.Config
{
  public static class Util
  {
    public static string CreateConfigDirectoryIfNotExists(string appName)
    {
      var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      var folderName = "." + appName;
      var configDir = Path.Combine(home, folderName);

      if (!Directory.Exists(configDir))
      {
        Directory.CreateDirectory(configDir);
      }

      return configDir;
    }

    public static T Load<T>(string folderName, string fileName)
    {
      var path = Path.Combine(folderName, fileName);
      if (!File.Exists(path))
      {
        return new T();
      }
      else 
      {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
      }
    }

    public static void Save(string folderName, string filePath, object obj)
    {
      var path = Path.Combine(folderName, fileName);
      if (!Directory.Exists(folderName))
      {
        return;
      }
      else 
      {
        var json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(path, json);
      }
    }
  }
}
