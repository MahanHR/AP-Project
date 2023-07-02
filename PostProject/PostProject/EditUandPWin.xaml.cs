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
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (data.Rows[i][0].ToString() == CustomerIDin)
                    {
                        string commandup = "insert into Customer values('"+ Convert.ToInt32(data.Rows[i][0].ToString()) +"','"+ data.Rows[i][1].ToString() +"','"+ data.Rows[i][2].ToString() +"','"+ data.Rows[i][3].ToString() +"','"+ data.Rows[i][4].ToString() +"','"+ data.Rows[i][5].ToString() +"','"+ userName +"','"+ passW +"' )";
                        /*
                        SqlCommand cmd = new SqlCommand("UpdateUserPass", conn);
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerID", Convert.ToInt32(data.Rows[i][4].ToString()));
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Pass", passW);
                        
                        string commandup = "update Customer Set CustomerID = '" + data.Rows[i][0].ToString() + "',FirstName = '" + data.Rows[i][1].ToString() + "',LastName = '" + data.Rows[i][2].ToString() + "',Email = '" + data.Rows[i][3].ToString() + "',SSN = '" + data.Rows[i][4].ToString() + "',Phone = '" + data.Rows[i][5].ToString() + "',UserName = '" + userName + "',Pass = '" + passWord + "' where CustomerID = '" + CustomerIDin + "'";
                        */
                        SqlCommand cmd = new SqlCommand(commandup, conn);
                        cmd.BeginExecuteNonQuery();
                        conn.Close();
                        throw new Exception("Success");
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
