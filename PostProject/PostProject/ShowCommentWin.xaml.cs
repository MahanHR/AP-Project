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
    /// Interaction logic for ShowCommentWin.xaml
    /// </summary>
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
