using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class Hostel
    {
        public static DbCommand cmd;

        private static string UpdateStr = 
            @"UPDATE Hostel SET Ground_id={0}, name='{1}' WHERE id = {2}";
        private static string DeleteStr =
            @"DELETE FROM Hostel WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO Hostel (Ground_id,name) VALUES ({0},'{1}')";
        private static string GetId =
            @"SELECT id FROM Hostel WHERE name='{0}'";
        private static string InitList =
            @"SELECT id,num,seat,occup FROM Room WHERE Hostel_id={0}";

        public int id { get; private set; }
        public Ground Ground { get; private set; }
        public string name { get; private set; }
        public List<Room> Room { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Hostel InsertAndNew(Ground Ground, string name)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, Ground.id, name);
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(GetId, name);
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
            return new Hostel(id, Ground, name);
        }

        public Hostel(int id, Ground Ground, string name)
        {
            this.id = id;
            this.Ground = Ground;
            this.name = name;
            this.Room = new List<Room>();
        }

        public bool Update(Ground Ground, string name)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, Ground.id, name, id);
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
                if (this.Ground != Ground)
                {
                    this.Ground.Hostel.Remove(this);
                    this.Ground = Ground;
                    this.Ground.Hostel.Add(this);
                }
                this.name = name;
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
                this.Ground.Hostel.Remove(this);
            return result;
        }

        public void InitRoom()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InitList, id);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        int? seat = read.IsDBNull(2) ? null : (int?)read.GetInt32(2);
                        int? occup = read.IsDBNull(3) ? null : (int?)read.GetInt32(3);
                        this.Room.Add(new Room((int)read["id"], this, (int)read["num"], seat,occup));
                    }
                }
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            foreach (Room room in this.Room)
                room.InitFurniture();
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
