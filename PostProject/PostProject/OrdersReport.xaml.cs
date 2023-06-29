using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for OrdersReport.xaml
    /// </summary>
    public partial class OrdersReport : UserControl
    {
        public OrdersReport()
        {
            InitializeComponent();
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
        }
    }
}
