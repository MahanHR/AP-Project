using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new SignUpEmployee();
        }
    }
}
