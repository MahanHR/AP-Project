using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;

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
