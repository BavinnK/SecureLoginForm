# Secure Login and Registration System in C# (Windows Forms)

This project demonstrates a C# Windows Forms application implementing a secure login and registration system using a SQL Server database. It incorporates security best practices like password hashing with salting, input validation, and a login attempt limitation mechanism.

---

## ✨ Features

*   **Secure User Registration:** Allows new users to create accounts with validated input.
*   **Secure User Login:** Authenticates existing users.
*   **Password Security:** Uses **SHA-256 hashing** with a unique **salt** for each password.
*   **Input Validation:** Enforces rules for username, password complexity, email format, age range, and phone number format.
*   **Login Attempt Limitation:** Locks user accounts temporarily (2 minutes) after 3 consecutive failed login attempts.
*   **Role-Based Access Control (RBAC):** Redirects users to different forms based on their role (e.g., 'admin', 'user') after successful login.
*   **SQL Server Integration:** Stores user credentials and details securely in a SQL Server database (`tb_log` table).
*   **SQL Injection Prevention:** Uses parameterized queries to protect against SQL injection vulnerabilities.

---

## 📸 Screenshots

**Login Screen (Starting Page):**
![getstarted](https://github.com/user-attachments/assets/c048fdd7-6a2b-427e-879f-e311a84f6345)

*   *Description:* The initial form where users enter their username and password. The "Login" button is disabled until both fields are filled. Includes options to Register or Exit.

**Registration Screen (Creating Account Page):**
![account](https://github.com/user-attachments/assets/15171b26-8a46-4778-a855-4361ecc52012)

*   *Description:* The form for new users to create an account. Includes fields for username, password, email, age, phone number, and role selection. Input validation is performed before account creation.


---

## 🛠️ Technologies Used

*   **Language:** C#
*   **Framework:** .NET Framework (for Windows Forms)
*   **UI:** Windows Forms
*   **Database:** SQL Server
*   **Hashing Algorithm:** SHA-256

---

## ⚙️ Setup & Prerequisites

1.  **Environment:**
    *   Windows Operating System
    *   .NET Framework (version compatible with the project, likely 4.x) installed.
    *   Microsoft Visual Studio (2017, 2019, 2022 recommended).
    *   SQL Server (Express, Developer, or Standard edition) installed and running.

2.  **Database Setup:**
    *   **Option A (Using .mdf file):** Ensure the `Database1.mdf` file is included in the project structure and the connection string in the code (e.g., `Form1.cs`, `Form2.cs`) correctly points to it, potentially using `AttachDbFilename=|DataDirectory|\Database1.mdf;`. The `|DataDirectory|` token usually resolves to the application's output directory (e.g., `bin\Debug`).
    *   **Option B (Using SQL Script):** If you have a `.sql` script, connect to your SQL Server instance using SQL Server Management Studio (SSMS) or a similar tool. Create a database (e.g., `UserAuthDB`) and execute the script to create the `tb_log` table with the necessary columns:
        ```sql
        -- Example SQL Script (Adjust data types/lengths as needed)
        CREATE TABLE tb_log (
            Id INT PRIMARY KEY IDENTITY(1,1),
            Username NVARCHAR(50) UNIQUE NOT NULL,
            PasswordHash NVARCHAR(MAX) NOT NULL, -- Store Base64 SHA256 hash
            Salt NVARCHAR(MAX) NOT NULL,         -- Store GUID salt
            Email NVARCHAR(100) UNIQUE NULL,     -- Added UNIQUE constraint if desired
            Age INT NULL,
            PhoneNumber NVARCHAR(15) NULL,
            Role NVARCHAR(20) NOT NULL DEFAULT 'user',
            FailedAttempts INT NOT NULL DEFAULT 0,
            LockoutTime DATETIME2 NULL
        );
        ```
    *   **Connection String:** **Crucially, update the `connectionString` variable** in the C# code (`Form1.cs`, `Form2.cs`, etc.) to match your specific SQL Server instance, database name, and authentication method (Windows Authentication or SQL Server Authentication). Example:
        ```csharp
        // Example for SQL Server Express with Windows Authentication
        string connectionString = @"Server=.\SQLEXPRESS;Database=UserAuthDB;Integrated Security=True;";
        // Example for SQL Server Authentication
        // string connectionString = @"Server=YourServerName;Database=UserAuthDB;User ID=YourSqlUser;Password=YourSqlPassword;";
        ```

3.  **Build the Project:**
    *   Clone or download the repository.
    *   Open the solution file (`.sln`) in Visual Studio.
    *   Build the solution (Build > Build Solution or `Ctrl+Shift+B`).

---

## ▶️ How to Use

1.  **Run the Application:** Press `F5` or click the "Start" button in Visual Studio.
2.  **Login:** The Login form (`Form1`) will appear.
    *   Enter a registered username and password.
    *   Click "Login".
    *   If credentials are correct and the account isn't locked, you'll be redirected based on your role (Form3 for admin, Form4 for user).
    *   If credentials are incorrect, an error message will show, and the failed attempt counter increases.
    *   After 3 failed attempts, the account locks for 2 minutes.
3.  **Register:**
    *   Click the "Register" button on the Login form.
    *   The Registration form (`Form2`) will appear.
    *   Fill in all fields according to the validation rules (tooltips might provide hints).
    *   Select a role (if applicable, though the code seems to default to 'user').
    *   Click "Create Account".
    *   If successful, a confirmation message appears, and you are returned to the Login form.
    *   If validation fails, an error message specific to the issue will be shown.
4.  **Data Input (Conceptual - Form3/Form4):**
    *   After logging in, the appropriate form (Form3 or Form4) appears.
    *   These forms are intended for data entry (Name, Age, Email, Phone), likely with their own validation. *Note: The detailed implementation of Form3/Form4 is not fully covered in the provided snippets.*
5.  **Exit:** Click the "Exit" button on the forms to close the application.

---

## 🔐 Implementation Details

### 1. User Registration (`Form2.cs`)

*   **Process:** Collects username, password, email, age, phone, and role. Validates input. Generates a unique salt (`Guid.NewGuid().ToString()`). Hashes the password concatenated with the salt using SHA-256. Stores user data (including hash and salt) in the `tb_log` table via a parameterized SQL query.
*   **Code Snippet (`btnCreateAccount_Click`):**
    ```csharp
    private void btnCreateAccount_Click(object sender, EventArgs e)
    {
        // --- Input Validation (See Section 5) ---
        // Example: Validate username, password complexity, email, age, phone

        if (/* validation fails */)
        {
            MessageBox.Show("Validation Error Message");
            return;
        }

        string username = txtNewUsername.Text;
        string password = txtNewPassword.Text;
        string email = txtEmail.Text;
        // ... get other fields ...

        string salt = Guid.NewGuid().ToString();
        string hashedPassword = HashPassword(password, salt); // Assumes HashPassword method is accessible

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                // Use Parameterized Query to prevent SQL Injection
                string query = @"INSERT INTO tb_log
                                 (Username, PasswordHash, Salt, Email, Age, PhoneNumber, Role, FailedAttempts, LockoutTime)
                                 VALUES
                                 (@username, @passwordHash, @salt, @Email, @Age, @PhoneNumber, @Role, 0, NULL)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Age", /* parsed age */);
                    cmd.Parameters.AddWithValue("@PhoneNumber", /* phone number */);
                    cmd.Parameters.AddWithValue("@Role", comboBoxRole.SelectedItem?.ToString() ?? "user"); // Get selected role or default

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Registration successful!");
                this.Close(); // Close registration form
                // Optionally show login form again: new Form1().Show();
            }
            catch (SqlException ex)
            {
                // Handle potential SQL errors (e.g., duplicate username)
                MessageBox.Show($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
    ```

### 2. User Login (`Form1.cs`)

*   **Process:** Takes username and password. Retrieves the user's record (including hash, salt, role, failed attempts, lockout time) from the database using the username. Checks for account lockout. If not locked, hashes the entered password with the retrieved salt. Compares the generated hash with the stored hash.
*   **Success:** If hashes match, resets failed attempts, clears lockout time, hides the login form, and shows the appropriate role-based form (Form3 or Form4).
*   **Failure:** If hashes don't match, increments the failed attempts counter. If attempts reach 3, sets the lockout time. Updates the database and displays an error message.
*   **Code Snippet (`btnLog_Click`):** *(See original report snippet - it accurately reflects the logic)*

### 3. Role-Based Access Control (RBAC) (`Form1.cs`)

*   **Process:** After successful authentication, the `Role` value retrieved from the database determines which form is displayed next.
*   **Code Snippet (`btnLog_Click` - Success Block):** *(See original report snippet)*

### 4. Data Input (Conceptual - `Form3.cs` / `Form4.cs`)

*   **Purpose:** These forms are placeholders for the application's core functionality after login (e.g., viewing/managing data).
*   **Expected Features:** Input fields (Name, Age, Email, Phone), validation logic, Submit/Clear buttons, Tooltips for guidance. The Submit button should ideally be disabled until all required fields are validly filled.

### 5. Input Validation

*   **Where:** Implemented in `Form1` (Login - basic check for non-empty fields), `Form2` (Registration - comprehensive checks), and conceptually in `Form3`/`Form4` (Data Input).
*   **Checks:**
    *   **Username:** Non-empty, potentially alphanumeric (`Regex.IsMatch(username, @"^[a-zA-Z0-9]+$")`).
    *   **Password:** Non-empty, minimum length (e.g., 8), complexity (uppercase, lowercase, digit, special character - using `Regex` or manual checks).
    *   **Email:** Valid format using `Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")`.
    *   **Age:** Numeric, within a defined range (e.g., 13-100 or 18-100) using `int.TryParse` and range checks.
    *   **Phone Number:** Numeric, specific length range (e.g., 10-15 digits) using `Regex.IsMatch(phoneNumber, @"^\d{10,15}$")`.
*   **Feedback:** Uses `MessageBox.Show` for errors and enables/disables buttons based on validity.

### 6. Password Hashing and Salting

*   **Algorithm:** SHA-256.
*   **Salt:** A unique salt (`Guid.NewGuid().ToString()`) is generated for *each user* during registration and stored alongside the hashed password.
*   **Process:** `password + salt` -> SHA-256 Hash -> Base64 Encode -> Store in DB.
*   **Verification:** Retrieve salt -> `entered_password + salt` -> SHA-256 Hash -> Base64 Encode -> Compare with stored hash.
*   **Code Snippet (`HashPassword` method):** *(See original report snippet - ensure this method is accessible where needed, perhaps in a utility class or base form)*

### 7. Login Attempt Limitation and Lockout

*   **Mechanism:** Uses `FailedAttempts` (INT) and `LockoutTime` (DATETIME2 or DATETIME) columns in `tb_log`.
*   **Trigger:** `FailedAttempts` increments on incorrect password entry.
*   **Lockout:** When `FailedAttempts` >= 3, `LockoutTime` is set to `DateTime.Now.AddMinutes(2)`.
*   **Check:** Before processing a login attempt, the code checks if `LockoutTime` is set and `DateTime.Now` is before the stored `LockoutTime`.
*   **Reset:** On successful login, `FailedAttempts` is reset to 0 and `LockoutTime` is set to `NULL`.
*   **Code Snippets (`btnLog_Click` - Lockout Check & Update Logic):** *(See original report snippet)*

---



