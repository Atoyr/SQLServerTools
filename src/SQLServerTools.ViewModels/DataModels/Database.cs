using System;

namespace SQLServerTools.ViewModels.DataModels
{
  public class Database
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Status { get; set; }
    public string RecoveryModel { get; set; }
  }
}


