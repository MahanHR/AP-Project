using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Data.SqlClient;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        public void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        public void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var p = new Window1();
            p.Show();
            this.Close();
        }

        private void alaki_Click(object sender, RoutedEventArgs e)
        {
            var p = new Customer();
            p.Show();
            this.Close();
        }

        private void alaki2_Click(object sender, RoutedEventArgs e)
        {
            var p = new Employee();
            p.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\source\repos\HajAmir-Post\AP-Project\PostProject\PostProject\SQL\save.mdf;Integrated Security=True");
                conn.Open();
                string command = "select * from Employee";
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][4].ToString() == Uname.Text.ToString() && data.Rows[i][5].ToString() == Pass.Password.ToString())
                    {                        
                        string commandup = "update Employee Set isEntered = '" + 1 + "' where Employee.UserName = '" + Uname.Text.ToString() + "'";
                        SqlCommand cmd = new SqlCommand(commandup, conn);
                        cmd.BeginExecuteNonQuery();
                        conn.Close();
                        var p = new Employee();
                        p.Show();
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
                        string commandup = "update Customer Set CusomerID = '" + Convert.ToInt32(data.Rows[i][0]) + "',FirstName = '" + data.Rows[i][1] + "',LastName = '" + data.Rows[i][2] + "',Email = '" + data.Rows[i][3] + "',SSN Number = '" + data.Rows[i][4] + "',Phone Number = '" + data.Rows[i][5] + "',UserName = '" + data.Rows[i][6] + "',PassWord = '" + data.Rows[i][7] + "',isEntered = '" + 1 + "' where Customer.UserName = '" + Uname.Text + "'";
                        SqlCommand cmd = new SqlCommand(commandup, conn);
                        cmd.BeginExecuteNonQuery();
                        conn.Close();
                        var p = new Customer();
                        p.Show();
                        this.Close();
                        throw new Exception();
                    }
                }
                throw new Exception("No account found");

                conn.Close();
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
