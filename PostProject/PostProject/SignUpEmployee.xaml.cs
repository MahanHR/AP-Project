﻿using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

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
                Regex firstNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex lastNameRegex = new(@"^[a-zA-Z]{3,32}$");
                Regex personnelIDRegex = new(@"^\d{2}9\d{2}$");
                Regex usernameRegex = new(@"^\S{3,32}$");
                Regex passwordRegex = new(@"^(?=\S{8})(?!\S{33})(\S*[a-z]+\S*[A-Z]+\S*|\S*[A-Z]+\S*[a-z]+\S*)$");
                Regex emailRegex = new(@"^[\w-\.]{3,32}@[A-z]{3,32}.[A-z]{2,3}$");
                if (!firstNameRegex.Match(Name.Text.ToString()).Success)
                {
                    throw new Exception("Input format of first name is not correct.");
                }
                if (!lastNameRegex.Match(LName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of last name is not correct.");
                }
                if (!personnelIDRegex.Match(PersonnelID.Text.ToString()).Success)
                {
                    throw new Exception("Input format of personnel ID is not correct.");
                }
                if (!usernameRegex.Match(UName.Text.ToString()).Success)
                {
                    throw new Exception("Input format of username is not correct.");
                }
                if (!passwordRegex.Match(Pass.Password.ToString()).Success)
                {
                    throw new Exception("Input format of password is not correct.");
                }
                if (!passwordRegex.Match(PassCon.Password.ToString()).Success)
                {
                    throw new Exception("Input format of password is not correct.");
                }
                if (!emailRegex.Match(Email.Text.ToString()).Success)
                {
                    throw new Exception("Input format of email is not correct.");
                }
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\source\repos\HajAmir-Post\AP-Project\PostProject\PostProject\SQL\save.mdf;Integrated Security=True");
                conn.Open();
                string command = "select * from Employee";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if ((string)data.Rows[i][4] == UName.Text)
                    {
                        throw new Exception("The username is already used");
                    }
                    if ((string)data.Rows[i][3] == Email.Text)
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
