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
using System.Windows.Shapes;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        public void TextBox_Name(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_LName(object sender, TextChangedEventArgs e)
        {

        }
        public void PasswordBox_Confirm(object sender, RoutedEventArgs e)
        {

        }
        public void TextBox_Email(object sender, TextChangedEventArgs e)
        {

        }
        public void TextBox_PersonnelID(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_UName(object sender, TextChangedEventArgs e)
        {

        }
        public void PasswordBox_Pass(object sender, RoutedEventArgs e)
        {

        }
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            var p = new MainWindow();
            p.Show();
            this.Close();
        }
    }
}
