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

        private void alaki2_Click(object sender, RoutedEventArgs e)
        {
            var p = new Employee();
            p.Show();
            this.Close();
        }
    }
}
