using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace secureLoginForm
{
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\source\repos\secureLoginForm\secureLoginForm\Database1.mdf;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
            cmbRole.Items.Add("admin");
            cmbRole.Items.Add("user");
            cmbRole.SelectedIndex = 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text;
            string password = txtNewPassword.Text;
            string email = txtEmail.Text;
            string ageText = txtNewAge.Text;
            string phoneNumber = txtPhoneNumber.Text;  

            
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            
            if (!int.TryParse(ageText, out int age) || age < 13 || age > 100)
            {
                MessageBox.Show("Age must be a valid number between 13 and 100.");
                return;
            }

            
            if (!Regex.IsMatch(phoneNumber, @"^\d{10,15}$"))
            {
                MessageBox.Show("Phone number must be between 10 and 15 digits.");
                return;
            }

            
            string salt = Guid.NewGuid().ToString();
            string hashedPassword = HashPassword(password, salt);

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tb_log (Username, PasswordHash, Salt, Email, Age, PhoneNumber, Role, FailedAttempts, LockoutTime) VALUES (@username, @passwordHash, @salt, @Email, @Age, @PhoneNumber, 'user', 0, NULL)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Registration successful!");
            this.Close();
            new Form1().Show();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
