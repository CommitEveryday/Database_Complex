using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    public class InventoryType
    {
        public static IQueryExecuter queryEx;

        public int id { get; private set; }
        public string title { get; private set; }
        public string descrition { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //При ошибке генерирует исключение
        public static InventoryType InsertAndNew(string title, string descrition)
        {
            queryEx.ExecuteNonQuery(CommandType.Text,
                @"INSERT INTO InventoryType (title,description) VALUES (?TITLE,?DESCRIPTION)",
                new Dictionary<string, object>() { { "?TITLE", title }, { "?DESCRIPTION", descrition } });
            int id = (int)queryEx.ExecuteScalar(CommandType.Text,
                @"SELECT id FROM InventoryType WHERE title=?TITLE",
                new Dictionary<string, object>() { { "?TITLE", title }});
            return new InventoryType(id, title, descrition);
        }

        public InventoryType(int id, string title, string descrition)
        {
            this.id = id;
            this.title = title;
            this.descrition = descrition;
        }

        public void Update(string title, string descrition)
        {
            queryEx.ExecuteNonQuery(CommandType.Text,
                @"UPDATE InventoryType SET title=?TITLE, description=?DESCRIPTION WHERE id = ?ID",
                new Dictionary<string, object>() { { "?TITLE", title }, { "?DESCRIPTION", descrition },
                {"?ID", id}});
            this.title = title;
            this.descrition = descrition;
        }

        public void Delete()
        {
            queryEx.ExecuteNonQuery(CommandType.Text,
                @"DELETE FROM InventoryType WHERE id = ?ID",
                new Dictionary<string, object>() {{"?ID", id}});
        }

        public override string ToString()
        {
            return this.title;
        }

    }
}
