using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class EduBuilding
    {
        public static DbCommand cmd;

        private static string UpdateStr =
            @"UPDATE EduBuilding SET Ground_id={0}, name='{1}' WHERE id = {2}";
        private static string DeleteStr =
            @"DELETE FROM EduBuilding WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO EduBuilding (Ground_id,name) VALUES ({0},'{1}')";
        private static string GetId =
            @"SELECT id FROM EduBuilding WHERE name='{0}'";
        private static string InitList =
            @"SELECT id,num,size FROM Classroom WHERE EduBuilding_id={0}";

        public int id { get; private set; }
        public Ground Ground { get; private set; }
        public string name { get; private set; }
        public List<Classroom> Classroom { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static EduBuilding InsertAndNew(Ground Ground, string name)
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
            return new EduBuilding(id, Ground, name);
        }

        public EduBuilding(int id, Ground Ground, string name)
        {
            this.id = id;
            this.Ground = Ground;
            this.name = name;
            this.Classroom = new List<Classroom>();
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
                    this.Ground.EduBuilding.Remove(this);
                    this.Ground = Ground;
                    this.Ground.EduBuilding.Add(this);
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
                this.Ground.EduBuilding.Remove(this);
            return result;
        }

        public void InitClassroom()
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
                        int? size = read.IsDBNull(2) ? null : (int?)read.GetInt32(2);
                        this.Classroom.Add(new Classroom((int)read["id"], this, (int)read["num"], size));
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
            return this.name;
        }
    }
}
