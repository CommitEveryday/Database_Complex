using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Database_complex
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public bool sql_server;
        public string con_str = @"Server=FUN\SQLEXPRESS;Database=complex;User=sa;Password=masterkey";
        public static DbCommand cmd;
        ORMRoot root;

        private void MainForm_Load(object sender, EventArgs e)
        {
            bool good = false;
            DbConnection con = null;
            while (!good)
            {
                good = true;
                DialogResult res = new AutoForm(this).ShowDialog();
                if (res != DialogResult.OK)
                {
                    //Application.Exit();
                    this.Close();
                    return;
                }
                    
                if (sql_server)
                    con = new SqlConnection(con_str);
                else con = new MySqlConnection(con_str);
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    good = false;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            if (sql_server)
                cmd = new SqlCommand();
            else cmd = new MySqlCommand();
            cmd.Connection = con;

            root = new ORMRoot(cmd);

            initHostels();
            initTreeView();
            initGroundPage();
            initInventoryPage();
        }

        private void initGroundPage()
        {
            listBoxGround.Items.Clear();
            listBoxGround.Items.AddRange(root.Grounds.ToArray());
            if (listBoxGround.Items.Count > 0)
            {
                btGroundPageSave.Enabled = btGroundPageDel.Enabled = true;
                listBoxGround.Enabled = true;
                listBoxGround.SelectedIndex = 0;
                 
            }
            else
            {
                btGroundPageSave.Enabled = btGroundPageDel.Enabled = false;
                listBoxGround.Enabled = false;
                textBoxGround.Text = "";
                 
            }
        }

        private void initInventoryPage()
        {
            listBoxInventory.Items.Clear();
            listBoxInventory.Items.AddRange(root.Inventorys.ToArray());
            if (listBoxInventory.Items.Count > 0)
            {
                btInventoryDel.Enabled = btInventorySave.Enabled = true;
                listBoxInventory.Enabled = true;
                listBoxInventory.SelectedIndex = 0;
            }
            else
            {
                btInventoryDel.Enabled = btInventorySave.Enabled = false;
                listBoxInventory.Enabled = false;
                tbInventoryTitle.Text = tbInventoryDesc.Text = "";
            }
        }

        private void initHostels()
        {
            cbGroundPageHostel.Items.Clear();
            cbGroundPageHostel.Items.AddRange(root.Grounds.ToArray());
            cbGroundOfHostel.Items.Clear();
            cbGroundOfHostel.Items.AddRange(root.Grounds.ToArray());
            gbHostel.Enabled = false;
        }

        private void initTreeView()
        {
            setStateAllControls(gbEdu, false);
            setStateAllControls(gbClass, false);
            setStateAllControls(gbGround, false);
            gbInventoryClass.Enabled = false;
            treeViewEdu.Nodes.Clear();
            TreeNode tn = new TreeNode("Участки");
            tn.Tag = null;
            foreach (Ground ground in root.Grounds)
            {
                TreeNode node = new TreeNode(ground.ToString());
                node.Tag = ground;
                tn.Nodes.Add(node);
                foreach (EduBuilding edu in ground.EduBuilding)
                {
                    TreeNode nodeEdu = new TreeNode(edu.ToString());
                    nodeEdu.Tag = edu;
                    node.Nodes.Add(nodeEdu);
                    foreach (Classroom classroom in edu.Classroom)
                    {
                        TreeNode nodeClass = new TreeNode(classroom.ToString());
                        nodeClass.Tag = classroom;
                        nodeEdu.Nodes.Add(nodeClass);
                    }
                }

            }
            tn.Expand();
            treeViewEdu.Nodes.Add(tn);

            listViewCountEdu.Items.Clear();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT * FROM ClassInBuilding";
            try
            {
                cmd.Connection.Open();
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        listViewCountEdu.Items.Add(read.GetString(0)).SubItems.Add(read.GetInt32(1).ToString());
                    }
                }
            }
            catch (System.Data.Common.DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }

            
        }

        private static void setStateAllControls(Control ctrl, bool state)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
                ctrl.Controls[i].Enabled = state;
        }

        private void cbGroundPageHostel_SelectedIndexChanged(object sender, EventArgs e)
        { 
            prepareHostel();
        }

        private void prepareHostel()
        {
            gbHostel.Enabled = true;
            cbHostel.Items.Clear();
            cbHostel.Items.AddRange(((Ground)cbGroundPageHostel.SelectedItem).Hostel.ToArray());
            if (cbHostel.Items.Count > 0)
            {
                cbHostel.SelectedIndex = 0;
                btDelHostel.Enabled = true;
                btChangeHostel.Enabled = true;
                cbHostel.Enabled = true;
                cbHostelOfRoom.Items.Clear();
                foreach (Ground ground in root.Grounds) 
                    cbHostelOfRoom.Items.AddRange(ground.Hostel.ToArray());
                prepareRoom();
            }
            else
            {
                tbHostelName.Text = "";
                cbGroundOfHostel.SelectedIndex = -1;
                btDelHostel.Enabled = false;
                btChangeHostel.Enabled = false;
                cbHostel.Enabled = false;
                gbRoom.Enabled = false;
            }
        }

        private void prepareRoom()
        {
            gbRoom.Enabled = true;
            cbRoom.Items.Clear();
            cbRoom.Items.AddRange(((Hostel)cbHostel.SelectedItem).Room.ToArray());
            if (cbRoom.Items.Count > 0)
            {
                cbRoom.SelectedIndex = 0;
                btDelRoom.Enabled = true;
                btChangeRoom.Enabled = true;
                cbRoom.Enabled = true;
                prepareFurniture();
            }
            else
            {
                numUpDownRoomNum.Value = 0;
                rbOccupNULL.Checked = true;
                rbSeatNULL.Checked = true;
                cbHostelOfRoom.SelectedIndex = -1;
                btDelRoom.Enabled = false;
                btChangeRoom.Enabled = false;
                cbRoom.Enabled = false;
                gbFurniture.Enabled = false;
            }
        }

        private void prepareFurniture()
        {
            gbFurniture.Enabled = true;
            listBoxFurniture.Items.Clear();
            listBoxFurniture.Items.AddRange(((Room)cbRoom.SelectedItem).Furniture.ToArray());
            if (listBoxFurniture.Items.Count > 0)
            {
                listBoxFurniture.SelectedIndex = 0;
                btDelFurn.Enabled = true;
                btChangeFurn.Enabled = true;
                listBoxFurniture.Enabled = true;
            }
            else
            {
                tbFurnDesc.Text = "";
                tbFurnName.Text = "";
                btDelFurn.Enabled = false;
                btChangeFurn.Enabled = false;
                listBoxFurniture.Enabled = false;
            }
        }

        private void btDelHostel_Click(object sender, EventArgs e)
        {
            Hostel curHostel = (Hostel)cbHostel.SelectedItem;
            if (curHostel != null)
            {
                curHostel.Delete();
                prepareHostel();
            }
        }

        private void cbHostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hostel curHostel = (Hostel)((ComboBox)sender).SelectedItem;
            tbHostelName.Text = curHostel.name;
            cbGroundOfHostel.SelectedItem = curHostel.Ground;
            prepareRoom();
        }

        private void btAddHostel_Click(object sender, EventArgs e)
        {
            Ground ground = (Ground)cbGroundOfHostel.SelectedItem;
            if (null == ground)
            {
                MessageBox.Show("Нужно выбрать земельный участок!");
                return;
            }
            Hostel newHostel = Hostel.InsertAndNew(ground, tbHostelName.Text);
            if (null != newHostel)
            {
                ground.Hostel.Add(newHostel);
                prepareHostel();
            }
        }

        private void btChangeHostel_Click(object sender, EventArgs e)
        {
            Hostel hostel = (Hostel)cbHostel.SelectedItem;
            if (hostel != null)
            {
                Ground ground = (Ground)cbGroundOfHostel.SelectedItem;
                hostel.Update(ground, tbHostelName.Text);
                prepareHostel();
            }
        }

        private void btDelRoom_Click(object sender, EventArgs e)
        {
            Room cur = (Room)cbRoom.SelectedItem;
            if (cur != null)
            {
                cur.Delete();
                prepareRoom();
            }
        }

        private void btChangeRoom_Click(object sender, EventArgs e)
        {
            Room Room = (Room)cbRoom.SelectedItem;
            if (Room != null)
            {
                Hostel hostel = (Hostel)cbHostelOfRoom.SelectedItem;
                int? seat = rbSeatNULL.Checked?null:(int?)numUpDownSeat.Value;
                int? occup = rbOccupNULL.Checked?null:(int?)numUpDownOccup.Value;
                Room.Update(hostel, (int)numUpDownRoomNum.Value, seat, occup);
                prepareRoom();
            }
        }

        private void btAddRoom_Click(object sender, EventArgs e)
        {
            Hostel hostel = (Hostel)cbHostelOfRoom.SelectedItem;
            if (null == hostel)
            {
                MessageBox.Show("Нужно выбрать общежитие!");
                return;
            }
            int? seat = rbSeatNULL.Checked?null:(int?)numUpDownSeat.Value;
            int? occup = rbOccupNULL.Checked?null:(int?)numUpDownOccup.Value;
            Room newRoom = Room.InsertAndNew(hostel, (int)numUpDownRoomNum.Value, seat, occup);
            if (null != newRoom)
            {
                hostel.Room.Add(newRoom);
                prepareRoom();
            }
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Room curRoom = (Room)((ComboBox)sender).SelectedItem;
            numUpDownRoomNum.Value = curRoom.num;
            if (curRoom.seat.HasValue)
            {
                rbSeatNum.Checked = true;
                numUpDownSeat.Value = curRoom.seat.Value;
            }
            else 
            {
                rbSeatNULL.Checked = true;
            }
            if (curRoom.occup.HasValue)
            {
                rbOccupNum.Checked = true;
                numUpDownOccup.Value = curRoom.occup.Value;
            }
            else
            {
                rbOccupNULL.Checked = true;
            }
            cbHostelOfRoom.SelectedItem = curRoom.Hostel;
            prepareFurniture();
        }

        private void rbSeatNULL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSeatNULL.Checked)
            {
                numUpDownSeat.Enabled = false;
            }
            else
            {
                numUpDownSeat.Enabled = true;
            }
        }

        private void rbOccupNULL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOccupNULL.Checked)
            {
                numUpDownOccup.Enabled = false;
            }
            else
            {
                numUpDownOccup.Enabled = true;
            }
        }

        private void listBoxFurniture_SelectedIndexChanged(object sender, EventArgs e)
        {
            Furniture cur = (Furniture)((ListBox)sender).SelectedItem;
            tbFurnName.Text = cur.title;
            tbFurnDesc.Text = cur.descrition;
        }

        private void btDelFurn_Click(object sender, EventArgs e)
        {
            Furniture cur = (Furniture)listBoxFurniture.SelectedItem;
            if (cur != null)
            {
                cur.Delete();
                prepareFurniture();
            }
        }

        private void btChangeFurn_Click(object sender, EventArgs e)
        {
            Furniture furn = (Furniture)listBoxFurniture.SelectedItem;
            if (furn != null)
            {
                furn.Update(furn.Room, tbFurnName.Text, tbFurnDesc.Text);
                prepareFurniture();
            }
        }

        private void btAddFurn_Click(object sender, EventArgs e)
        {
            Room curRoom = (Room)cbRoom.SelectedItem;
            Furniture newFurn = Furniture.InsertAndNew(curRoom, tbFurnName.Text, tbFurnDesc.Text);
            if (null != newFurn)
            {
                curRoom.Furniture.Add(newFurn);
                prepareFurniture();
            }
        }

        private void treeViewEdu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            setStateAllControls(gbEdu, false);
            setStateAllControls(gbClass, false);
            setStateAllControls(gbGround, false);
            gbInventoryClass.Enabled = false;
            TreeNode node = treeViewEdu.SelectedNode;
            if (node == null)
            {
                return;
            }
            if (node.Tag==null)
            {
                setStateAllControls(gbGround, true);
                btGroundDel.Enabled = btGroundSave.Enabled = false;
                tbGround.Text = "";
            }
            Object obj = node.Tag;
            if (obj is Ground)
            {
                setStateAllControls(gbEdu, true);
                Ground ground = (Ground)obj;
                tbEduName.Text = "";
                btEduDel.Enabled = btEduSave.Enabled = false;
                setStateAllControls(gbGround, true);
                btGroundAdd.Enabled = false;
                tbGround.Text = ground.cadastre;
            }
            else if (obj is EduBuilding)
            {
                setStateAllControls(gbEdu, true);
                btEduAdd.Enabled = false;
                EduBuilding edu = (EduBuilding)obj;
                tbEduName.Text = edu.name;
                setStateAllControls(gbClass, true);
                //numUpDownClassNum.Enabled = true;
                btClassDel.Enabled = btClassSave.Enabled = false;
                numUpDownClassNum.Value = 0;
                rbClass2.Checked = true;
            }
            else if (obj is Classroom)
            {
                Classroom classroom = (Classroom)obj;
                setStateAllControls(gbClass, true);
                setStateAllControls(dbClassSize, true);
                btClassAdd.Enabled = false;
                numUpDownClassNum.Value = classroom.num;
                if (classroom.size.HasValue)
                {
                    rbClass1.Checked = true;
                    numUpDownClassSize.Value = classroom.size.Value;
                }
                else
                {
                    rbClass2.Checked = true;
                }
                gbInventoryClass.Enabled = true;
            }
        }

        private void btEduAdd_Click(object sender, EventArgs e)
        {
            EduBuilding newEdu = EduBuilding.InsertAndNew((Ground)treeViewEdu.SelectedNode.Tag, tbEduName.Text);
            if (null != newEdu)
            {
                ((Ground)treeViewEdu.SelectedNode.Tag).EduBuilding.Add(newEdu);
                initTreeView();
            }
        }

        private void btEduSave_Click(object sender, EventArgs e)
        {
            EduBuilding edu = (EduBuilding)treeViewEdu.SelectedNode.Tag;
            edu.Update((Ground)treeViewEdu.SelectedNode.Parent.Tag, tbEduName.Text);
            initTreeView();
        }

        private void btEduDel_Click(object sender, EventArgs e)
        {
            EduBuilding curEdu = (EduBuilding)treeViewEdu.SelectedNode.Tag;
            curEdu.Delete();
            initTreeView();
        }

        private void rbClass2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClass2.Checked)
            {
                numUpDownClassSize.Enabled = false;
            }
            else
            {
                numUpDownClassSize.Enabled = true;
            }
        }

        private void btClassAdd_Click(object sender, EventArgs e)
        {
            int? size = rbClass2.Checked?null:(int?)numUpDownClassSize.Value;
            Classroom newClass = Classroom.InsertAndNew((EduBuilding)treeViewEdu.SelectedNode.Tag, (int)numUpDownClassNum.Value, size);
            if (null != newClass)
            {
                ((EduBuilding)treeViewEdu.SelectedNode.Tag).Classroom.Add(newClass);
                initTreeView();
            }
        }

        private void btClassDel_Click(object sender, EventArgs e)
        {
            Classroom curClass = (Classroom)treeViewEdu.SelectedNode.Tag;
            curClass.Delete();
            initTreeView();
        }

        private void btClassSave_Click(object sender, EventArgs e)
        {
            Classroom edu = (Classroom)treeViewEdu.SelectedNode.Tag;
            int? size = rbClass2.Checked?null:(int?)numUpDownClassSize.Value;
            edu.Update((EduBuilding)treeViewEdu.SelectedNode.Parent.Tag, (int)numUpDownClassNum.Value, size);
            initTreeView();
        }

        private void btGroundAdd_Click(object sender, EventArgs e)
        {
            Ground newGround = Ground.InsertAndNew(tbGround.Text);
            if (null != newGround)
            {
                root.Grounds.Add(newGround);
                initTreeView();
                initHostels();
                initGroundPage();
            }
        }

        private void btGroundDel_Click(object sender, EventArgs e)
        {
            Ground curEdu = (Ground)treeViewEdu.SelectedNode.Tag;
            if (curEdu.Delete())
                root.Grounds.Remove(curEdu);
            initTreeView();
            initHostels();
            initGroundPage();
        }

        private void btGroundSave_Click(object sender, EventArgs e)
        {
            Ground edu = (Ground)treeViewEdu.SelectedNode.Tag;
            edu.Update(tbGround.Text);
            initTreeView();
            initHostels();
            initGroundPage();
        }

        private void btGroundPageAdd_Click(object sender, EventArgs e)
        {
            Ground newGround = Ground.InsertAndNew(textBoxGround.Text);
            if (null != newGround)
            {
                root.Grounds.Add(newGround);
                initTreeView();
                initHostels();
                initGroundPage();
            }
        }

        private void btGroundPageSave_Click(object sender, EventArgs e)
        {
            Ground edu = (Ground)listBoxGround.SelectedItem;
            edu.Update(textBoxGround.Text);
            initTreeView();
            initHostels();
            initGroundPage();
        }

        private void btGroundPageDel_Click(object sender, EventArgs e)
        {
            Ground curEdu = (Ground)listBoxGround.SelectedItem;
            if (curEdu.Delete())
                root.Grounds.Remove(curEdu);
            initTreeView();
            initHostels();
            initGroundPage();
        }

        private void listBoxGround_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxGround.Text = ((Ground)listBoxGround.SelectedItem).cadastre;
        }

        private void listBoxInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryType inv = (InventoryType)listBoxInventory.SelectedItem;
            tbInventoryTitle.Text = inv.title;
            tbInventoryDesc.Text = inv.descrition;
        }

        private void btInventoryAdd_Click(object sender, EventArgs e)
        {
            InventoryType inv = InventoryType.InsertAndNew(tbInventoryTitle.Text, tbInventoryDesc.Text);
            if (null != inv)
            {
                root.Inventorys.Add(inv);
                initInventoryPage();
            }
        }

        private void btInventorySave_Click(object sender, EventArgs e)
        {
            InventoryType inv = (InventoryType)listBoxInventory.SelectedItem;
            inv.Update(tbInventoryTitle.Text, tbInventoryDesc.Text);
            initInventoryPage();
        }

        private void btInventoryDel_Click(object sender, EventArgs e)
        {
           InventoryType inv = (InventoryType)listBoxInventory.SelectedItem;
           if (inv.Delete())
               root.Inventorys.Remove(inv);
           initInventoryPage();
        }

        private void gbInventoryClass_EnabledChanged(object sender, EventArgs e)
        {
            if (((GroupBox)sender).Enabled)
            {
                lbInventoryClass.Items.Clear();
                lbInventoryClass.Items.AddRange(Classroom_Inventory.getInvByClass(root.Class_has_inventory,
                    (Classroom)treeViewEdu.SelectedNode.Tag).ToArray());
                if (lbInventoryClass.Items.Count > 0)
                {
                    btInventoryClassDel.Enabled = true;
                    lbInventoryClass.SelectedIndex = 0;
                }
                else
                {
                    btInventoryClassDel.Enabled = false;
                }
            }
        }

        private void btInventoryClassDel_Click(object sender, EventArgs e)
        {
            InventoryType inv = (InventoryType)lbInventoryClass.SelectedItem;
            Classroom classroom = (Classroom)treeViewEdu.SelectedNode.Tag;
            Classroom_Inventory.Delete(root.Class_has_inventory, classroom, inv);
            gbInventoryClass.Enabled = false;
            gbInventoryClass.Enabled = true;
        }

        private void btInventoryClassAdd_Click(object sender, EventArgs e)
        {
            AddInventoryInClass addForm = new AddInventoryInClass();
            List<InventoryType> haveInv = Classroom_Inventory.getInvByClass(
                root.Class_has_inventory, (Classroom)treeViewEdu.SelectedNode.Tag);
            List<InventoryType> notHaveInv = new List<InventoryType>();
            foreach (InventoryType inv in root.Inventorys)
                if (!haveInv.Contains(inv))
                    notHaveInv.Add(inv);
            if (notHaveInv.Count > 0)
            {
                addForm.listBoxInv.Items.AddRange(notHaveInv.ToArray());
                addForm.listBoxInv.SelectedIndex = 0;
            }
            else
                addForm.btOK.Enabled = false;
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                InventoryType newInv = (InventoryType)addForm.listBoxInv.SelectedItem;
                Classroom_Inventory newRel = 
                    Classroom_Inventory.InsertAndNew((Classroom)treeViewEdu.SelectedNode.Tag, newInv);
                if (newRel != null)
                {
                    root.Class_has_inventory.Add(newRel);
                    gbInventoryClass.Enabled = false;
                    gbInventoryClass.Enabled = true;
                }
            }
        }
    }
}
