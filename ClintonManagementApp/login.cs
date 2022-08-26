using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClintonManagementApp
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false)
                Paswd.UseSystemPasswordChar =true;
            else
                Paswd.UseSystemPasswordChar =false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Uname.Clear();
            Paswd.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
