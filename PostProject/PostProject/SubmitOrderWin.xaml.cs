using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
                Phone.IsEnabled = false;
                wBox.IsEnabled = false;
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
                isEXy.IsEnabled = false;
                isEXn.IsEnabled = false;
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
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
                int check = 0;
                double price = 10;
                for (int i = 0; i < Phone.Text.Length; i++)
                {
                    if (Phone.Text[i].ToString() != " ")
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
                if(wBox.Text.Length == 0)
                {
                    throw new Exception("Invalid weight");
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
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                string command = "select * from Orders";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                int idSetter = data.Rows.Count + 1;
                int posttype = 0, packtype = 0;
                string isexc = "";
                if (Ordinary.IsChecked == true)
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
                    price *= 3;
                    price /= 2;
                    packtype = 2;
                }
                else if (Fragile.IsChecked == true)
                {
                    price *= 2;
                    packtype = 3;
                }
                if (isEXn.IsChecked == true)
                {
                    isexc = "No";
                }
                else if (isEXy.IsChecked == true)
                {
                    price *= 2;
                    isexc = "Yes";
                }
                if (double.Parse(wBox.Text) > 0.5)
                {
                    double y = double.Parse(wBox.Text) / 0.5;
                    int yy = (int)y;
                    price *= Math.Pow(1.2, yy);
                }
                string o = price.ToString("f4");
                price = double.Parse(o);
                string command10 = "select * from Customer where SSN = '" + SSNsearch.Text + "'";
                SqlDataAdapter adapter10 = new(command10, conn);
                DataTable data10 = new();
                adapter10.Fill(data10);
                if(double.Parse(data10.Rows[0][8].ToString()) - price < 0)
                {
                    throw new Exception("This customer's balance is lower than the price");
                }
                String query2 = "UPDATE Customer SET Balance = @b Where CustomerID = @id";
                SqlCommand command5 = new SqlCommand(query2, conn);
                double h = double.Parse(data10.Rows[0][8].ToString()) - price;
                string hh = h.ToString("f4");
                h = double.Parse(hh);
                command5.Parameters.AddWithValue("@id", int.Parse(data10.Rows[0][0].ToString()));
                command5.Parameters.AddWithValue("@b", h);
                command5.ExecuteNonQuery();
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
                command3.Parameters.AddWithValue("@pri", price);
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
                wBox.Text = "";
                Phone.Text = "";
                Origin.IsEnabled = false;
                Destination.IsEnabled = false;
                Phone.IsEnabled = false;
                wBox.IsEnabled = false;
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
                isEXy.IsEnabled = false;
                isEXn.IsEnabled = false;
                throw new Exception("The order is submitted successfully by price of " + price);
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
