using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class Room
    {
        public static DbCommand cmd;

        private static string UpdateStr = 
            @"UPDATE Room SET Hostel_id={0}, num={1}, seat={2}, occup={3} WHERE id = {4}";
        private static string DeleteStr =
            @"DELETE FROM Room WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO Room (Hostel_id,num,seat,occup) VALUES ({0},{1},{2},{3})";
        private static string GetId =
            @"SELECT id FROM Room WHERE Hostel_id={0} AND num={1}";
        private static string InitList =
            @"SELECT id,title,description FROM Furniture WHERE Room_id={0}";

        public int id { get; private set; }
        public Hostel Hostel { get; private set; }
        public int num { get; private set; }
        public int? seat { get; private set; }
        public int? occup { get; private set; }
        public List<Furniture> Furniture { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Room InsertAndNew(Hostel Hostel, int num, int? seat, int? occup)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, Hostel.id, num,
                seat.HasValue ? seat.ToString() : "NULL", occup.HasValue ? occup.ToString() : "NULL");
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(GetId, Hostel.id, num);
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
            return new Room(id, Hostel, num, seat, occup);
        }

        public Room(int id, Hostel Hostel, int num, int? seat, int? occup)
        {
            this.id = id;
            this.Hostel = Hostel;
            this.num = num;
            this.seat = seat;
            this.occup = occup;
            this.Furniture = new List<Furniture>();
        }

        public bool Update(Hostel Hostel, int num, int? seat, int? occup)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, Hostel.id, num,
                seat.HasValue ? seat.ToString() : "NULL", occup.HasValue ? occup.ToString() : "NULL", id);
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
                if (this.Hostel != Hostel)
                {
                    this.Hostel.Room.Remove(this);
                    this.Hostel = Hostel;
                    this.Hostel.Room.Add(this);
                }
                this.num = num;
                this.seat = seat;
                this.occup = occup;
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
                this.Hostel.Room.Remove(this);
            return result;
        }

        public void InitFurniture()
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
                        string desc = (string)(read.IsDBNull(2)?"":read["description"]);
                        this.Furniture.Add(new Furniture((int)read["id"],this,(string)read["title"],desc));
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
        }

        public override string ToString()
        {
            return this.num.ToString();
        }
    }
}
