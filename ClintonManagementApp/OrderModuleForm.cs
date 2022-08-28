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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClintonManagementApp
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\USER\Documents\clinton dbms.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
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
            cm = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT(Cd,Cname) LIKE '%" + txtSearchCust.Text + "%'", con);
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
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid,pname,pprice,pdescription,pcategory) LIKE '%" + txtSearchProd.Text + "%'", con);
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
            GetQty();
            if (Convert.ToInt16(UDqty.Value) > qty)
            {
                MessageBox.Show("Instock Quantity is not enough", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UDqty.Value = UDqty.Value - 1;
                return;
            }
            if (Convert.ToInt16(UDqty.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(UDqty.Value);
                txtTotal.Text = total.ToString();
            }
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
          //  qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());

        }

        private void dgvCustomer_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCid.Text == "")
                {
                    MessageBox.Show("Please Select Customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Please Select Product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to Insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    cm = new SqlCommand("INSERT INTO tbOrder(odate,pid,cd,qty,price,total)VALUES(@odate,@pid,@cd,@qty,@price,@total)", con);
                cm.Parameters.AddWithValue("@odate", dtorder.Text);
                cm.Parameters.AddWithValue("@pid", Convert.ToInt16(txtPid.Text));
                cm.Parameters.AddWithValue("@cd", Convert.ToInt16(txtCid.Text));
                cm.Parameters.AddWithValue("@qty", Convert.ToInt16(UDqty.Value));
                cm.Parameters.AddWithValue("@price", Convert.ToInt16(txtPrice.Text));
                cm.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Order has been inserted successfully");

                cm = new SqlCommand("UPDATE tbProduct SET pqty * ( pqty=@pqty)  WHERE pid LIKE '" + txtPid.Text + "'", con);

                cm.Parameters.AddWithValue("@pqty", UDqty.Text);

                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                Clear();
                loadProduct();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCid.Clear();
            txtCName.Clear();

            txtPid.Clear();
            txtPName.Clear();

            txtPrice.Clear();
            UDqty.Value = 1;
            txtTotal.Clear();
            dtorder.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
          
            
        }
        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid = '" + txtPid.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                qty =Convert.ToInt32( dr[0].ToString());


            }
            dr.Close();
            con.Close();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
