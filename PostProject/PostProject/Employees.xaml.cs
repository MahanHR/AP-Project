using System.Windows;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public Employees()
        {
            InitializeComponent();
        }
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new RegisterCustomerWin();
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SubmitOrderWin();
        }

        private void OrdersReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new OrderReportEmpWin();
        }

        private void PackageInfo_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new PackageInfoEmpWin();
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SendEmailWin();
        }

        private void ShowComments_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new ShowCommentWin();
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
