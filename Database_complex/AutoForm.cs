using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Database_complex
{
    public partial class AutoForm : Form
    {

        MainForm form;
        public AutoForm(MainForm form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.sql_server = rbSqlServer.Checked;
            if (radioButtonConnString.Checked)
            {
                form.con_str = textBox1.Text;
            }
            else
            {
                if (rbSqlServer.Checked)
                {
                    form.con_str = String.Format(
@"Server={0};Database={1};User={2};Password={3}",
                        tbServer.Text,tbDatabase.Text,tbUser.Text,tbPassword.Text);
                }
                else
                {
                    form.con_str = String.Format(
@"Server={0};Database={1};UID={2};Pwd={3}",
                        tbServer.Text, tbDatabase.Text, tbUser.Text, tbPassword.Text);
                }
            }
            
        }
    }
}
