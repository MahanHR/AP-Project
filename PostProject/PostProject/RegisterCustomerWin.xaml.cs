using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for RegisterCustomerWin.xaml
    /// </summary>
    public partial class RegisterCustomerWin : UserControl
    {
        public string EmployeeID = "";
        public RegisterCustomerWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        public void TextBox_Name(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_LName(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_Email(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_SSNNumber(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_Number(object sender, TextChangedEventArgs e)
        {

        }

        private void SignUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Regex firstNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex lastNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex ssnRegex = new(@"^00\d{8}$");
                Regex phoneNumberRegex = new(@"^09\d{9}$");
                Regex emailRegex = new(@"^[\w-\.]{3,32}@[A-z]{3,32}.[A-z]{2,3}$");
                if (!firstNameRegex.Match(FName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of first name is not correct.");
                }
                if (!lastNameRegex.Match(LName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of last name is not correct.");
                }
                if (!ssnRegex.Match(SSN.Text.ToString()).Success)
                {
                    throw new Exception("Input format of SSN is not correct");
                }
                if (!phoneNumberRegex.Match(PhoneNo.Text.ToString()).Success)
                {
                    throw new Exception("Input format of phone number is not correct");
                }
                if (!emailRegex.Match(Email.Text.ToString()).Success)
                {
                    throw new Exception("Input format of email is not correct.");
                }
                string firstName = FName.Text.ToString();
                string lastName = LName.Text.ToString();
                string email = Email.Text.ToString();
                string ssn = SSN.Text.ToString();
                string phoneNumber = PhoneNo.Text.ToString();
                //
                var exclude = new HashSet<int>();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Customer";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if ((string)data.Rows[i][4] == SSN.Text)
                    {
                        conn.Close();
                        throw new Exception("Person with this SSN already has an account");
                    }
                    if ((string)data.Rows[i][3] == Email.Text)
                    {
                        conn.Close();
                        throw new Exception("The email is already used");
                    }
                    if ((string)data.Rows[i][5] == PhoneNo.Text)
                    {
                        conn.Close();
                        throw new Exception("This phone number is already used");
                    }
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    int usedUserNum = int.Parse(data.Rows[i][6].ToString().Substring(4));
                    exclude.Add(usedUserNum);
                }
                var range = Enumerable.Range(1, 9999).Where(i => !exclude.Contains(i));
                var rand = new Random();
                int index = rand.Next(0, 9999 - exclude.Count);
                //
                string username = "user" + range.ElementAt(index).ToString();
                //
                exclude.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    int usedPass = int.Parse(data.Rows[i][7].ToString());
                    exclude.Add(usedPass);
                }
                range = Enumerable.Range(0, 99999999).Where(i => !exclude.Contains(i));
                rand = new Random();
                index = rand.Next(0, 99999999 - exclude.Count);
                string generated = range.ElementAt(index).ToString();
                while (generated.Length < 8)
                {
                    generated = "0" + generated;
                }
                //
                string password = generated;
                int balance = 0;
                command = "insert into Customer values('" + firstName + "','" + lastName + "','" + email + "','" + ssn + "','" + phoneNumber + "','" + username + "','" + password + "','" + balance + "')";
                conn.Close();

            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
            }
        }
    }
}
