using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Page
    {
        public Customer()
        {
            InitializeComponent();
        }
        public void HomeB_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
        public void EditUandP_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new EditUnP();
        }
        public void CustomerWallet_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new CustomerWallet();
        }
        public void PackageInfo_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new PackageInfo();
        }
        public void OrderReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new OrdersReport();
        }
    }
}
