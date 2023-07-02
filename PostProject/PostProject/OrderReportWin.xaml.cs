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

namespace PostProject
{
    /// <summary>
    /// Interaction logic for OrderReportWin.xaml
    /// </summary>
    public partial class OrderReportWin : UserControl
    {
        public string CustomerID = "";
        public OrderReportWin(string ID)
        {
            CustomerID = ID;
            InitializeComponent();
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
            string SSN = "", Sending = "";
            try
            {
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Customer where CustomerID = '" + CustomerID + "'";
                SqlDataAdapter adapter = new(command, conn);
                DataTable data2 = new();
                adapter.Fill(data2);
                SSN = data2.Rows[0][4].ToString();
                string command2 = "select * from Orders";
                adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                int found = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if(SSN == data.Rows[i][4].ToString())
                    {
                        found++;
                        string Ty = "", PoTy = "";
                        if (int.Parse(data.Rows[i][3].ToString()) == 1)
                        {
                            Ty = "Object";
                        }
                        else if (int.Parse(data.Rows[i][3].ToString()) == 2)
                        {
                            Ty = "Document";
                        }
                        else if (int.Parse(data.Rows[i][3].ToString()) == 3) 
                        {
                            Ty = "Fragile";
                        }
                        if (int.Parse(data.Rows[i][5].ToString()) == 1)
                        {
                            PoTy = "Ordinary";
                        }
                        else if (int.Parse(data.Rows[i][5].ToString()) == 2)
                        {
                            PoTy = "Speed";
                        }
                        Sending += found + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "Price : " + data.Rows[i][9].ToString() + "\n\n";
                    }
                }
                if(found == 0)
                {
                    throw new Exception("You have not submitted any order");
                }
                conn.Close();
                throw new Exception(Sending);
            }
            catch (Exception ex)
            {
                Orders.Text = ex.Message;
            }
        }
    }
}
