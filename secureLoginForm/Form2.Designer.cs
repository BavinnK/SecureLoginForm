namespace secureLoginForm
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtNewUsername = new TextBox();
            txtNewPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            cmbRole = new ComboBox();
            btnCreateAccount = new Button();
            btnExit = new Button();
            lblRegisterMessage = new Label();
            txtEmail = new TextBox();
            txtNewAge = new TextBox();
            label6 = new Label();
            txtAge = new Label();
            label7 = new Label();
            txtPhoneNumber = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MediumSlateBlue;
            label1.Location = new Point(41, 26);
            label1.Name = "label1";
            label1.Size = new Size(161, 40);
            label1.TabIndex = 1;
            label1.Text = "Get Started";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(41, 142);
            label2.Name = "label2";
            label2.Size = new Size(67, 17);
            label2.TabIndex = 2;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(41, 255);
            label3.Name = "label3";
            label3.Size = new Size(92, 17);
            label3.TabIndex = 3;
            label3.Text = "PhoneNumber";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlDarkDark;
            label5.Location = new Point(41, 429);
            label5.Name = "label5";
            label5.Size = new Size(83, 17);
            label5.TabIndex = 4;
            label5.Text = "Select a Role";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(41, 370);
            label4.Name = "label4";
            label4.Size = new Size(114, 17);
            label4.TabIndex = 5;
            label4.Text = "Confirm Password";
            // 
            // txtNewUsername
            // 
            txtNewUsername.BorderStyle = BorderStyle.None;
            txtNewUsername.Location = new Point(41, 162);
            txtNewUsername.Multiline = true;
            txtNewUsername.Name = "txtNewUsername";
            txtNewUsername.Size = new Size(273, 23);
            txtNewUsername.TabIndex = 6;
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderStyle = BorderStyle.None;
            txtNewPassword.Location = new Point(41, 334);
            txtNewPassword.Multiline = true;
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(273, 23);
            txtNewPassword.TabIndex = 7;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.None;
            txtConfirmPassword.Location = new Point(41, 390);
            txtConfirmPassword.Multiline = true;
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(273, 23);
            txtConfirmPassword.TabIndex = 8;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FlatStyle = FlatStyle.Flat;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(41, 449);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(273, 23);
            cmbRole.TabIndex = 9;
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.BackColor = Color.MediumSlateBlue;
            btnCreateAccount.FlatAppearance.BorderSize = 0;
            btnCreateAccount.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnCreateAccount.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnCreateAccount.FlatStyle = FlatStyle.Flat;
            btnCreateAccount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateAccount.ForeColor = Color.White;
            btnCreateAccount.Location = new Point(78, 507);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new Size(200, 30);
            btnCreateAccount.TabIndex = 10;
            btnCreateAccount.Text = "REGISTER";
            btnCreateAccount.UseVisualStyleBackColor = false;
            btnCreateAccount.Click += btnCreateAccount_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.White;
            btnExit.FlatAppearance.BorderColor = Color.MediumSlateBlue;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.MediumSlateBlue;
            btnExit.Location = new Point(78, 568);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(200, 30);
            btnExit.TabIndex = 11;
            btnExit.Text = "EXIT";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // lblRegisterMessage
            // 
            lblRegisterMessage.AutoSize = true;
            lblRegisterMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRegisterMessage.ForeColor = Color.Red;
            lblRegisterMessage.Location = new Point(108, 362);
            lblRegisterMessage.Name = "lblRegisterMessage";
            lblRegisterMessage.Size = new Size(0, 15);
            lblRegisterMessage.TabIndex = 12;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Location = new Point(41, 106);
            txtEmail.Multiline = true;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(273, 23);
            txtEmail.TabIndex = 13;
            // 
            // txtNewAge
            // 
            txtNewAge.BorderStyle = BorderStyle.None;
            txtNewAge.Location = new Point(41, 219);
            txtNewAge.Multiline = true;
            txtNewAge.Name = "txtNewAge";
            txtNewAge.Size = new Size(273, 23);
            txtNewAge.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(41, 86);
            label6.Name = "label6";
            label6.Size = new Size(39, 17);
            label6.TabIndex = 2;
            label6.Text = "Email";
            // 
            // txtAge
            // 
            txtAge.AutoSize = true;
            txtAge.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAge.ForeColor = SystemColors.ControlDarkDark;
            txtAge.Location = new Point(41, 199);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(31, 17);
            txtAge.TabIndex = 2;
            txtAge.Text = "Age";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(41, 314);
            label7.Name = "label7";
            label7.Size = new Size(64, 17);
            label7.TabIndex = 3;
            label7.Text = "Password";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.BorderStyle = BorderStyle.None;
            txtPhoneNumber.Location = new Point(41, 275);
            txtPhoneNumber.Multiline = true;
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(273, 23);
            txtPhoneNumber.TabIndex = 15;
            txtPhoneNumber.TextChanged += textBox1_TextChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 640);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtNewAge);
            Controls.Add(txtEmail);
            Controls.Add(lblRegisterMessage);
            Controls.Add(btnExit);
            Controls.Add(btnCreateAccount);
            Controls.Add(cmbRole);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(txtNewUsername);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(txtAge);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create Account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
        private TextBox txtNewUsername;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole;
        private Button btnCreateAccount;
        private Button btnExit;
        private Label lblRegisterMessage;
        private TextBox txtEmail;
        private TextBox txtNewAge;
        private Label label6;
        private Label txtAge;
        private Label label7;
        private TextBox txtPhoneNumber;
    }
}