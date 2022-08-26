using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace ClintonManagementApp
{
    public partial class CategoryModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\USER\Documents\clinton dbms.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();

        public CategoryModule()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Category?", "update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    cm = new SqlCommand("UPDATE tbCategory SET  Catname=@Catname WHERE CatId LIKE '" + lblCatId.Text + "'", con);
                cm.Parameters.AddWithValue("@Catname", txtCatName.Text);

                
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Category has been updated  successfully");
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Are you sure you want to save this Category?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    cm = new SqlCommand("INSERT INTO tbCategory(Catname)VALUES(@CatName)", con);
                cm.Parameters.AddWithValue("@Catname", txtCatName.Text);

                
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Category has been saved successfully");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCatName.Clear();
        }

        private void CategoryModule_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
