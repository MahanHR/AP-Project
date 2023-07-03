using System.Windows.Controls;

namespace PostProject
{
    /// <summary>
    /// Interaction logic for RegisterCustomerWin.xaml
    /// </summary>
    public partial class RegisterCustomerWin : UserControl
    {
        public string EmployeeID = "";
        public RegisterCustomerWin(string inp)
        {
            EmployeeID = inp; 
            InitializeComponent();
        }

        public void TextBox_Name(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_LName(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_Email(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_SSNNumber(object sender, TextChangedEventArgs e)
        {

        }

        public void TextBox_Number(object sender, TextChangedEventArgs e)
        {

        }
    }
}
