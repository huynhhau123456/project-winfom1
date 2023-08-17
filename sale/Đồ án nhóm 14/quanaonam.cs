using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Đồ_án_nhóm_14
{
    public partial class quanaonam : Form
    {
        public quanaonam()
        {
            InitializeComponent();

            guna2ComboBox1.Items.Add(1);
            guna2ComboBox1.Items.Add(2);
            guna2ComboBox1.Items.Add(3);
        }

        private void quanao_Load(object sender, EventArgs e)
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
                guna2DataGridView2.DataSource = dt;
                guna2DataGridView2.Columns["CategoryID"].Visible = false;
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string commandText = "INSERT INTO Product(CategoryID,ProductName, Description, Quantity, UnitPrice, BranchID, Anh) VALUES (@CategoryID,@ProductName, @Description, @Quantity, @UnitPrice, @BranchID, @Image)";
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = int.Parse(guna2TextBox8.Text);
                command.Parameters.Add("@ProductName", SqlDbType.VarChar, 50).Value = guna2TextBox2.Text;
                command.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = guna2TextBox3.Text;
                command.Parameters.Add("@Quantity", SqlDbType.SmallInt).Value = Int16.Parse(guna2TextBox4.Text);
                command.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = decimal.Parse(guna2TextBox5.Text);

                command.Parameters.Add("@BranchID", SqlDbType.Int).Value = int.Parse(guna2ComboBox1.Text);

                // Kiểm tra file ảnh đã chọn
                try
                {
                    // Kiểm tra file ảnh đã chọn
                    if (pictureBox.Image == null)
                    {
                        MessageBox.Show("Bạn chưa chọn ảnh");
                        return;
                    }
                    Image originalImage = pictureBox.Image;
                    Image copiedImage = new Bitmap(originalImage);
                    pictureBox.Image = copiedImage;
                    byte[] image = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        image = ms.ToArray();
                    }
                    command.Parameters.Add("@Image", SqlDbType.Image).Value = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return;
                }

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công");
                }
                else
                {
                    MessageBox.Show("Không thể thêm sản phẩm");
                }
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "Image Files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.ImageLocation = openFileDialog.FileName;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            //
            if (!short.TryParse(guna2TextBox4.Text, out short quantity))
            {
                guna2TextBox4.Text = "";
                return;
            }

            // Tính giá trị lãi
            decimal unitPrice;
            decimal.TryParse(guna2TextBox5.Text, out unitPrice);
            decimal result = quantity * unitPrice;

            // Gán giá trị tính được cho textbox thứ 3
            guna2TextBox6.Text = result.ToString();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
/*            if (!decimal.TryParse(guna2TextBox5.Text, out decimal unitPrice))
            {
                guna2TextBox5.Text = "";
                return;
            }

            // Tính giá trị lãi
            short quantity;
            short.TryParse(guna2TextBox4.Text, out quantity);
            decimal result = quantity * unitPrice;

            // Gán giá trị tính được cho textbox thứ 3
            guna2TextBox6.Text = result.ToString();*/
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string commandText = "INSERT INTO Orders(ProductName,CustomerName, Quantity, UnitPrice, LineTotal,OrderStatus, BranchID,Anh) VALUES (@ProductName,@CustomerName, @Quantity, @UnitPrice, @LineTotal,@OrderStatus, @BranchID, @Image)";
                SqlCommand command = new SqlCommand(commandText, connection);


                command.Parameters.Add("@ProductName", SqlDbType.VarChar, 50).Value = guna2TextBox2.Text;
                command.Parameters.Add("@CustomerName", SqlDbType.VarChar, 50).Value = guna2TextBox7.Text;
                command.Parameters.Add("@Quantity", SqlDbType.SmallInt).Value = Int16.Parse(guna2TextBox4.Text);
                command.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = decimal.Parse(guna2TextBox5.Text);
                command.Parameters.Add("@LineTotal", SqlDbType.Decimal).Value = decimal.Parse(guna2TextBox6.Text);
                command.Parameters.Add("@OrderStatus", SqlDbType.NVarChar, 50).Value = "Mua hàng thành công";
                command.Parameters.Add("@BranchID", SqlDbType.Int).Value = int.Parse(guna2ComboBox1.Text);

                /*  // Kiểm tra chi nhánh đã chọn
                  if (int.Parse(guna2ComboBox1.Text) == 1)
                  {
                      command.Parameters.Add("@BranchID", SqlDbType.Int).Value = 1;
                      chinhanh1 cn1 = new chinhanh1();
                      cn1.ShowDialog();
                  }
                  else if (int.Parse(guna2ComboBox1.Text)==2 )
                  {
                      command.Parameters.Add("@BranchID", SqlDbType.Int).Value = 2;
                      chinhanh2 cn2 = new chinhanh2();
                      cn2.ShowDialog();
                  }
                  else if (int.Parse(guna2ComboBox1.Text) == 3)
                  {
                      command.Parameters.Add("@BranchID", SqlDbType.Int).Value = 3;
                      chinhanh3 cn3 = new chinhanh3();
                      cn3.ShowDialog();
                  }
                  else
                  {
                      MessageBox.Show("Bạn chưa chọn chi nhánh");
                      return;
                  }*/
                // Kiểm tra file ảnh đã chọn
                try
                {
                    // Kiểm tra file ảnh đã chọn
                    if (pictureBox.Image == null)
                    {
                        MessageBox.Show("Bạn chưa chọn ảnh");
                        return;
                    }
                    Image originalImage = pictureBox.Image;
                    Image copiedImage = new Bitmap(originalImage);
                    pictureBox.Image = copiedImage;
                    byte[] image = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        image = ms.ToArray();
                    }
                    command.Parameters.Add("@Image", SqlDbType.Image).Value = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return;
                }

                // Nếu không có lỗi, thực hiện thêm sản phẩm vào database
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {

                    MessageBox.Show("Thêm sản phẩm thành công");
                    // Kiểm tra chi nhánh đã chọn
                    if (int.Parse(guna2ComboBox1.Text) == 1)
                    {

                        chinhanh1 cn1 = new chinhanh1();
                        this.Hide();

                        cn1.ShowDialog();
                        quanaonam quanao = new quanaonam();
                        quanao.ShowDialog();
                    }
                    else if (int.Parse(guna2ComboBox1.Text) == 2)
                    {

                        this.Hide();

                        chinhanh2 cn2 = new chinhanh2();
                        cn2.ShowDialog();
                        quanaonam quanao = new quanaonam();
                        quanao.ShowDialog();
                    }
                  
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn chi nhánh");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Không thể thêm sản phẩm");
                }
                connection.Close();
            }
        }

      

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = guna2DataGridView2.CurrentRow.Index;
            guna2TextBox1.Text = guna2DataGridView2.Rows[i].Cells[0].Value.ToString();
            guna2TextBox8.Text = guna2DataGridView2.Rows[i].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView2.Rows[i].Cells[2].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView2.Rows[i].Cells[3].Value.ToString();
            /*    guna2TextBox4.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();*/
            guna2TextBox5.Text = guna2DataGridView2.Rows[i].Cells[5].Value.ToString();
            object value = guna2DataGridView2.Rows[i].Cells[6].Value;
            if (value != DBNull.Value)
            {
                byte[] anhBytes = (byte[])value;

                if (anhBytes != null && anhBytes.Length > 0)
                {
                    // Chuyển đổi dữ liệu ảnh sang kiểu Image
                    using (MemoryStream ms = new MemoryStream(anhBytes))
                    {
                        Image anh = Image.FromStream(ms);
                        pictureBox.Image = anh;
                    }
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
