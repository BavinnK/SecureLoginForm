namespace secureLoginForm
{
    using System;
    using System.Data.SqlClient;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Data.SqlClient;

    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\source\repos\secureLoginForm\secureLoginForm\Database1.mdf;Integrated Security=True";
        public Form1()
        {

            InitializeComponent();
            btnLogin.Enabled = false;
            txtUsername.TextChanged += ValidateInputs;
            txtPassword.TextChanged += ValidateInputs;
        }
        private void ValidateInputs(object sender, EventArgs e)
        {
            btnLogin.Enabled = txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT PasswordHash, Salt, Role, FailedAttempts, LockoutTime FROM tb_log WHERE Username = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            lblMessage.Text = "User not found.";
                            return;
                        }

                        string storedHash = reader["PasswordHash"].ToString();
                        string salt = reader["Salt"].ToString();
                        string role = reader["Role"].ToString();
                        int failedAttempts = Convert.ToInt32(reader["FailedAttempts"]);
                        DateTime? lockoutTime = reader["LockoutTime"] as DateTime?;

                        reader.Close(); 

                        
                        if (lockoutTime.HasValue && lockoutTime.Value > DateTime.Now)
                        {
                            lblMessage.Text = $"Account locked. Try again at {lockoutTime.Value}.";
                            return;
                        }

                        
                        if (HashPassword(password, salt) == storedHash)
                        {
                            MessageBox.Show("Login successful!");

                            
                            string resetQuery = "UPDATE tb_log SET FailedAttempts = 0, LockoutTime = NULL WHERE Username = @username";
                            using (SqlCommand resetCmd = new SqlCommand(resetQuery, conn))
                            {
                                resetCmd.Parameters.AddWithValue("@username", username);
                                resetCmd.ExecuteNonQuery();
                            }

                            this.Hide(); 

                            
                            if (role == "admin")
                            {
                                Form3 form3 = new Form3();
                                form3.ShowDialog();
                            }
                            else
                            {
                                Form4 form4 = new Form4();
                                form4.ShowDialog();
                            }
                        }
                        else
                        {
                            failedAttempts++;

                            
                            if (failedAttempts >= 3)
                            {
                                lblMessage.Text = "Account locked for 2 minutes.";
                                string lockQuery = "UPDATE tb_log SET FailedAttempts = @failedAttempts, LockoutTime = @lockout WHERE Username = @username";
                                using (SqlCommand lockCmd = new SqlCommand(lockQuery, conn))
                                {
                                    lockCmd.Parameters.AddWithValue("@failedAttempts", failedAttempts);
                                    lockCmd.Parameters.AddWithValue("@lockout", DateTime.Now.AddMinutes(2));
                                    lockCmd.Parameters.AddWithValue("@username", username);
                                    lockCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                lblMessage.Text = $"Invalid login attempt {failedAttempts}/3.";
                                string updateQuery = "UPDATE tb_log SET FailedAttempts = @failedAttempts WHERE Username = @username";
                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@failedAttempts", failedAttempts);
                                    updateCmd.Parameters.AddWithValue("@username", username);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }

        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
        private string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hash = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            this.Hide();  
            Form2 registerForm = new Form2();
            registerForm.ShowDialog();
            
        }
    }
}
