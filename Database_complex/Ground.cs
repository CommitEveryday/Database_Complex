using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;

namespace Database_complex
{
    class Ground
    {
        public static DbCommand cmd;

        private static string UpdateStr = 
            @"UPDATE Ground SET cadastre='{0}' WHERE id = {1}";
        private static string DeleteStr =
            @"DELETE FROM Ground WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO Ground (cadastre) VALUES ('{0}')";
        private static string GetId =
            @"SELECT id FROM Ground WHERE cadastre='{0}'";
        private static string InitList =
            @"SELECT id,name FROM Hostel WHERE Ground_id={0}";
        private static string InitList2 =
            @"SELECT id,name FROM EduBuilding WHERE Ground_id={0}";

        public int id { get; private set; }
        public string cadastre { get; private set; }
        public List<Hostel> Hostel { get; private set; }
        public List<EduBuilding> EduBuilding { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Ground InsertAndNew(string cadastre)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, cadastre);
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(GetId, cadastre);
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
            return new Ground(id, cadastre);
        }

        public Ground(int id, string cadastre)
        {
            this.id = id;
            this.cadastre = cadastre;
            this.Hostel = new List<Hostel>();
            this.EduBuilding = new List<EduBuilding>();
        }

        public bool Update(string cadastre)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, cadastre, id);
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
                this.cadastre = cadastre;
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

        public void InitHostel()
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
                        string name = read.IsDBNull(1) ? "" : read.GetString(1);
                        this.Hostel.Add(new Hostel((int)read["id"], this, name));
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
            foreach (Hostel hostel in this.Hostel)
                hostel.InitRoom();
        }

        public void InitEduBuilding()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InitList2, id);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        string name = read.IsDBNull(1) ? "" : read.GetString(1);
                        this.EduBuilding.Add(new EduBuilding((int)read["id"], this, name));
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
            foreach (EduBuilding edu in this.EduBuilding)
                edu.InitClassroom();
        }

        public override string ToString()
        {
            return this.cadastre;
        }
    }
}
