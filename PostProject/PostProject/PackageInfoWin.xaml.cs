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
    /// Interaction logic for PackageInfoWin.xaml
    /// </summary>
    public partial class PackageInfoWin : UserControl
    {
        public string CustomerID = "";
        public PackageInfoWin(string IDGet)
        {
            CustomerID = IDGet;
            InitializeComponent();
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";

            string SSN = "", Sending = "", ID = IDSearch.Text;
            try
            {
                Comment.IsEnabled = false;
                Comment.Text = "Leave your comment here";
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command = "select * from Customer where CustomerID = '" + CustomerID + "'";
                SqlDataAdapter adapte = new(command, conn);
                DataTable data = new();
                adapte.Fill(data);
                SSN = data.Rows[0][4].ToString();
                string command2 = "select * from Orders";
                SqlDataAdapter adapter = new(command2, conn);
                data = new();
                adapter.Fill(data);
                int found = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (SSN == data.Rows[i][4].ToString() && ID == data.Rows[i][0].ToString())
                    {
                        found++;
                        string Ty = "", PoTy = "",Stu = "";
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
                        if (int.Parse(data.Rows[i][3].ToString()) == 1)
                        {
                            Stu = "Submitted";
                        }
                        else if (int.Parse(data.Rows[i][3].ToString()) == 2)
                        {
                            Stu = "Ready To Send";
                        }
                        else if (int.Parse(data.Rows[i][3].ToString()) == 3)
                        {
                            Stu = "Is Sending";
                        }
                        else if (int.Parse(data.Rows[i][3].ToString()) == 4)
                        {
                            Stu = "Sent";
                        }
                        Sending += found + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "   Price : " + data.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                    }
                }
                if (found == 0)
                {
                    throw new Exception("You have not ordered this package");
                }
                conn.Close();
                Comment.IsEnabled = true;
                Comment.Text = "";
                throw new Exception(Sending);
            }
            catch (Exception ex)
            {
                Orders.Text = ex.Message;
            }
        }

        private void SendComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Comment.IsEnabled)
                {
                    SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                    conn.Open();
                    string com = "Select * from Orders where ID = '" + IDSearch.Text + "'";
                    SqlDataAdapter sql = new(com,conn);
                    DataTable data20 = new();
                    sql.Fill(data20);
                    if (data20.Rows[0][10].ToString().ToLower() == "no")
                    {
                        throw new Exception("This Order has not been sent yet");
                    }
                    string command = "select * from Comments";
                    SqlDataAdapter adapter = new(command, conn);
                    DataTable data2 = new();
                    adapter.Fill(data2);
                    for (int i = 0; i < data2.Rows.Count; i++)
                    {
                        if (data2.Rows[i][1].ToString() == CustomerID)
                        {
                            String query = "UPDATE Comments SET Comment = @c Where CustomerID = @id";
                            SqlCommand command3 = new SqlCommand(query, conn);
                            command3.Parameters.AddWithValue("@id", int.Parse(CustomerID));
                            command3.Parameters.AddWithValue("@c", Comment.Text);
                            command3.ExecuteNonQuery();
                            conn.Close();
                            throw new Exception("Your Comment is submitted");
                        }
                    }
                    String query2 = "INSERT INTO Comments (OrderID,CustomerID,Comment) VALUES (@Oid, @Cid, @Co)";
                    SqlCommand command2 = new SqlCommand(query2, conn);
                    command2.Parameters.AddWithValue("@Oid", int.Parse(IDSearch.Text));
                    command2.Parameters.AddWithValue("@Cid", int.Parse(CustomerID));
                    command2.Parameters.AddWithValue("@Co", Comment.Text);
                    command2.ExecuteNonQuery();
                    conn.Close();
                    throw new Exception("Your Comment is submitted");
                }
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
