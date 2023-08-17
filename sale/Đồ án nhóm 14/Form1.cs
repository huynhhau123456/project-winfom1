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
using System.Web.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Đồ_án_nhóm_14
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            txtUserName.Text = "-----------Nhập tài khoản-----------"; // Đặt giá trị mặc định cho TextBox
            txtUserName.TabStop = false;
            txtUserName.Enter += new EventHandler(guna2TextBox1_Enter);
            txtUserName.Leave += new EventHandler(guna2TextBox1_Leave);

            txtPassword.Text = "-----------Nhập mật khẩu-----------";
            txtPassword.TabStop = false;
            txtPassword.Enter += new EventHandler(pass_Enter);
            txtPassword.Leave += new EventHandler(pass_Leave);

            comboBoxUserType.Items.Add("Employee");
            comboBoxUserType.Items.Add("Manager");
            comboBoxUserType.Items.Add("Customer");

        }

        private void guna2TextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.Text = "-----------Nhập tài khoản-----------";
            }

        }

        private void guna2TextBox1_Enter(object sender, EventArgs e)
        {

            txtUserName.Text = "";
        }

        private void pass_Enter(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }

        private void pass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "-----------Nhập mật khẩu-----------";
            }
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    
        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-1FAUCLF\\CLIENT;Initial Catalog=Store;Integrated Security=True";
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string userType = comboBoxUserType.Text;

            bool branchA = chkBranchA.Checked;
            bool branchB = chkBranchB.Checked;
            bool branchC = chkBranchC.Checked;

            if ((userType == "Employee" || userType == "Manager") && !branchA && !branchB && !branchC)
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
                query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password AND UserType = @UserType AND ((@BranchA = 1 AND BranchID = 1) OR (@BranchB = 1 AND BranchID = 2) OR (@BranchC = 1 AND BranchID IS NULL))";
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
                        command.Parameters.AddWithValue("@BranchC", branchC);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@BranchA", DBNull.Value);
                        command.Parameters.AddWithValue("@BranchB", DBNull.Value);
                        command.Parameters.AddWithValue("@BranchC", DBNull.Value);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string result = reader["UserType"].ToString();

                                if (result == "Manager" && branchA)
                                {
                                    EmployeeForm employeeForm = new EmployeeForm();
                                    /*   employeeForm.radioButton1.Enabled = false;*/
                                    employeeForm.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Employee" && branchB)
                                {
                                    ManagerForm managerForm = new ManagerForm();
                                    managerForm.Show();

                                    MessageBox.Show("Đăng nhập thành công");
                                }
                                else if (result == "Customer")
                                {
                                    CustomerForm customerForm = new CustomerForm();
                                    customerForm.Show();

                                    MessageBox.Show("Đăng nhập thành công");
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

        private void chkBranchA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranchA.Checked)
            {
                // Ẩn các checkbox khác nếu chi nhánh A được chọn
                chkBranchB.Enabled = false;
                chkBranchC.Enabled = false;
            }
            else
            {
                // Hiện lại các checkbox nếu chi nhánh A bị bỏ chọn
                chkBranchB.Enabled = true;
                chkBranchC.Enabled = true;
            }
        }

        private void chkBranchB_CheckedChanged(object sender, EventArgs e)
        {

            if (chkBranchB.Checked)
            {
                // Ẩn các checkbox khác nếu chi nhánh B được chọn
                chkBranchA.Enabled = false;
                chkBranchC.Enabled = false;
            }
            else
            {
                // Hiện lại các checkbox nếu chi nhánh B bị bỏ chọn
                chkBranchA.Enabled = true;
                chkBranchC.Enabled = true;
            }
        }

        private void chkBranchC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranchC.Checked)
            {
                // Ẩn các checkbox khác nếu chi nhánh C được chọn
                chkBranchA.Enabled = false;
                chkBranchB.Enabled = false;
            }
            else
            {
                // Hiện lại các checkbox nếu chi nhánh C bị bỏ chọn
                chkBranchA.Enabled = true;
                chkBranchB.Enabled = true;
            }
        }

        private void comboBoxUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUserType.Text == "Customer")
            {
                chkBranchA.Visible = false;
                chkBranchB.Visible = false;
                chkBranchC.Visible = false;
            }
            else
            {
                chkBranchA.Visible = true;
                chkBranchB.Visible = true;
                chkBranchC.Visible = true;
            }
        }
    }
}
