using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using log4net;

namespace Database_complex
{
    class QueryExecuter : IQueryExecuter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QueryExecuter)); 

        protected DbCommand Command {get; set;}

        protected string ChangeParamPrefixTo { get; set; }

        public QueryExecuter(DbCommand command, string changeParamPrefixTo = null)
        {
            Command = command;
            ChangeParamPrefixTo = changeParamPrefixTo;
        }

        public void ExecuteNonQuery(CommandType cmdType, string sqlText, Dictionary<string, object> parameters)
        {
            Command.Parameters.Clear();
            Command.CommandType = cmdType;
            Command.CommandText = ChangeParamPrefix(sqlText);
            if (parameters != null && parameters.Count() > 0)
            {
                foreach (KeyValuePair<string, object> paramInfo in parameters)
                {
                    DbParameter newParam = new MySqlParameter(ChangeParamPrefix(paramInfo.Key), paramInfo.Value);
                    Command.Parameters.Add(newParam);
                }
            }
            try
            {
                Command.Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                log.Fatal(null, ex);
                throw;
            }
            finally
            {
                Command.Connection.Close();
            }
        }

        protected string ChangeParamPrefix(string oldStr)
        {
            if (ChangeParamPrefixTo == null)
                return oldStr;
            return oldStr.Replace("?", ChangeParamPrefixTo);
        }


        public object ExecuteScalar(CommandType cmdType, string sqlText, Dictionary<string, object> parameters)
        {
            Command.Parameters.Clear();
            Command.CommandType = cmdType;
            Command.CommandText = ChangeParamPrefix(sqlText);
            if (parameters != null && parameters.Count() > 0)
            {
                foreach (KeyValuePair<string, object> paramInfo in parameters)
                {
                    DbParameter newParam = new MySqlParameter(ChangeParamPrefix(paramInfo.Key), paramInfo.Value);
                    Command.Parameters.Add(newParam);
                }
            }
            object result = null;
            try
            {
                Command.Connection.Open();
                result = Command.ExecuteScalar();
            }
            catch (DbException ex)
            {
                log.Fatal(null, ex);
                throw;
            }
            finally
            {
                Command.Connection.Close();
            }
            return result;
        }
    }
}
