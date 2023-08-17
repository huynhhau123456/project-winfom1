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
    public partial class Login : Form
    {

        public static string result;
        public static int checkchinhanh;
        public Login()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
          
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            anime.HideSync(p2);
            anime.ShowSync(p4);
        }

        private void label4_Click(object sender, EventArgs e)
        {
      
        }

        private void p2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string userType = comboBoxUserType.Text;

            bool branchA = chkBranchA.Checked;
            bool branchB = chkBranchB.Checked;
            /*  bool branchC = chkBranchC.Checked;*/

            if ((userType == "Employee" || userType == "Manager") && !branchA && !branchB)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một chi nhánh.");
                return;
            }

            string query;
            if (userType == "Customer")
            {
                query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password AND UserType = @UserType";
            }
            else
            {
                query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password AND UserType = @UserType AND ((@BranchA = 1 AND BranchID = 1) OR (@BranchB = 1 AND BranchID = 2))";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@UserType", userType);

                    if (userType == "Employee" || userType == "Manager")
                    {
                        command.Parameters.AddWithValue("@BranchA", branchA);
                        command.Parameters.AddWithValue("@BranchB", branchB);

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@BranchA", DBNull.Value);
                        command.Parameters.AddWithValue("@BranchB", DBNull.Value);

                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result = reader["UserType"].ToString();


                                if (result == "Manager" && branchA)
                                {
                                    chinhanh1 cn1 = new chinhanh1();
                                    /*   employeeForm.radioButton1.Enabled = false;*/
                                    cn1.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Manager" && branchA)
                                {
                                    chinhanh1 cn1 = new chinhanh1();
                                    /*   employeeForm.radioButton1.Enabled = false;*/
                                    cn1.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Manager" && branchB)
                                {
                                    chinhanh2 cn2 = new chinhanh2();
                                    /*   employeeForm.radioButton1.Enabled = false;*/
                                    cn2.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Employee" && branchA)
                                {
                                    checkchinhanh = 1;
                                    chinhanh1 cn1 = new chinhanh1();
                                    cn1.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Employee" && branchB)
                                {
                                    chinhanh2 cn2 = new chinhanh2();
                                    cn2.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Customer")
                                {
                                    // Ẩn Form hiện tại
                                    this.Hide();
                                    Danhmucsanpham customerForm = new Danhmucsanpham();
                                    customerForm.ShowDialog();
                                    // Đóng Form mới và hiển thị lại Form hiện tại sau khi Form mới được đóng
                                    this.Show();

                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy dữ liệu.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thông tin đăng nhập không chính xác.");
                        }
                    }
                }
            }
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            anime.HideSync(p4);
            anime.ShowSync(p2);
        }

        private void chkBranchA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranchA.Checked)
            {
                // Ẩn các checkbox khác nếu chi nhánh A được chọn
                chkBranchB.Enabled = false;
            }
            else
            {
                // Hiện lại các checkbox nếu chi nhánh A bị bỏ chọn
                chkBranchB.Enabled = true;

            }
        }

        private void chkBranchB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranchB.Checked)
            {
                // Ẩn các checkbox khác nếu chi nhánh B được chọn
                chkBranchA.Enabled = false;

            }
            else
            {
                // Hiện lại các checkbox nếu chi nhánh B bị bỏ chọn
                chkBranchA.Enabled = true;

            }
        }

        private void comboBoxUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUserType.Text == "Customer")
            {
                chkBranchA.Visible = false;
                chkBranchB.Visible = false;

            }
            else
            {
                chkBranchA.Visible = true;
                chkBranchB.Visible = true;

            }
        }

        private void label4_Click_2(object sender, EventArgs e)
        {
            anime.HideSync(p4);
            anime.ShowSync(p2);
        }

        private void p4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserType.Text = "Customer";
            txtUserType.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";
            string customerName = txtCustomerName.Text;
            string address = txtAddress.Text;
            string phoneNumber = txtPhoneNumber.Text;
            decimal balance = decimal.Parse(txtBalance.Text);
            string email = txtEmail.Text;
            string username = txtUsername1.Text;
            string password = txtPassword1.Text;
            string userType = txtUserType.Text;
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            if (!decimal.TryParse(txtBalance.Text, out balance))
            {
                MessageBox.Show("Vui lòng nhập giá trị số cho Balance.");
                return;
            }

            if (!int.TryParse(txtPhoneNumber.Text, out int phoneNumberValue))
            {
                MessageBox.Show("Vui lòng nhập giá trị số cho PhoneNumber.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Thêm khách hàng vào bảng Customer
                    SqlCommand customerCommand = new SqlCommand("INSERT INTO Customer (CustomerName, Address, PhoneNumber, Balance, Email) VALUES (@CustomerName, @Address, @PhoneNumber, @Balance, @Email)", connection);
                    customerCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    customerCommand.Parameters.AddWithValue("@Address", address);
                    customerCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    customerCommand.Parameters.AddWithValue("@Balance", balance);
                    customerCommand.Parameters.AddWithValue("@Email", email);

                    int customerRowsAffected = customerCommand.ExecuteNonQuery();

                    // Thêm tài khoản và mật khẩu vào bảng Users
                    SqlCommand userCommand = new SqlCommand("INSERT INTO Users (UserName, Password, UserType, BranchID) VALUES (@UserName, @Password, @UserType, NULL)", connection);
                    userCommand.Parameters.AddWithValue("@UserName", username);
                    userCommand.Parameters.AddWithValue("@Password", password);
                    userCommand.Parameters.AddWithValue("@UserType", userType);

                    int userRowsAffected = userCommand.ExecuteNonQuery();

                    if (customerRowsAffected > 0 && userRowsAffected > 0)
                    {
                        MessageBox.Show("Đăng ký thành công!");
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Đăng ký không thành công. Vui lòng thử lại.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
        private void ClearForm()
        {
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtPhoneNumber.Text = "";
            txtBalance.Text = "";
            txtEmail.Text = "";
            txtUsername1.Text = "";
            txtPassword1.Text = "";
            txtUserType.Text = "Customer";
        }
    }
}
