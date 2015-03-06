using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace Database_complex
{
    class Classroom
    {
        public static DbCommand cmd;

        private static string UpdateStr =
            @"UPDATE Classroom SET EduBuilding_id={0}, num={1}, size={2} WHERE id = {3}";
        private static string DeleteStr =
            @"DELETE FROM Classroom WHERE id = {0}";
        private static string InsertStr =
            @"INSERT INTO Classroom (EduBuilding_id,num,size) VALUES ({0},{1},{2})";
        private static string GetId =
            @"SELECT id FROM Classroom WHERE EduBuilding_id={0} AND num={1}";

        public int id { get; private set; }
        public EduBuilding EduBuilding { get; private set; }
        public int num { get; private set; }
        public int? size { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Classroom InsertAndNew(EduBuilding EduBuilding, int num, int? size)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, EduBuilding.id, num,
                size.HasValue ? size.ToString() : "NULL");
            int id = 0;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = String.Format(GetId, EduBuilding.id, num);
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
            return new Classroom(id, EduBuilding, num, size);
        }

        public Classroom(int id, EduBuilding EduBuilding, int num, int? size)
        {
            this.id = id;
            this.EduBuilding = EduBuilding;
            this.num = num;
            this.size = size;
        }

        public bool Update(EduBuilding EduBuilding, int num, int? size)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(UpdateStr, EduBuilding.id, num,
                size.HasValue ? size.ToString() : "NULL", id);
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
                if (this.EduBuilding != EduBuilding)
                {
                    this.EduBuilding.Classroom.Remove(this);
                    this.EduBuilding = EduBuilding;
                    this.EduBuilding.Classroom.Add(this);
                }
                this.num = num;
                this.size = size;
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
                this.EduBuilding.Classroom.Remove(this);
            return result;
        }

        public override string ToString()
        {
            return this.num.ToString();
        }
    }
}
