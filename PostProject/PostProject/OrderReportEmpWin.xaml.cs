using System.Windows;
using System.Windows.Controls;

namespace PostProject
{
    public partial class OrderReportEmpWin : UserControl
    {
        public OrderReportEmpWin()
        {
            InitializeComponent();
        }

        private void Serach_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SSN.IsChecked == true)
            {
                SBox.IsEnabled = true;
            }

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (PackageType.IsChecked == true)
            {
                Object.IsEnabled = true;
                Document.IsEnabled = true;
                Fragile.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Price.IsChecked == true)
            {
                pBox.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Weight.IsChecked == true)
            {
                wBox.IsEnabled = true;
            }
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            if (PostType.IsChecked == true)
            {
                Ordinary.IsEnabled = true;
                Speed.IsEnabled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Object_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Document_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Fragile_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Ordinary_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Speed_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SSN_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SSN.IsChecked == false)
            {
                SBox.IsEnabled = false;
            }
        }

        private void PackageType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PackageType.IsChecked == false)
            {
                Object.IsEnabled = false;
                Document.IsEnabled = false;
                Fragile.IsEnabled = false;
            }
        }

        private void Price_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Price.IsChecked == false)
            {
                pBox.IsEnabled = false;
            }
        }

        private void Weight_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Weight.IsChecked == false)
            {
                wBox.IsEnabled = false;
            }
        }

        private void PostType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PostType.IsChecked == false)
            {
                Ordinary.IsEnabled = false;
                Speed.IsEnabled = false;
            }
        }

        private void Object_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Document_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Fragile_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Ordinary_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Speed_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
