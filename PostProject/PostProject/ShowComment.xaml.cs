using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for ShowComment.xaml
    /// </summary>
    public partial class ShowComment : UserControl
    {
        public ShowComment()
        {
            InitializeComponent();
        }
        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Orders.Text = "";
        }
    }
}
