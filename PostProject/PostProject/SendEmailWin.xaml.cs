﻿using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class SendEmailWin : UserControl
    {
        public string EmployeeID = "";
        public SendEmailWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
