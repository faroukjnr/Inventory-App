using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClintonManagementApp
{
    public partial class ProductModule : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\USER\Documents\clinton dbms.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModule()
        {
            InitializeComponent();
            loadProduct();
        }
        public void loadProduct()
        {
            comboBox1.Items.Clear();
            cm = new SqlCommand("SELECT pname FROM tbProduct", con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(0));
            }
            dr.Close();
            con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                if (MessageBox.Show("Are you sure you want to save this Product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    cm = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                cm.Parameters.AddWithValue("@pname", txtPro.Text);
                cm.Parameters.AddWithValue("@pqty",Convert.ToInt16( txtQty.Text));
                cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPrice.Text));
                cm.Parameters.AddWithValue("@Pdescription", txtDes.Text);
                cm.Parameters.AddWithValue("@Pcategory", comboBox1.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product has been saved successfully");
                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtPro.Clear();
            txtQty.Clear();
            txtPrice.Clear();
            txtDes.Clear();
            comboBox1.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
               
                if (MessageBox.Show("Are you sure youu want to update this Product?", "update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    cm = new SqlCommand("UPDATE tbProduct SET  pname=@pname, pqty=@pqty,pprice=@pprice, pdescription=@pdescription,pcategory=@pcategory WHERE pname LIKE '" + txtPro.Text + "'", con);
                cm.Parameters.AddWithValue("@pname", txtPro.Text);
                cm.Parameters.AddWithValue("@pqty", txtQty.Text);
                cm.Parameters.AddWithValue("@pprice", txtPrice.Text);
                cm.Parameters.AddWithValue("@pdescription", txtDes.Text);
                cm.Parameters.AddWithValue("@pcategory", comboBox1.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product has been updated  successfully");
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductModule_Load(object sender, EventArgs e)
        {

        }
    }
}
