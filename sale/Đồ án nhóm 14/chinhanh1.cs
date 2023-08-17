using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_nhóm_14
{
    public partial class chinhanh1 : Form
    {
       
        public chinhanh1()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            hoadon1 hd1 = new hoadon1();
            hd1.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Danhmucsanpham n = new Danhmucsanpham();
            n.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }
    }
}
