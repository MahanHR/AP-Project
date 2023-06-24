using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class SignUpEmployee : Page
    {
        public SignUpEmployee()
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
            Application.Current.MainWindow.Content = new Login();
        }
    }
}
