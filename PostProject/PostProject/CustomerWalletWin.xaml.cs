using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class CustomerWalletWin : UserControl
    {
        public string CustomerID = "";
        public CustomerWalletWin(string ID)
        {
            CustomerID = ID;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowBalance_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
