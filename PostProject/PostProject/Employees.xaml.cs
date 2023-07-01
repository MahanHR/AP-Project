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
using System.Windows.Shapes;

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
            ButtonShow.Content = new ShowComment();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new Login();
        }

        private void HomeB_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new EmptyWin();
        }
    }
}
