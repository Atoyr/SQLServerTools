using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{
  public static class Util
  {
    public static T Value<T>(this DataRow dataRow, string key) => dataRow[key] == DBNull.Value ? default(T) : (T)dataRow[key];
  }
}
