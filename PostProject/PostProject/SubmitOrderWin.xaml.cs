using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class SubmitOrderWin : UserControl
    {
        public string EmployeeID = "";
        public SubmitOrderWin(string inp)
        {
            EmployeeID = inp;
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
