using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for CustomerWallet.xaml
    /// </summary>
    public partial class CustomerWallet : UserControl
    {
        public CustomerWallet()
        {

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
            Balance.Text = "Your Balance is : ";
        }
    }
}
