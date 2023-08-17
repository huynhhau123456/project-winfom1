using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Đồ_án_nhóm_14
{
    public partial class Danhmucsanpham : Form
    {
        public Danhmucsanpham()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

   

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            quanaonam q = new quanaonam();
             if(Login.result == "Customer")
            {
          
                q.label10.Visible=false;
                q.guna2TextBox8.Visible = false;
               q.ShowDialog();
                this.Show();
            }
            else if (Login.result == "Employee")
            {
                q.label1.Visible = false;
                q.guna2TextBox1.Visible = false;
                q.guna2TextBox6.Visible = false;
                q.label5.Visible = false;
                q.label5.Visible = false;
                q.guna2TextBox7.Visible = false;
                q.label9.Visible=false;
                q.ShowDialog();

            }



        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            quanaonu q = new quanaonu();
            
            q.ShowDialog();
        }

        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            dienthoai dt = new dienthoai();
            dt.ShowDialog();
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {
            guna2CirclePictureBox1.Tag = "xx";
        }

        private void guna2ComboBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
