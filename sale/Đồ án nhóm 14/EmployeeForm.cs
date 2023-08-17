using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_nhóm_14
{
    public partial class EmployeeForm : Form
    {

        public EmployeeForm()
        {
            InitializeComponent();
           
            foreach (DataGridViewRow row in this.guna2DataGridView1.Rows)
            {
            
                this.guna2DataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.guna2DataGridView1_CellPainting);
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connString);
            try
            {

                connection.Open();
                string sql = string.Format("select ProductID,CategoryID,ProductName,Description,Quantity,UnitPrice,Anh from Product where CategoryID =1");
                SqlDataAdapter apt = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                apt.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                guna2DataGridView1.Columns["CategoryID"].Visible = false;
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

        private void guna2DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy kích thước của ô hiện tại
                Rectangle rect = e.CellBounds;

                // Tính toán tọa độ của hình tròn
                int x = rect.X + rect.Width / 2 - rect.Height / 2;
                int y = rect.Y + 5;
                int diameter = rect.Height - 10;

                // Vẽ hình tròn bằng SolidBrush
                SolidBrush brush = new SolidBrush(Color.Gray);

                // Làm mịn đường vẽ
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Thiết lập PaintParts để chỉ vẽ hình tròn
                

                e.Graphics.FillEllipse(brush, x, y, diameter, diameter);

                e.Handled = true; // Ngăn không cho DataGridView vẽ lại ô
            }
        }
    }
}
