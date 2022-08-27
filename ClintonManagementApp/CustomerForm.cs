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

namespace ClintonManagementApp
{ 

    public partial class CustomerForm : Form
{

    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\USER\Documents\clinton dbms.mdf"";Integrated Security=True;Connect Timeout=30");
    SqlCommand cm = new SqlCommand();
    SqlDataReader dr;
    public CustomerForm()
    {
        InitializeComponent();
            loadCustomer();
    }
    public void loadCustomer()
    {
        int i = 0;
        dgvCustomer.Rows.Clear();
        cm = new SqlCommand("SELECT * FROM tbCustomer", con);
        con.Open();
        dr = cm.ExecuteReader();
        while (dr.Read())
        {
            i++;
            dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());


        }
        dr.Close();
        con.Close();

    }

    private void customerbtn_Click(object sender, EventArgs e)
    {
        CustomerModuleForm moduleForm = new CustomerModuleForm();
        moduleForm.btnSave.Enabled = true;
        moduleForm.btnUpdate.Enabled = false;
        moduleForm.ShowDialog();
        loadCustomer();
    }

    private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
     string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
        if (colName == "edit")
        {
            CustomerModuleForm CustomerModule = new CustomerModuleForm();
            CustomerModule.label4.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            CustomerModule.txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            CustomerModule.txtCphone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();

            CustomerModule.btnSave.Enabled = false;
            CustomerModule.btnUpdate.Enabled = true;
            CustomerModule.ShowDialog();
        }
        else if (colName == "delete")
        {
            if (MessageBox.Show("Are you sure u want to delete this Customer", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cm = new SqlCommand("DELETE FROM tbCustomer WHERE Cd LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record has been successfully deleted");
            }

        }
        loadCustomer();
    } } }

