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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void customerbtn_Click(object sender, EventArgs e)
        {
            OrderModuleForm ModuleForm = new OrderModuleForm();
            ModuleForm.ShowDialog();
            
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
