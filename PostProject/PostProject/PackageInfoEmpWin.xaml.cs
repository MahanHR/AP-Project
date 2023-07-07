using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class PackageInfoEmpWin : UserControl
    {
        public string EmployeeID = "";
        public PackageInfoEmpWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";

            string SSN = "", Sending = "", ID = IDsearch.Text;
            try
            {
                Submitted.IsEnabled = false;
                Sent.IsEnabled = false;
                RTSend.IsEnabled = false;
                IsSending.IsEnabled = false;
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                if (data.Rows.Count == 0)
                {
                    throw new Exception("You have not ordered this package");
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string Ty = "", PoTy = "", Stu = "";
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
                    Submitted.IsEnabled = true;
                    Sent.IsEnabled = true;
                    RTSend.IsEnabled = true;
                    IsSending.IsEnabled = true;
                    if (int.Parse(data.Rows[i][11].ToString()) == 1)
                    {
                        Submitted.IsChecked = true;
                        Stu = "Submitted";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 2)
                    {
                        RTSend.IsChecked = true;
                        Stu = "Ready To Send";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 3)
                    {
                        IsSending.IsChecked = true;
                        Stu = "Is Sending";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 4)
                    {
                        Sent.IsChecked = true;
                        Submitted.IsEnabled = false;
                        Sent.IsEnabled = false;
                        RTSend.IsEnabled = false;
                        IsSending.IsEnabled = false;
                        Stu = "Sent";
                    }
                    Sending += i + 1 + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "   Price : " + data.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                }
                conn.Close();
                throw new Exception(Sending);
            }
            catch (Exception ex)
            {
                Orders.Text = ex.Message;
            }
        }
        private void Submitted_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                String query = "UPDATE Orders SET isReceived = @c, Status = @s Where ID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(IDsearch.Text));
                command3.Parameters.AddWithValue("@s", 1);
                command3.Parameters.AddWithValue("@c", "NO");
                command3.ExecuteNonQuery();
                conn.Close();
                IsSending.IsChecked = false;
                RTSend.IsChecked = false;
                Sent.IsChecked = false;
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                string Sending = "";
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string Ty = "", PoTy = "", Stu = "";
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
                    if (int.Parse(data.Rows[i][11].ToString()) == 1)
                    {
                        Stu = "Submitted";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 2)
                    {
                        Stu = "Ready To Send";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 3)
                    {
                        Stu = "Is Sending";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 4)
                    {
                        Stu = "Sent";
                    }
                    Sending += i + 1 + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "   Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "   Price : " + data.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                }
                Orders.Text = Sending;
                throw new Exception("Status has been set as 'Submitted'");

            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }

        private void RTSend_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                String query = "UPDATE Orders SET isReceived = @c, Status = @s Where ID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(IDsearch.Text));
                command3.Parameters.AddWithValue("@s", 2);
                command3.Parameters.AddWithValue("@c", "NO");
                command3.ExecuteNonQuery();
                conn.Close();
                Submitted.IsChecked = false;
                IsSending.IsChecked = false;
                Sent.IsChecked = false;
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                string Sending = "";
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string Ty = "", PoTy = "", Stu = "";
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
                    if (int.Parse(data.Rows[i][11].ToString()) == 1)
                    {
                        Stu = "Submitted";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 2)
                    {
                        Stu = "Ready To Send";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 3)
                    {
                        Stu = "Is Sending";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 4)
                    {
                        Stu = "Sent";
                    }
                    Sending += i + 1 + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "   Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "   Price : " + data.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                }
                Orders.Text = Sending;
                throw new Exception("Status has been set as 'Ready To Send'");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
        private void IsSending_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                String query = "UPDATE Orders SET isReceived = @c, Status = @s Where ID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(IDsearch.Text));
                command3.Parameters.AddWithValue("@s", 3);
                command3.Parameters.AddWithValue("@c", "NO");
                command3.ExecuteNonQuery();
                conn.Close();
                Submitted.IsChecked = false;
                RTSend.IsChecked = false;
                Sent.IsChecked = false;
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new(command2, conn);
                DataTable data = new();
                adapter.Fill(data);
                string Sending = "";
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string Ty = "", PoTy = "", Stu = "";
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
                    if (int.Parse(data.Rows[i][11].ToString()) == 1)
                    {
                        Stu = "Submitted";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 2)
                    {
                        Stu = "Ready To Send";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 3)
                    {
                        Stu = "Is Sending";
                    }
                    else if (int.Parse(data.Rows[i][11].ToString()) == 4)
                    {
                        Stu = "Sent";
                    }
                    Sending += i + 1 + "." + "ID : " + data.Rows[i][0].ToString() + "   Origin : " + data.Rows[i][1].ToString() + "   Destination : " + data.Rows[i][2].ToString() + "   Type : " + Ty + "   Post Type : " + PoTy + "\nIs Expensive? " + data.Rows[i][6].ToString() + "   Is Received? " + data.Rows[i][10].ToString() + "   Weight : " + data.Rows[i][7].ToString() + "   Price : " + data.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                }
                Orders.Text = Sending;
                throw new Exception("Status has been set as 'Is Sending'");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
        private void Sent_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                string parent1 = Directory.GetParent(currentpath).ToString();
                string parent2 = Directory.GetParent(parent1).ToString();
                string path = Directory.GetParent(parent2).ToString();
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\SQL\save.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command2, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (data.Rows[0][11].ToString() == "4")
                {
                    throw new Exception("Status has been set as 'Sent'");
                }
                string u = "select * from Customer where SSN = '" + data.Rows[0][4].ToString() + "'";
                SqlDataAdapter uu = new(u, conn);
                DataTable dt = new DataTable();
                uu.Fill(dt);
                if (data.Rows.Count == 0)
                {
                    throw new Exception("ID is invalid");
                }
                string fromMail = "mmpostproject@gmail.com";
                string fromPassword = "jkkglspngxbghnfu";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = "Order's Status";
                mailMessage.To.Add(new MailAddress(dt.Rows[0][3].ToString()));
                mailMessage.Body = "<html><body> Your package by the ID : " + IDsearch.Text + " has been sent to the destination </body></html>";
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);
                String query = "UPDATE Orders SET isReceived = @c, Status = @s Where ID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(IDsearch.Text));
                command3.Parameters.AddWithValue("@s", 4);
                command3.Parameters.AddWithValue("@c", "Yes");
                command3.ExecuteNonQuery();
                conn.Close();
                Submitted.IsChecked = false;
                RTSend.IsChecked = false;
                IsSending.IsChecked = false;
                Submitted.IsEnabled = false;
                Sent.IsEnabled = false;
                RTSend.IsEnabled = false;
                IsSending.IsEnabled = false;
                string command4 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter4 = new(command4, conn);
                DataTable data4 = new();
                adapter.Fill(data4);
                string Sending = "";
                for (int i = 0; i < data4.Rows.Count; i++)
                {
                    string Ty = "", PoTy = "", Stu = "";
                    if (int.Parse(data4.Rows[i][3].ToString()) == 1)
                    {
                        Ty = "Object";
                    }
                    else if (int.Parse(data4.Rows[i][3].ToString()) == 2)
                    {
                        Ty = "Document";
                    }
                    else if (int.Parse(data4.Rows[i][3].ToString()) == 3)
                    {
                        Ty = "Fragile";
                    }
                    if (int.Parse(data4.Rows[i][5].ToString()) == 1)
                    {
                        PoTy = "Ordinary";
                    }
                    else if (int.Parse(data4.Rows[i][5].ToString()) == 2)
                    {
                        PoTy = "Speed";
                    }
                    if (int.Parse(data4.Rows[i][11].ToString()) == 1)
                    {
                        Stu = "Submitted";
                    }
                    else if (int.Parse(data4.Rows[i][11].ToString()) == 2)
                    {
                        Stu = "Ready To Send";
                    }
                    else if (int.Parse(data4.Rows[i][11].ToString()) == 3)
                    {
                        Stu = "Is Sending";
                    }
                    else if (int.Parse(data4.Rows[i][11].ToString()) == 4)
                    {
                        Stu = "Sent";
                    }
                    Sending += i + 1 + "." + "ID : " + data4.Rows[i][0].ToString() + "   Origin : " + data4.Rows[i][1].ToString() + "   Destination : " + data4.Rows[i][2].ToString() + "   Type : " + Ty + "   Post Type : " + PoTy + "\nIs Expensive? " + data4.Rows[i][6].ToString() + "   Is Received? " + data4.Rows[i][10].ToString() + "   Weight : " + data4.Rows[i][7].ToString() + "   Price : " + data4.Rows[i][9].ToString() + "   Status : " + Stu + "\n\n";
                }
                Orders.Text = Sending;
                throw new Exception("The email has been sent");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
