using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerTools.Database
{

  public class User
  {
    private const string Query =
      " select "
      + "  sid"
      + " ,createdate"
      + " ,updatedate"
      + " ,name"
      + " ,dbname"
      + " ,isnull(language, N'') as language"
      + " ,denylogin"
      + " ,hasaccess"
      + " ,isntname"
      + " ,isntgroup"
      + " ,isntuser"
      + " ,sysadmin"
      + " ,securityadmin"
      + " ,serveradmin"
      + " ,setupadmin"
      + " ,processadmin"
      + " ,diskadmin"
      + " ,dbcreator"
      + " ,bulkadmin"
      + " from master.sys.syslogins" ;

    public byte[] Sid { set; get; }
    public DateTime CreateDate { set; get; }
    public DateTime UpdateDate { set; get; }
    public string Name { set; get; }
    public string DefaultDbName { set; get; }
    public string Language { set; get; }
    public bool Denylogin { set; get; }
    public bool Hasaccess { set; get; }
    public bool Isntname { set; get; }
    public bool Isntgroup { set; get; }
    public bool Isntuser { set; get; }
    public bool Sysadmin { set; get; }
    public bool Securityadmin { set; get; }
    public bool Serveradmin { set; get; }
    public bool Setupadmin { set; get; }
    public bool Processadmin { set; get; }
    public bool Diskadmin { set; get; }
    public bool Dbcreator { set; get; }
    public bool Bulkadmin { set; get; }

    public static User GetUser(string connectionString)
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
      var username = string.Empty;
      if (builder.IntegratedSecurity) {
        if (string.IsNullOrEmpty(Environment.UserDomainName))
        {
          username = Environment.UserName;
        }
        else
        {
          username = $"{Environment.UserDomainName}\\{Environment.UserName}";
        }
      }
      else 
      {
        username = builder.UserID;
      }
      return GetUser(connectionString, username);
    }

    public static User GetUser(string connectionString, string username)
    {
      var users = GetUsers(connectionString);
      return users.FirstOrDefault(x => x.Name.ToUpper() == username.ToUpper());
    }

    public static IEnumerable<User> GetUsers(string connectionString)
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
        return GetUsers(dt);
      }
      else
      {
        // TODO
        throw new Exception("data not found");
      }
    }

    public static IEnumerable<User> GetUsers(DataTable dt)
    {
      IList<User> users = new List<User>();
      foreach (DataRow row in dt.Rows)
      {
        var u = new User()
        {
          Sid = (byte[])row["sid"],
          CreateDate = (DateTime)row["createdate"],
          UpdateDate = (DateTime)row["updatedate"],
          Name = (string)row["name"],
          DefaultDbName = (string)row["dbname"],
          Language = (string)row["language"],
          Denylogin = (int)row["denylogin"] == 1 ? true : false,
          Hasaccess = (int)row["hasaccess"] == 1 ? true : false,
          Isntname = (int)row["isntname"] == 1 ? true : false,
          Isntgroup = (int)row["isntgroup"] == 1 ? true : false,
          Isntuser = (int)row["isntuser"] == 1 ? true : false,
          Sysadmin = (int)row["sysadmin"] == 1 ? true : false,
          Securityadmin = (int)row["securityadmin"] == 1 ? true : false,
          Serveradmin = (int)row["serveradmin"] == 1 ? true : false,
          Setupadmin = (int)row["setupadmin"] == 1 ? true : false,
          Processadmin = (int)row["processadmin"] == 1 ? true : false,
          Diskadmin = (int)row["diskadmin"] == 1 ? true : false,
          Dbcreator = (int)row["dbcreator"] == 1 ? true : false,
          Bulkadmin = (int)row["bulkadmin"] == 1 ? true : false,
        };
        users.Add(u);
      }
      return users;
    }
  }
}


