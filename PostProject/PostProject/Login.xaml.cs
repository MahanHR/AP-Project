using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new SignUpEmployee();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex usernameRegex = new(@"^\S{3,32}$");
                Regex customerPasswordRegex = new(@"^\d{8}$");
                Regex employeePasswordRegex = new(@"^(?=\S{8})(?!\S{33})(\S*[a-z]+\S*[A-Z]+\S*|\S*[A-Z]+\S*[a-z]+\S*)$");
                if (!usernameRegex.Match(Uname.Text.ToString()).Success)
                {
                    throw new Exception("Input format of username is not correct");
                }
                if (!(customerPasswordRegex.Match(Pass.Password.ToString()).Success|employeePasswordRegex.Match(Pass.Password.ToString()).Success))
                {
                    throw new Exception("Input format of password is not correct");
                }
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=SQL\save.mdf;Integrated Security=True");
                conn.Open();
                string command = "select * from Employee";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                command = "select * from Employee";
                adapter = new SqlDataAdapter(command, conn);
                data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string commandup = "update Employee Set PersonnelID ='" + data.Rows[i][0].ToString() + "',FirstName = '" + data.Rows[i][1].ToString() + "',LastName = '" + data.Rows[i][2].ToString() + "',Email = '" + data.Rows[i][3].ToString() + "',UserName = '" + data.Rows[i][4].ToString() + "',PassWord = '" + data.Rows[i][5].ToString() + "',isEntered = '" + 0 + "' where PersonnelID = '" + i + 1 + "'";
                    SqlCommand cmd = new(commandup, conn);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][4].ToString() == Uname.Text.ToString() && data.Rows[i][5].ToString() == Pass.Password.ToString())
                    {
                        string commandup = "update Employee Set PersonnelID ='" + data.Rows[i][0].ToString() + "',FirstName = '" + data.Rows[i][1].ToString() + "',LastName = '" + data.Rows[i][2].ToString() + "',Email = '" + data.Rows[i][3].ToString() + "',UserName = '" + data.Rows[i][4].ToString() + "',PassWord = '" + data.Rows[i][5].ToString() + "',isEntered = '" + 1 + "' where UserName = '" + Uname.Text.ToString() + "'";
                        SqlCommand cmd = new(commandup, conn);
                        cmd.BeginExecuteNonQuery();
                        conn.Close();
                        Application.Current.MainWindow.Content = new Employee();
                        throw new Exception();
                    }
                }
                command = "select * from Customer";
                adapter = new SqlDataAdapter(command, conn);
                data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string commandup = "update Customer Set CustomerID = '" + data.Rows[i][0].ToString() + "',FirstName = '" + data.Rows[i][1].ToString() + "',LastName = '" + data.Rows[i][2].ToString() + "',Email = '" + data.Rows[i][3].ToString() + "',SSN = '" + data.Rows[i][4].ToString() + "',Phone = '" + data.Rows[i][5].ToString() + "',UserName = '" + data.Rows[i][6].ToString() + "',PassWord = '" + data.Rows[i][7].ToString() + "',isEntered = '" + 0 + "' where CustomerID = '" + i + 1 + "'";
                    SqlCommand cmd = new(commandup, conn);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][6].ToString() == Uname.Text.ToString() && data.Rows[i][7].ToString() == Pass.Password.ToString())
                    {
                        string commandup = "update Customer Set CustomerID = '" + data.Rows[i][0].ToString() + "',FirstName = '" + data.Rows[i][1].ToString() + "',LastName = '" + data.Rows[i][2].ToString() + "',Email = '" + data.Rows[i][3].ToString() + "',SSN = '" + data.Rows[i][4].ToString() + "',Phone = '" + data.Rows[i][5].ToString() + "',UserName = '" + data.Rows[i][6].ToString() + "',PassWord = '" + data.Rows[i][7].ToString() + "',isEntered = '" + 1 + "' where UserName = '" + Uname.Text.ToString() + "'";
                        SqlCommand cmd = new(commandup, conn);
                        cmd.BeginExecuteNonQuery();
                        conn.Close();
                        Application.Current.MainWindow.Content = new Customer();
                        throw new Exception();
                    }
                }
                conn.Close();
                throw new Exception("No account found");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
