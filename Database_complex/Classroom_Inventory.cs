using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;

namespace Database_complex
{
    class Classroom_Inventory
    {
        public static DbCommand cmd;

        private static string DeleteStr =
            @"DELETE FROM Classroom_has_Inventory WHERE Classroom_id={0} AND Inventory_id={1}";
        private static string InsertStr =
            @"INSERT INTO Classroom_has_Inventory (Classroom_id,Inventory_id) VALUES ({0},{1})";
        private static string GetInventoryByClass =
            @"SELECT Inventory_id FROM Classroom_has_Inventory WHERE Classroom_id={0}";

        public Classroom Classroom { get; private set; }
        public InventoryType InvetrotyType { get; private set; }

        //Вставляет новую запись в БД и возвращает её объект
        //return null - ошибка
        public static Classroom_Inventory InsertAndNew(Classroom Classroom, InventoryType InvetrotyType)
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(InsertStr, Classroom.id, InvetrotyType.id);
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
            if (false == result) return null;
            return new Classroom_Inventory(Classroom, InvetrotyType);
        }

        public Classroom_Inventory(Classroom Classroom, InventoryType InvetrotyType)
        {
            this.Classroom = Classroom;
            this.InvetrotyType = InvetrotyType;
        }

        public bool Delete()
        {
            bool result = true;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(DeleteStr, Classroom.id, InvetrotyType.id);
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

        public static bool Delete(List<Classroom_Inventory> lst,
            Classroom Classroom, InventoryType InvetrotyType)
        {
            foreach (Classroom_Inventory cl_inv in lst)
                if (cl_inv.Classroom.Equals(Classroom) &&
                    cl_inv.InvetrotyType.Equals(InvetrotyType))
                    if (cl_inv.Delete())
                    {
                        lst.Remove(cl_inv);
                        return true;
                    }
            return false;
        }

        public static void addInventoryByClass(List<Classroom_Inventory> lst, Classroom classroom, 
            List<InventoryType> invents)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format(GetInventoryByClass, classroom.id);
            try
            {
                cmd.Connection.Open();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        int inv_id = read.GetInt32(0);
                        lst.Add(new Classroom_Inventory(classroom, InventoryType.getById(invents, inv_id)));
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

        public static List<InventoryType> getInvByClass(List<Classroom_Inventory> lst,
            Classroom classroom)
        {
            List<InventoryType> res = new List<InventoryType>();
            foreach (Classroom_Inventory cl_inv in lst)
                if (cl_inv.Classroom.Equals(classroom))
                    res.Add(cl_inv.InvetrotyType);
            return res;
        }


    }
}
