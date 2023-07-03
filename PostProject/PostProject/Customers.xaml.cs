using System.Windows;

namespace PostProject
{
    public partial class Customers : Window
    {
        public string CustomerID = "";
        public Customers()
        {
            InitializeComponent();
        }

        public void HomeB_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new EmptyWin();
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            var p = new MainWindow();
            p.Show();
            this.Close();
        }

        public void EditUandP_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new EditUandPWin(CustomerID);
        }

        public void CustomerWallet_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new CustomerWalletWin(CustomerID);
        }

        public void PackageInfo_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new PackageInfoWin(CustomerID);
        }

        public void OrderReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonShow.Content = new OrderReportWin(CustomerID);
        }
    }
}
