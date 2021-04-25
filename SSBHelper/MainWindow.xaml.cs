using SSBHelper.Common;
using SSBHelper.View;
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

namespace SSBHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {                
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.CreateDirStructure();
            Utils.InitializeApp();
            btnPPDT.IsChecked = true;
        }

        private void BtnPPDT_Checked(object sender, RoutedEventArgs e)
        {
            UnChecckOtherButtons(Exams.PPDT);
            PPDT ppdtView = new PPDT();
            windowContainer.Navigate(ppdtView);
        }        

        private void BtnTAT_Checked(object sender, RoutedEventArgs e)
        {
            UnChecckOtherButtons(Exams.TAT);
            TAT tatView = new TAT();
            windowContainer.Navigate(tatView);
        }

        private void BtnWAT_Checked(object sender, RoutedEventArgs e)
        {
            UnChecckOtherButtons(Exams.WAT);
            WAT watView = new WAT();
            windowContainer.Navigate(watView);
        }

        private void BtnSRT_Checked(object sender, RoutedEventArgs e)
        {
            UnChecckOtherButtons(Exams.SRT);
            SRT srtView = new SRT();
            windowContainer.Navigate(srtView);
        }
        private void UnChecckOtherButtons(Exams exam)
        {
            btnSRT.Checked -= BtnSRT_Checked;
            btnSRT.IsChecked = Exams.SRT == exam;
            btnSRT.Checked += BtnSRT_Checked;

            btnPPDT.Checked -= BtnPPDT_Checked;
            btnPPDT.IsChecked = Exams.PPDT == exam;
            btnPPDT.Checked += BtnPPDT_Checked;

            btnTAT.Checked -= BtnTAT_Checked;
            btnTAT.IsChecked = Exams.TAT == exam;
            btnTAT.Checked += BtnTAT_Checked;

            btnWAT.Checked -= BtnWAT_Checked;
            btnWAT.IsChecked = Exams.WAT == exam;
            btnWAT.Checked += BtnWAT_Checked;
        }
    }
}
