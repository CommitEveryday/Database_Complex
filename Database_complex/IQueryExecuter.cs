using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Database_complex
{
    public interface IQueryExecuter
    {
        void ExecuteNonQuery(CommandType cmdType, string sqlText, Dictionary<string, object> parameters);
        object ExecuteScalar(CommandType cmdType, string sqlText, Dictionary<string, object> parameters);
    }
}
