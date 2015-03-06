using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class InventoryType
    {
        public static DbCommand cmd;

        private static string UpdateStr =
            @"UPDATE InventoryType SET title='{0}', description='{1}' WHERE id = {2}";
        private static string DeleteStr =
            @"DELETE FROM InventoryType WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO InventoryType (title,description) VALUES ('{0}','{1}')";
        private static string GetId =
            @"SELECT id FROM InventoryType WHERE title='{0}'";

        public int id { get; private set; }
        public string title { get; private set; }
        public string descrition { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static InventoryType InsertAndNew(string title, string descrition)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, title, descrition);
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(GetId,title);
                id = (int)cmd.ExecuteScalar();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            finally
            {
                cmd.Connection.Close();
            }
            if (false == result) return null;
            return new InventoryType(id, title, descrition);
        }

        public InventoryType(int id, string title, string descrition)
        {
            this.id = id;
            this.title = title;
            this.descrition = descrition;
        }

        public bool Update(string title, string descrition)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, title, descrition, id);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            finally
            {
                cmd.Connection.Close();
            }
            if (true == result)
            {
                this.title = title;
                this.descrition = descrition;
            }
            return result;
        }

        public bool Delete()
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(DeleteStr, id);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }

        public override string ToString()
        {
            return this.title;
        }

        public static InventoryType getById(List<InventoryType> lst, int id)
        {
            foreach (InventoryType elem in lst)
                if (elem.id == id) return elem;
            return null;
        }
    }
}
