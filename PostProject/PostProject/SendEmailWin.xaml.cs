using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
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

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SQL\save.mdf;Initial Catalog=save;Integrated Security=True");
                conn.Open();
                string command2 = "select * from Orders where ID = '" + IDsearch.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command2, conn);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if(data.Rows.Count == 0)
                {
                    throw new Exception("ID is invalid");
                }
                string fromMail = "mmpostproject@gmail.com";
                string fromPassword = "jkkglspngxbghnfu";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = "Order's Status";
                mailMessage.To.Add(new MailAddress(fromMail));
                mailMessage.Body = "<html><body> Your package by the ID : " + IDsearch.Text + " has been sent to the destination </body></html>";
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);
                String query = "UPDATE Orders SET isReceived = @c Where ID = @id";
                SqlCommand command3 = new SqlCommand(query, conn);
                command3.Parameters.AddWithValue("@id", int.Parse(IDsearch.Text));
                command3.Parameters.AddWithValue("@c", "Yes");
                command3.ExecuteNonQuery();
                conn.Close();
                throw new Exception("The email has been sent");
            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
            }
        }
    }
}
