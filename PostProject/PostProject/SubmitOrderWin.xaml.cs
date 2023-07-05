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
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace PostProject
{
    public partial class SubmitOrderWin : UserControl
    {
        public string EmployeeID = "";
        public SubmitOrderWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Origin.IsEnabled = false;
                Destination.IsEnabled = false;
                pBox.IsEnabled = false;
                Phone.IsEnabled = false;
                wBox.IsEnabled = false;
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
                isEXy.IsEnabled = false;
                isEXn.IsEnabled = false;
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command2 = "select * from Customer where SSN = '" + SSNsearch.Text + "'";
                SqlDataAdapter adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                if (data.Rows.Count == 0)
                {
                    throw new Exception("Invalid SSN");
                }
                Origin.IsEnabled = true;
                Destination.IsEnabled = true;
                pBox.IsEnabled = true;
                Phone.IsEnabled = true;
                wBox.IsEnabled = true;
                Object.IsEnabled = true;
                Document.IsEnabled = true;
                Fragile.IsEnabled = true;
                Ordinary.IsEnabled = true;
                Speed.IsEnabled = true;
                isEXy.IsEnabled = true;
                isEXn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!(pBox.Text[0] >= 49 && pBox.Text[0] <= 57))
                {
                    throw new Exception("Invalid price");
                }
                for(int i = 1; i < pBox.Text.Length; i++)
                {
                    if (!(pBox.Text[i] >= 48 && pBox.Text[i] <= 57))
                    {
                        throw new Exception("Invalid price");
                    }
                }
                int check = 0;
                for(int i = 0;i < Phone.Text.Length; i++)
                {
                    if(Phone.Text[i].ToString() != " ")
                    {
                        check++;
                    }
                }
                if (check != 0)
                {
                    Regex phoneNumberRegex = new(@"^09\d{9}$");
                    if (!phoneNumberRegex.Match(Phone.Text.ToString()).Success)
                    {
                        throw new Exception("Input format of phone number is not correct");
                    }
                }
                if (!(wBox.Text[0] >= 49 && wBox.Text[0] <= 57))
                {
                    throw new Exception("Invalid weight");
                }
                for (int i = 1; i < wBox.Text.Length; i++)
                {
                    if (!(wBox.Text[i] >= 48 && wBox.Text[i] <= 57))
                    {
                        throw new Exception("Invalid weight");
                    }
                }
                if (Document.IsChecked == false && Fragile.IsChecked == false && Object.IsChecked == false)
                {
                    throw new Exception("You must select a Package type");
                }
                if (Ordinary.IsChecked == false && Speed.IsChecked == false)
                {
                    throw new Exception("You must select a Post type");
                }
                if (isEXn.IsChecked == false && isEXy.IsChecked == false)
                {
                    throw new Exception("You must specify the package value");
                }
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Orders";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                int idSetter = data.Rows.Count + 1;
                int posttype = 0, packtype = 0;
                string isexc = "";
                if(Ordinary.IsChecked == true)
                {
                    posttype = 1;
                }
                else if (Speed.IsChecked == true)
                {
                    posttype = 2;
                }
                if (Object.IsChecked == true)
                {
                    packtype = 1;
                }
                else if (Document.IsChecked == true)
                {
                    packtype = 2;
                }
                else if (Fragile.IsChecked == true)
                {
                    packtype = 3;
                }
                if (isEXn.IsChecked == true)
                {
                    isexc = "No";
                }
                else if (isEXy.IsChecked == true)
                {
                    isexc = "Yes";
                }
                String query = "INSERT INTO Orders (ID,Origin,Destination,Type,SSN,PostType,IsExpensive,Weight,Phone,Price,isReceived,Status) VALUES (@id, @o, @d, @ty, @S, @Pty, @isex, @wei, @ph, @pri, @isRe, @Sta)";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", idSetter);
                command3.Parameters.AddWithValue("@o", Origin.Text);
                command3.Parameters.AddWithValue("@d", Destination.Text);
                command3.Parameters.AddWithValue("@ty", packtype);
                command3.Parameters.AddWithValue("@S", SSNsearch.Text);
                command3.Parameters.AddWithValue("@Pty", posttype);
                command3.Parameters.AddWithValue("@isex", isexc);
                command3.Parameters.AddWithValue("@wei", int.Parse(wBox.Text));
                command3.Parameters.AddWithValue("@ph", Phone.Text);
                command3.Parameters.AddWithValue("@pri", int.Parse(pBox.Text));
                command3.Parameters.AddWithValue("@isRe", "No");
                command3.Parameters.AddWithValue("@Sta", 1);
                command3.ExecuteNonQuery();
                conn.Close();
                Document.IsChecked = false;
                Object.IsChecked = false;
                Fragile.IsChecked = false;
                Speed.IsChecked = false;
                Ordinary.IsChecked = false;
                isEXy.IsChecked = false;
                isEXn.IsChecked = false;
                SSNsearch.Text = "";
                Origin.Text = "";
                Destination.Text = "";
                pBox.Text = "";
                wBox.Text = "";
                Phone.Text = "";
                Origin.IsEnabled = false;
                Destination.IsEnabled = false;
                pBox.IsEnabled = false;
                Phone.IsEnabled = false;
                wBox.IsEnabled = false;
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
                isEXy.IsEnabled = false;
                isEXn.IsEnabled = false;
                throw new Exception("The order is submitted successfully");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }

        private void Object_Checked(object sender, RoutedEventArgs e)
        {
            Document.IsChecked = false;
            Fragile.IsChecked = false;
        }

        private void Document_Checked(object sender, RoutedEventArgs e)
        {
            Object.IsChecked = false;
            Fragile.IsChecked = false;
        }

        private void Fragile_Checked(object sender, RoutedEventArgs e)
        {
            Document.IsChecked = false;
            Object.IsChecked = false;
        }

        private void Speed_Checked(object sender, RoutedEventArgs e)
        {
            Ordinary.IsChecked = false;
        }

        private void Ordinary_Checked(object sender, RoutedEventArgs e)
        {
            Speed.IsChecked = false;
        }

        private void isEXy_Checked(object sender, RoutedEventArgs e)
        {
            isEXn.IsChecked = false;
        }

        private void isEXn_Checked(object sender, RoutedEventArgs e)
        {
            isEXy.IsChecked = false;
        }
    }
}
