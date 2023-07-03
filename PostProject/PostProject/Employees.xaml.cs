using System.Windows;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public string EmployeeID = "";
        public Employees()
        {
            InitializeComponent();
        }
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new RegisterCustomerWin(EmployeeID);
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SubmitOrderWin(EmployeeID);
        }

        private void OrdersReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new OrderReportEmpWin(EmployeeID);
        }

        private void PackageInfo_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new PackageInfoEmpWin(EmployeeID);
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SendEmailWin(EmployeeID);
        }

        private void ShowComments_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new ShowCommentWin(EmployeeID);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var p = new MainWindow();
            p.Show();
            this.Close();
        }

        private void HomeB_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new EmptyWin();
        }
    }
}
