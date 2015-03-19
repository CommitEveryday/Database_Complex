using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;

namespace Database_complex
{
    class ORMRoot
    {
        public static DbCommand cmd;

        private static string InitList =
            @"SELECT id,cadastre FROM Ground";
        private static string StrInventoryType =
            @"SELECT id,title,description FROM InventoryType";

        public List<Ground> Grounds { get; private set; }
        public List<InventoryType> Inventorys { get; private set; }
        public List<Classroom_Inventory> Class_has_inventory { get; private set; }

        public ORMRoot(DbCommand cmd)
        {
            ORMRoot.cmd = cmd;
            Ground.cmd = cmd;
            Hostel.cmd = cmd;
            Room.cmd = cmd;
            Furniture.cmd = cmd;
            EduBuilding.cmd = cmd;
            Classroom.cmd = cmd;
            InventoryType.queryEx = new QueryExecuter(cmd);
            Classroom_Inventory.cmd = cmd;
            Grounds = new List<Ground>();
            Inventorys = new List<InventoryType>();
            Class_has_inventory = new List<Classroom_Inventory>();

            InitGrounds();
            InitInventoryType();
            InitClassHasInventory();
        }

        private void InitClassHasInventory()
        {
            foreach (Ground ground in Grounds)
                foreach (EduBuilding edu in ground.EduBuilding)
                    foreach (Classroom clsr in edu.Classroom)
                        Classroom_Inventory.addInventoryByClass(Class_has_inventory, clsr, Inventorys);
        }

        private void InitGrounds()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = InitList;
            try
            {
                cmd.Connection.Open();
                //cmd.ExecuteNonQuery();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        this.Grounds.Add(new Ground((int)read["id"], (string)read["cadastre"]));
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
            foreach (Ground ground in this.Grounds)
            {
                ground.InitHostel();
                ground.InitEduBuilding();
            }
        }

        private void InitInventoryType()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = StrInventoryType;
            try
            {
                cmd.Connection.Open();
                //cmd.ExecuteNonQuery();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        string desc = (string)(read.IsDBNull(2) ? "" : read["description"]);
                        this.Inventorys.Add(new InventoryType((int)read["id"], (string)read["title"], desc));
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
    }
}
