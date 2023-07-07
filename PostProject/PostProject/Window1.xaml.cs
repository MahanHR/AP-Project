using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void TextBox_Name(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_LName(object sender, TextChangedEventArgs e)
        {

        }

        public void PasswordBox_Confirm(object sender, RoutedEventArgs e)
        {

        }

        public void TextBox_Email(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_PersonnelID(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_UName(object sender, TextChangedEventArgs e)
        {

        }

        public void PasswordBox_Pass(object sender, RoutedEventArgs e)
        {

        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            var p = new MainWindow();
            p.Show();
            this.Close();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex firstNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex lastNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex personnelIDRegex = new(@"^\d{2}9\d{2}$");
                Regex usernameRegex = new(@"^\S{3,32}$");
                Regex passwordRegex = new(@"^(?=\S{8})(?!\S{33})(\S*[a-z]+\S*[A-Z]+\S*|\S*[A-Z]+\S*[a-z]+\S*)$");
                Regex emailRegex = new(@"^[\w-\.]{3,32}@[A-z]{3,32}.[A-z]{2,3}$");
                if (!firstNameRegex.Match(Name.Text.ToString()).Success)
                {
                    throw new Exception("Input format of first name is not correct.");
                }
                if (!lastNameRegex.Match(LName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of last name is not correct.");
                }
                if (!personnelIDRegex.Match(PersonnelID.Text.ToString()).Success)
                {
                    throw new Exception("Input format of personnel ID is not correct.");
                }
                if (!usernameRegex.Match(UName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of username is not correct.");
                }
                if (!passwordRegex.Match(Pass.Password.ToString()).Success)
                {
                    throw new Exception("Input format of password is not correct.");
                }
                if (!passwordRegex.Match(PassCon.Password.ToString()).Success)
                {
                    throw new Exception("Input format of password is not correct.");
                }
                if (!emailRegex.Match(Email.Text.ToString()).Success)
                {
                    throw new Exception("Input format of email is not correct.");
                }
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                string command = "select * from Employee";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][0].ToString() == PersonnelID.Text)
                    {
                        conn.Close();
                        throw new Exception("This personnel ID is already used");
                    }
                    if ((string)data.Rows[i][4] == UName.Text)
                    {
                        conn.Close();
                        throw new Exception("This username is already used");
                    }
                    if ((string)data.Rows[i][3] == Email.Text)
                    {
                        conn.Close();
                        throw new Exception("This email is already used");
                    }
                }
                if (Pass.Password != PassCon.Password)
                {
                    throw new Exception("Repeated password does not match");
                }
                /*command = "insert into Employee values('" + int.Parse(PersonnelID.Text) + "','" + Name.Text + "','" + LName.Text + "','" + Email.Text + "','" + UName.Text + "','" + Pass.Password.ToString() + "')";
                SqlCommand cmd = new(command, conn);
                cmd.BeginExecuteNonQuery();
                conn.Close();*/
                String query = "INSERT INTO Employee (PersonnelID,FirstName,LastName,Email,UserName,Pass) VALUES (@id, @f, @l, @e, @us, @ps)";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(PersonnelID.Text));
                command3.Parameters.AddWithValue("@f", Name.Text);
                command3.Parameters.AddWithValue("@l", LName.Text);
                command3.Parameters.AddWithValue("@e", Email.Text);
                command3.Parameters.AddWithValue("@us", UName.Text);
                command3.Parameters.AddWithValue("@ps", Pass.Password);
                command3.ExecuteNonQuery();
                conn.Close();
                var p = new MainWindow();
                p.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
