using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class Furniture
    {
        public static DbCommand cmd;

        private static string UpdateStr = 
            @"UPDATE Furniture SET Room_id={0}, title='{1}', description='{2}' WHERE id = {3}";
        private static string DeleteStr =
            @"DELETE FROM Furniture WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO Furniture (Room_id,title,description) VALUES ({0},'{1}','{2}')";
        private static string GetId =
            @"SELECT MAX(id) FROM Furniture";

        public int id { get; private set; }
        public Room Room { get; private set; }
        public string title { get; private set; }
        public string descrition { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Furniture InsertAndNew(Room Room, string title, string descrition)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, Room.id, title, descrition);
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = GetId;
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
            return new Furniture(id, Room, title, descrition);
        }

        public Furniture(int id, Room Room, string title, string descrition)
        {
            this.id = id;
            this.Room = Room;
            this.title = title;
            this.descrition = descrition;
        }

        public bool Update(Room Room, string title, string descrition)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, Room.id, title, descrition, id);
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
                if (this.Room != Room)
                {
                    this.Room.Furniture.Remove(this);
                    this.Room = Room;
                    this.Room.Furniture.Add(this);
                }
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
            if (true == result)
                this.Room.Furniture.Remove(this);
            return result;
        }

        public override string ToString()
        {
            return this.title;
        }
    }
}
