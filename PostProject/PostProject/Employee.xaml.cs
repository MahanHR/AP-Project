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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PackageInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowComments_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new ShowComment();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var p = new MainWindow();
            p.Show();
            this.Close();
        }

        private void HomeB_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new Empty();
        }
    }
}
