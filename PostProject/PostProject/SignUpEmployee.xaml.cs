using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Data.SqlClient;

namespace PostProject
{
    public partial class SignUpEmployee : Page
    {
        public SignUpEmployee()
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
            Application.Current.MainWindow.Content = new Login();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
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
                    if (data.Rows[i][4] == UName.Text)
                    {
                        throw new Exception("The username is already used");
                    }
                    if (data.Rows[i][3] == Email.Text)
                    {
                        throw new Exception("The email is already used");
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;    
            }
        }
    }
}
