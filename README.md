# **Report: Secure Login and Registration System in C#**

This report details a C# Windows Forms application implementing a secure login and registration system, along with a data insertion feature. The application adheres to security best practices, including password hashing with salting, input validation, and a limited-attempt login mechanism. It uses a SQL Server database to store user data.

**Database:** The database file (`Database1.mdf`) and the script to create the `tb_log` table are essential components. (I'll assume the script is similar to the one I provided in the previous response).

## Table of Contents

1.  [User Registration](#user-registration)
2.  [User Login](#user-login)
3.  [Role-Based Access Control](#role-based-access-control)
4.  [Data Input (Conceptual)](#data-input)
5.  [Input Validation](#input-validation)
6.  [Password Hashing and Salting](#password-hashing-and-salting)
7.  [Login Attempt Limitation and Lockout](#login-attempt-limitation-and-lockout)

## 1. User Registration

**Description:** Enables new users to create accounts.  The system validates input, securely hashes the password with a unique salt, and stores the user information in the database.

**Screenshot (Description):**

*   **Registration Form (Form2):** A form with textboxes for "New Username," "New Password," "Email," "New Age," and "Phone Number." A ComboBox for "Role" selection (admin/user).  "Create Account" and "Exit" buttons.

**Code Snippet (Form2.cs - `btnCreateAccount_Click`):**

```csharp
private void btnCreateAccount_Click(object sender, EventArgs e)
{
    // ... (Input validation - see section 5) ...

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
```

**Key Points:**

*   **SQL Injection Prevention:** Uses parameterized SQL queries to prevent SQL injection attacks.
*   **Password Hashing:**  Hashes the password using `HashPassword` (see Section 6).
*   **Salt Generation:** Generates a unique salt using `Guid.NewGuid().ToString()`.
*   **Database Insertion:** Inserts the username, hashed password, salt, email, age, phone number, role ('user' - hardcoded), failed attempts (0), and lockout time (NULL) into the `tb_log` table.
*   **User Feedback:** Displays a success message and navigates back to the login form (Form1).
*  **Role Combobox:** the code contains combobox to let the user select the role.

## 2. User Login

**Description:** Authenticates registered users.  The system retrieves the stored salt, re-hashes the entered password, and compares it to the stored hash.

**Screenshot (Description):**


*   **Login Form (Form1):**  "Username" and "Password" textboxes (with placeholder text).  "Login" (initially disabled), "Register," and "Exit" buttons.  `lblMessage` for error messages.

**Code Snippet (Form1.cs - `btnLog_Click`):**

```csharp
private void btnLog_Click(object sender, EventArgs e)
{
    // ... (Connection setup and query - see previous responses) ...

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

        reader.Close(); // Close the reader before further processing

        // ... (Lockout check - see Section 7) ...

        if (HashPassword(password, salt) == storedHash)
        {
            // ... (Reset failed attempts, redirect based on role) ...
        }
        else
        {
            // ... (Increment failed attempts, lock account if needed) ...
        }
    }
}
```

**Key Points:**

*   **Database Query:** Retrieves the stored hash, salt, role, failed attempts, and lockout time.
*   **User Existence Check:** Handles the case where the user is not found.
*   **Password Verification:**  Re-hashes the entered password with the retrieved salt and compares it to the stored hash.
*   **Role Retrieval:**  Retrieves the user's role for role-based access control (Section 3).
*    **Reader Closing**: The reader is closed before any further processing.

## 3. Role-Based Access Control

**Description:** After successful login, the application directs users to different forms based on their assigned role.

**Code Snippet (Form1.cs - `btnLog_Click` - within the successful login block):**

```csharp
if (HashPassword(password, salt) == storedHash)
{
    // ... (Reset failed attempts) ...

    this.Hide(); // Hide the login form

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
```

**Key Points:**

*   **Role Check:** Checks the `role` retrieved from the database.
*   **Conditional Navigation:**
    *   If the role is "admin," opens `Form3`.
    *   Otherwise (for "user" or other roles), opens `Form4`.
*   **Form Hiding:** Hides the login form (`Form1`) before showing the next form.

## 4. Data Input (Conceptual)

**Description:**  (Based on the assignment description, as the code for Form3/Form4 is not provided). After successful login, users (depending on their role) are presented with a form to enter or manage data.

**Screenshot (Description):**

*   **Data Entry Form (Form3/Form4):** Textboxes for Name, Age, Email, and Phone Number.  "Submit" (initially disabled), "Clear" buttons. Tooltips for guidance.

**Conceptual Code Snippet (Illustrative - not from your provided code):**

```csharp
// Example: Enabling/Disabling the Submit button
private void ValidateForm()
{
    bool allValid = !string.IsNullOrWhiteSpace(txtName.Text) &&
                    !string.IsNullOrWhiteSpace(txtAge.Text) &&
                    // ... (other field checks) ... &&
                    Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                    // ... (other validation checks) ...;

    btnSubmit.Enabled = allValid;
}
```

**Key Points (Conceptual):**

*   **Input Fields:**  Provides fields for Name, Age, Email, and Phone Number.
*   **Input Restrictions:** Enforces restrictions on input (see Section 5 for validation details).
*   **Submit Button:** Disabled until all fields are correctly filled.
*   **Tooltips:**  Provides guidance to users on input requirements.

## 5. Input Validation

**Description:** The application validates user input on both the registration and data entry forms, ensuring data integrity and security.

**Code Snippets (Form2.cs - `btnCreateAccount_Click` - Validation Examples):**

```csharp
// Email validation
if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
{
    MessageBox.Show("Invalid email format.");
    return;
}

// Age validation
if (!int.TryParse(ageText, out int age) || age < 13 || age > 100) // Note: Assignment says 18-100
{
    MessageBox.Show("Age must be a valid number between 13 and 100.");
    return;
}

// Phone number validation
if (!Regex.IsMatch(phoneNumber, @"^\d{10,15}$"))
{
    MessageBox.Show("Phone number must be between 10 and 15 digits.");
    return;
}
```
**Key Points:**
* The username is checked if it contains only alphanumeric values.
*The password is checked for minimum 8 characters with Uppercase, lowercase, digit and special character.

*   **Regular Expressions:**  Uses regular expressions (`Regex.IsMatch`) for email and phone number validation.
*   **Type Conversion and Range Checks:** Uses `int.TryParse` and range checks for age validation.
*   **Error Handling:**  Displays error messages using `MessageBox.Show` and prevents further processing if validation fails.
*   **Form1 Input Validation:** The `ValidateInputs` method in `Form1` ensures the login button is only enabled when both username and password fields have content.

## 6. Password Hashing and Salting

**Description:** The application employs SHA-256 hashing with a unique, randomly generated salt to securely store passwords.

**Code Snippet (Form1.cs - `HashPassword`):**

```csharp
private string HashPassword(string password, string salt)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] combinedBytes = Encoding.UTF8.GetBytes(password + salt);
        byte[] hash = sha256.ComputeHash(combinedBytes);
        return Convert.ToBase64String(hash);
    }
}
```

**Key Points:**

*   **SHA-256:**  Uses the SHA-256 hashing algorithm.
*   **Salting:**  Combines the password with a unique salt *before* hashing.
*   **Salt Generation (Form2):**  `Guid.NewGuid().ToString()` is used to generate a unique, string-based salt.
*   **Base64 Encoding:** Converts the resulting hash (a byte array) to a Base64-encoded string for storage in the database.
* **using Statement**: The using statment is used for SHA256.

## 7. Login Attempt Limitation and Lockout

**Description:**  The application limits the number of failed login attempts to three.  After three incorrect attempts, the account is temporarily locked for two minutes.

**Code Snippets (Form1.cs - `btnLog_Click`):**

```csharp
// ... (Retrieve failedAttempts and lockoutTime from database) ...

if (lockoutTime.HasValue && lockoutTime.Value > DateTime.Now)
{
    lblMessage.Text = $"Account locked. Try again at {lockoutTime.Value}.";
    return;
}

// ... (Inside the 'else' block for incorrect password) ...

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
    // ... (Update failedAttempts in database) ...
    lblMessage.Text = $"Invalid login attempt {failedAttempts}/3.";
}
```
##Start page
![getstarted](https://github.com/user-attachments/assets/f778fb6a-2cad-46f1-a1be-b8ef15a57f74)

---
##Creating account page
![account](https://github.com/user-attachments/assets/83f29c81-488e-4288-b577-6cebdd87869f)
---

**Key Points:**

*   **Failed Attempts Tracking:**  The `FailedAttempts` column in the `tb_log` table stores the number of consecutive failed login attempts.
*   **Lockout Time:** The `LockoutTime` column stores the date and time when the account lockout expires.
*   **Lockout Check:**  Before attempting to verify the password, the code checks if `LockoutTime` is set and if the current time is still within the lockout period.
*   **Lockout Implementation:**  If `FailedAttempts` reaches 3, the `LockoutTime` is set to the current time plus 2 minutes.
*   **Database Updates:**  The `FailedAttempts` and `LockoutTime` are updated in the database accordingly.
*   **User Feedback:**  Appropriate messages are displayed to the user, indicating the number of failed attempts or the lockout status.
*  **Resetting Failed Attempts:** On successful login, `FailedAttempts` are reset to 0, and `LockoutTime` is set to NULL.


