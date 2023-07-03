using System.Windows;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Controls;

namespace PostProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var p = new Window1();
            p.Show();
            this.Close();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Employee";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][4].ToString() == Uname.Text.ToString() && data.Rows[i][5].ToString() == Pass.Password.ToString())
                    {
                        conn.Close();
                        var p = new Employees();
                        p.Show();
                        p.EmployeeID = data.Rows[i][0].ToString();
                        this.Close();
                        throw new Exception();
                    }
                }
                command = "select * from Customer";
                adapter = new SqlDataAdapter(command, conn);
                data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][6].ToString() == Uname.Text.ToString() && data.Rows[i][7].ToString() == Pass.Password.ToString())
                    {
                        conn.Close();
                        var p = new Customers();
                        p.Show();
                        p.CustomerID = data.Rows[i][0].ToString();
                        this.Close();
                        throw new Exception();
                    }
                }
                throw new Exception("No account found");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
