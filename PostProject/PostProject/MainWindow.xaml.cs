using System.Windows;

namespace PostProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = new Login();
        }
    }
}
