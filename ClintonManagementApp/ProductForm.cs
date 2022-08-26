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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\USER\Documents\clinton dbms.mdf;Integrated Security = True; Connect Timeout = 30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            loadProduct();
        }
        public void loadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());


            }
            dr.Close();
            con.Close();

        }

        private void productbtn_Click(object sender, EventArgs e)
        {
            ProductModule moduleForm = new ProductModule();

            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            loadProduct();

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                ProductModule ProductModule = new ProductModule();
                ProductModule.lblId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                ProductModule.txtPro.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                ProductModule.txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                ProductModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                ProductModule.txtDes.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                ProductModule.comboBox1.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();


                ProductModule.btnSave.Enabled = false;
                ProductModule.btnUpdate.Enabled = true;
                ProductModule.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }

            }
            loadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }
    }
}
