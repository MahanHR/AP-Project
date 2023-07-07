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
using System.IO;
 

namespace PostProject
{
    /// <summary>
    /// Interaction logic for OrderReportEmpWin.xaml
    /// </summary>
    public partial class OrderReportEmpWin : UserControl
    {
        public string EmployeeID = "";
        public OrderReportEmpWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }
        private void Serach_Click(object sender, RoutedEventArgs e)
        {
            string Sending = "";
            try
            {
                bool ssn = (SSN.IsChecked == true);
                bool packType = (PackageType.IsChecked == true && (Object.IsChecked == true || Document.IsChecked == true || Fragile.IsChecked == true));
                bool price = (Price.IsChecked == true);
                bool weight = (Weight.IsChecked == true);
                bool postType = (PostType.IsChecked == true && (Ordinary.IsChecked == true || Speed.IsChecked == true));
                string command = "select * from Orders";
                int isOneTrue = 0;
                if (ssn)
                {
                    if(isOneTrue == 0)
                    {
                        command += " WHERE SSN = '" + SBox.Text + "'";
                    }
                    else
                    {
                        command += " AND SSN = '" + SBox.Text + "'";
                    }
                    isOneTrue++;
                }
                if (price)
                {
                    if (isOneTrue == 0)
                    {
                        command += " WHERE Price = '" + pBox.Text + "'";
                    }
                    else
                    {
                        command += " AND Price = '" + pBox.Text + "'";
                    }
                    isOneTrue++;
                }
                if (weight)
                {
                    if (isOneTrue == 0)
                    {
                        command += " WHERE Weight = '" + wBox.Text + "'";
                    }
                    else
                    {
                        command += " AND Weight = '" + wBox.Text + "'";
                    }
                    isOneTrue++;
                }
                if (postType)
                {
                    bool speed = (Speed.IsChecked == true);
                    bool ordinary = (Ordinary.IsChecked == true);
                    bool both = speed && ordinary;
                    if (!(both))
                    {
                        if (isOneTrue == 0)
                        {
                            if (speed)
                            {
                                command += " WHERE PostType = '" + 2 + "'";
                            }
                            else if (ordinary)
                            {
                                command += " WHERE PostType = '" + 1 + "'";
                            }
                        }
                        else
                        {
                            if (speed)
                            {
                                command += " AND PostType = '" + 2 + "'";
                            }
                            else if (ordinary)
                            {
                                command += " AND PostType = '" + 1 + "'";
                            }
                        }
                        isOneTrue++;
                    }
                }
                if (packType)
                {
                    bool fragile = (Fragile.IsChecked == true);
                    bool document = (Document.IsChecked == true);
                    bool obj = (Object.IsChecked == true);
                    bool all = fragile && document && obj;
                    if (!(all))
                    {
                        if (isOneTrue == 0)
                        {
                            if (fragile && document)
                            {
                                command += " WHERE PostType = '" + 2 + "' OR PostType = '" + 3 + "'";
                            }
                            else if (document && obj)
                            {
                                command += " WHERE PostType = '" + 2 + "' OR PostType = '" + 1 + "'";
                            }
                            else if (fragile && obj)
                            {
                                command += " WHERE PostType = '" + 1 + "' OR PostType = '" + 3 + "'";
                            }
                        }
                        else
                        {
                            if (fragile && document)
                            {
                                command += " AND PostType = '" + 2 + "' OR PostType = '" + 3 + "'";
                            }
                            else if (document && obj)
                            {
                                command += " AND PostType = '" + 2 + "' OR PostType = '" + 1 + "'";
                            }
                            else if (fragile && obj)
                            {
                                command += " AND PostType = '" + 1 + "' OR PostType = '" + 3 + "'";
                            }
                        }
                        isOneTrue++;
                    }
                }
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                SqlDataAdapter adapter = new(command, conn);
                DataTable data = new();
                adapter.Fill(data);
                int found = 0;

                String file = path + @"\CSV\Orders.csv";
                String separator = ",";
                StringBuilder output = new StringBuilder();
                for (int i = 0; i < data.Rows.Count; i++)
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
                    string y = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
                    Sending += found + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "Price : " + data.Rows[i][9].ToString() + "\n\n";
                    String[] newLine = { data.Rows[i][0].ToString(), data.Rows[i][1].ToString(), data.Rows[i][2].ToString(), Ty, PoTy, data.Rows[i][6].ToString(), data.Rows[i][10].ToString(), data.Rows[i][7].ToString(), data.Rows[i][9].ToString(), y};
                    output.AppendLine(string.Join(separator, newLine));
                }
                if (found == 0)
                {
                    throw new Exception("Nothing is ordered by this properties");
                }
                conn.Close();
                File.AppendAllText(file, output.ToString());

                throw new Exception(Sending);
            }
            catch (Exception ex)
            {
                Orders.Text = ex.Message;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SSN.IsChecked == true)
            {
                SBox.IsEnabled = true;
            }

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (PackageType.IsChecked == true)
            {
                Object.IsEnabled = true;
                Document.IsEnabled = true;
                Fragile.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Price.IsChecked == true)
            {
                pBox.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Weight.IsChecked == true)
            {
                wBox.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            if (PostType.IsChecked == true)
            {
                Ordinary.IsEnabled = true;
                Speed.IsEnabled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Object_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Document_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Fragile_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Ordinary_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Speed_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SSN_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SSN.IsChecked == false)
            {
                SBox.IsEnabled = false;
            }
        }

        private void PackageType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PackageType.IsChecked == false)
            {
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
            }
        }

        private void Price_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Price.IsChecked == false)
            {
                pBox.IsEnabled = false;
            }
        }

        private void Weight_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Weight.IsChecked == false)
            {
                wBox.IsEnabled = false;
            }
        }

        private void PostType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PostType.IsChecked == false)
            {
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
            }
        }

        private void Object_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Document_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Fragile_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Ordinary_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Speed_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }
}
