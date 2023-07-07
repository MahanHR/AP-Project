using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;

namespace PostProject
{
    public partial class ShowCommentWin : UserControl
    {
        public string EmployeeID = "";
        public ShowCommentWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }
        private void ShowComments_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                string command = "select * from Comments Where OrderID = '" + IDSearch.Text + "'";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data2 = new();
                adapter.Fill(data2);
                if (data2.Rows.Count == 0)
                {
                    throw new Exception("No comment has been submitted for this ID");
                }
                string sending = "";
                for (int i = 0; i < data2.Rows.Count; i++)
                {
                    sending += i + 1 + "." + "Customer ID : " + data2.Rows[i][1].ToString() + "   Comment : \n" + data2.Rows[i][2].ToString() + "\n\n";
                }
                throw new Exception(sending);

            }
            catch (Exception ex)
            {
                Orders.Text = ex.Message;
            }
        }
    }
}
