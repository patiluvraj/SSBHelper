using SSBHelper.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PPDT.xaml
    /// </summary>
    public partial class PPDT : Page
    {
        static bool isRefreshed = false;
        static bool readyToStartNew = true;
        Random random = new Random();        
        public PPDT()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Utils.ReloadPPDTPictues();
            txblkTotalPictures.Text = "Total Pictures : " + Utils.MAX_PPDT_PIC_COUNT;
            txblkTotalPictures.ToolTip = "Total Available Pictures count is "+ Utils.MAX_PPDT_PIC_COUNT;
            txtPictureno.Text = Utils.GetPictureNo(Utils.PPDT_Pictures.FirstOrDefault());
        }

        private async void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if(!readyToStartNew)
                isRefreshed = true;
            await IsReadyToStartNew();
            await ShowPPDTPicture();
        }

        private async Task ShowPPDTPicture()
        {
            readyToStartNew = false;
            string filePath = string.Empty;
            try
            {
                txtMessgae.Visibility = Visibility.Collapsed;
                if (chkRandom.IsChecked.Value)                
                    filePath=GetRandomPictureToShow();                
                else
                    filePath = GetPicturePath(Convert.ToInt32(txtPictureno.Text));                

                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    Utils.VISITED_PPDT_PICTURES.Add(filePath);
                    imageViewer.Source = new BitmapImage(new Uri(filePath));
                    int defaultTime = 30;
                    defaultTime = Properties.Settings.Default.PPDT_Show_Picture_Time_In_Sec;
                    if (defaultTime == 0)
                        throw new Exception("PPDT picture show time is 0");
                    await PutTaskDelay(defaultTime * 1000);
                    if (isRefreshed)
                        return;

                    txtMessgae.Visibility = Visibility.Visible;
                    SystemSounds.Beep.Play();
                    imageViewer.Source = null;
                    txtMessgae.Text = "Start story writing.....";

                    if (isRefreshed)
                        return;

                    defaultTime = 240;
                    defaultTime = Properties.Settings.Default.PPDT_Write_Story_Time_In_Sec;
                    if (defaultTime == 0)
                        throw new Exception("PPDT story write time is 0");
                    await PutTaskDelay(defaultTime * 1000);

                    if (isRefreshed)
                        return;

                    SystemSounds.Beep.Play();
                    txtMessgae.Text = "Your time is over !!!";
                }
                else
                {
                    ShowErrorAsPictureNotFound(filePath);
                }
            }
            finally
            {
                isRefreshed = false;
                readyToStartNew = true;
            }
        }

        private void ShowErrorAsPictureNotFound(string filePath)
        {
            txtMessgae.Visibility = Visibility.Visible;
            string errorMessage = string.Empty;
            if (chkRandom.IsChecked.Value)
                errorMessage = "File not found for picture no " + txtPictureno.Text;
            else
                errorMessage = "File not found for picture no " + filePath;
            txtMessgae.Text = errorMessage;
            imageViewer.Source = null;
        }

        private string GetPicturePath(int picNo)
        {
            return Directory.GetFiles(Utils.PPDTDir).Where(i => System.IO.Path.GetFileNameWithoutExtension(i).Equals(picNo.ToString())).FirstOrDefault();
        }        

        private string GetRandomPictureToShow()
        {
            string filePath = string.Empty;
            do
            {
                if (Utils.MAX_PPDT_PIC_COUNT == Utils.VISITED_PPDT_PICTURES.Count)
                    Utils.VISITED_PPDT_PICTURES.Clear();
                int picNo = random.Next(0, Utils.MAX_PPDT_PIC_COUNT);
                filePath = Utils.PPDT_Pictures[picNo];
            }
            while (Utils.VISITED_PPDT_PICTURES.Contains(filePath));
            return filePath;
        }

        public async Task PutTaskDelay(int waitTime)
        {
            await Task.Run(() => 
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.ElapsedMilliseconds < waitTime)
                {
                    if (isRefreshed)
                        break;
                } 
            });
        }
        
        private async Task IsReadyToStartNew()
        {
            await Task.Run(() =>
            {
                while (!readyToStartNew);                
            });
        }

        private void TxtPictureno_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = Utils.IsOnlyNumbersInIt(txtPictureno.Text);
        }
        
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            isRefreshed = true;
        }
    }
}
