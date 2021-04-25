using SSBHelper.Common;
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

namespace SSBHelper.View
{
    /// <summary>
    /// Interaction logic for TAT.xaml
    /// </summary>
    public partial class TAT : Page
    {
        public TAT()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtPictureno_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = Utils.IsOnlyNumbersInIt(txtPictureno.Text);
        }
    }
}
