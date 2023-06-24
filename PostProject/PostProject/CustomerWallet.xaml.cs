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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
