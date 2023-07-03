﻿using System;
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
    /// <summary>
    /// Interaction logic for EditUandPWin.xaml
    /// </summary>
    public partial class EditUandPWin : UserControl
    {
        public string CustomerIDin = "";
        public EditUandPWin(string ID)
        {
            CustomerIDin = ID;
            InitializeComponent();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            string userName = UBox.Text , passW = PassBox.Password ;
            try
            {
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Customer";
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][6].ToString() == userName)
                    {
                        throw new Exception("Username already used");
                    }
                }
                Regex usernameRegex = new(@"^\S{3,32}");
                Regex regex = new (@"^\d{8}$");
                if (!(usernameRegex.Match(userName).Success))
                {
                    conn.Close();
                    throw new Exception("Invalid Username");
                }
                if (!(regex.Match(passW).Success))
                {
                    conn.Close();
                    throw new Exception("Invalid Password");
                }
                //Buuuuuuuuuuuuuuuuuuuuuuuuuug!!!!!!!!!!!!!!!!!!!!!!!!1
                string command2 = "select * from Customer where CustomerID = '" + CustomerIDin + "'";
                SqlDataAdapter adapter2 = new(command2, conn);
                DataTable data2 = new();
                adapter.Fill(data2);
                int jj = int.Parse(CustomerIDin);
                //String query = "UPDATE Customer(CustomerID,FirstName,LastName,Email,SSN,Phone,UserName,Pass) VALUES (@id, @Fi, @La, @Em, @SSN, @Ph, @usern, @pass) WHERE CustomerID = '" + jj + "'";
                String query = "UPDATE Customer SET UserName = @usern, Pass = @pass Where CustomerID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", jj);
                command3.Parameters.AddWithValue("@usern", userName);
                command3.Parameters.AddWithValue("@pass", passW);
                command3.ExecuteNonQuery();
                conn.Close();
                throw new Exception("Username and pass changed successfully");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}



/*
 Syntax to insert :              (Don't Erase It)
 String query = "INSERT INTO Customer (CustomerID,FirstName,LastName,Email,SSN,Phone,UserName,Pass) VALUES (@id, @Fi, @La, @Em, @SSN, @Ph, @usern, @pass)";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", jj+1);
                command3.Parameters.AddWithValue("@Fi", data2.Rows[0][1].ToString());
                command3.Parameters.AddWithValue("@La", data2.Rows[0][2].ToString());
                command3.Parameters.AddWithValue("@Em", data2.Rows[0][3].ToString());
                command3.Parameters.AddWithValue("@SSN", data2.Rows[0][4].ToString());
                command3.Parameters.AddWithValue("@Ph", data2.Rows[0][5].ToString());
                command3.Parameters.AddWithValue("@usern", userName);
                command3.Parameters.AddWithValue("@pass", passW);
                command3.ExecuteNonQuery(); 
 */
