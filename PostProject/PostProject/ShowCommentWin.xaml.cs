using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class ShowCommentWin : UserControl
    {
        public string EmployeeID = "";
        public ShowCommentWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
        }
    }
}
