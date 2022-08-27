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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\USER\Documents\clinton dbms.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrderModuleForm()
        {
            InitializeComponent();
            loadCustomer();
            loadProduct();
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }
        public void loadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT(Cd,Cname) LIKE '%"+txtSearchCust.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());


            }
            dr.Close();
            con.Close();

        }
        public void loadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid,pname,pprice,pdescription,pcategory) LIKE '%"+txtSearchProd.Text+"%'", con);
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

       
        int qty = 0;
        

        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt16(numericUpDown1.Value)>qty)
            {
                MessageBox.Show("Instock Quantity is not enough","warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(numericUpDown1.Value);
            txtTotal.Text = total.ToString();
        }

        private void OrderModuleForm_Load(object sender, EventArgs e)
        {
            loadProduct();
            loadCustomer();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());

        }

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
