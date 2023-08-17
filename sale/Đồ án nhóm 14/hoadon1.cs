using Guna.UI2.WinForms;
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

namespace Đồ_án_nhóm_14
{
    public partial class hoadon1 : Form
    {
        public hoadon1()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void hoadon1_Load(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connString);
            try
            {

                connection.Open();
                string sql = string.Format("select * from Orders where BranchID =1");
                SqlDataAdapter apt = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                apt.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
