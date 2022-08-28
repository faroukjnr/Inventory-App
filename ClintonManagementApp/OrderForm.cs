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

    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\USER\Documents\clinton dbms.mdf;Integrated Security = True; Connect Timeout = 30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrderForm()
        {
            InitializeComponent();
            loadOrder();
        }
        public void loadOrder()
        {
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbOrder", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/mm/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());


            }
            dr.Close();
            con.Close();
        }


        private void customerbtn_Click(object sender, EventArgs e)
        {
            OrderModuleForm ModuleForm = new OrderModuleForm();
            ModuleForm.btnInsert.Enabled = true;
           
            ModuleForm.ShowDialog();
            loadOrder();

        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                OrderModuleForm moduleForm = new OrderModuleForm();
                moduleForm.lblOid.Text = dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString();
                moduleForm.dtorder.Text = dgvOrder.Rows[e.RowIndex].Cells[2].Value.ToString();
                moduleForm.txtPid.Text = dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString();
                moduleForm.txtCid.Text = dgvOrder.Rows[e.RowIndex].Cells[4].Value.ToString();
                moduleForm.UDqty.Value = Convert.ToInt32(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString());
                moduleForm.txtPrice.Text = dgvOrder.Rows[e.RowIndex].Cells[6].Value.ToString();

               moduleForm.btnInsert.Enabled = false;
           
                moduleForm.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE orderid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted");
                }

            }
            loadOrder();
        }
    }
}
