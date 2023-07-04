﻿using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PostProject
{
    public partial class CustomerWalletWin : UserControl
    {
        public string CustomerID = "";
        public CustomerWalletWin(string ID)
        {
            CustomerID = ID;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowBalance_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
            conn.Open();
            string command = "select * from Customer";
            SqlDataAdapter adapter = new(command, conn);
            DataTable data = new();
            adapter.Fill(data);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][0].ToString() == CustomerID)
                {
                    Balance.Text = "Your Balance IS: " + data.Rows[i][8].ToString();
                    conn.Close();
                    break;
                }
            }
        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (!long.TryParse(CardNum.Text, out _))
                {
                    throw new Exception("Card number is not entered correctly");
                }
                string cardNo = CardNum.Text;
                int Digitscount = cardNo.Length;
                int Sum = 0;
                bool isSecond = false;
                for (int i = Digitscount - 1; i >= 0; i--)
                {
                    int d = cardNo[i] - '0';
                    if (isSecond)
                    {
                        d *= 2;
                    }
                    Sum += d / 10;
                    Sum += d % 10;
                    isSecond = !isSecond;
                }
                if (Sum % 10 != 0)
                {
                    throw new Exception("Card number is not valid");
                }
                if (!int.TryParse(expYear.Text, out _))
                {
                    throw new Exception("Year is not entered correctly");
                }
                if (!int.TryParse(expMonth.Text, out _))
                {
                    throw new Exception("Month is not entered correctly");
                }
                if (int.Parse(expMonth.Text) > 12 || int.Parse(expMonth.Text) < 1)
                {
                    throw new Exception("Month is not entered correctly");
                }
                if (DateTime.Now.Year > int.Parse(expYear.Text))
                {
                    throw new Exception("Card is expired");
                }
                if (DateTime.Now.Year == int.Parse(expYear.Text) && DateTime.Now.Month > int.Parse(expMonth.Text))
                {
                    throw new Exception("Card is expired");
                }
                Regex cvv2Regex = new(@"^\d{3,4}$");
                if (!cvv2Regex.Match(CVV2.Text).Success)
                {
                    throw new Exception("CVV2 is not valid");
                }
                if (!int.TryParse(Amount.Text, out _))
                {
                    throw new Exception("Amount is not entered correctly");
                }
                int currentCharge = 0;
                int chargeAmount = int.Parse(Amount.Text);
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Customer";
                SqlDataAdapter adapter = new SqlDataAdapter(command, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][0].ToString() == CustomerID)
                    {
                        currentCharge = int.Parse(data.Rows[i][8].ToString());
                    }
                }
                command = "select * from Customer where CustomerID = '" + CustomerID + "'";
                adapter = new(command, conn);
                data = new();
                adapter.Fill(data);
                String query = "UPDATE Customer SET Balance = @b Where CustomerID = @id";
                SqlCommand command2 = new SqlCommand(query, conn);
                command2.Parameters.AddWithValue("@b", chargeAmount + currentCharge);
                command2.Parameters.AddWithValue("@id", int.Parse(CustomerID));
                command2.ExecuteNonQuery();
                conn.Close();
                throw new Exception("Your account has been charged");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
