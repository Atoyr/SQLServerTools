using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSQL;
using TSQL.Tokens;

namespace SQLServerTools.Database.Text
{
  public class SqlFormatter : IFormatter
  {
    public string FormatText(string query)
    {
      foreach(var token in TSQLTokenizer.ParseTokens(query))
      {
        if ((token.Type == TSQLTokenType.Identifier
            && token.Text.ToLower() == "go"))
        {

        }
      }

      return string.Empty;
    }
  }
}
