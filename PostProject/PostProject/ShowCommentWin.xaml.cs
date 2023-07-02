using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class ShowCommentWin : UserControl
    {
        public ShowCommentWin()
        {
            InitializeComponent();
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
        }
    }
}
