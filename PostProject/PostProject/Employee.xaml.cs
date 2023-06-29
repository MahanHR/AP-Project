using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new RegisterCustomer();
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SubmitOrder();
        }

        private void OrdersReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new OrdersReportEmployee();
        }

        private void PackageInfo_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new PackageInfoEmployee();
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new SendEmail();
        }

        private void ShowComments_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new ShowComment();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Login();
        }

        private void HomeB_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new Empty();
        }
    }
}
