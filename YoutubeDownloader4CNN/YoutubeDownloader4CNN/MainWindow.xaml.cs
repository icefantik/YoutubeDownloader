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
using YDL_Downloader;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.ServiceModel;
using Functional;
using System.Net;
using System.IO;

namespace YoutubeDownloader4CNN
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window, ServiceReference1.IGetAudioWCFCallback
    {
        private static ServiceReference1.IGetAudioWCF Service { set; get; }
        public MainWindow()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void StartDownload(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("http://localhost:8080/");
            WSDualHttpBinding binding = new WSDualHttpBinding();
            DuplexChannelFactory<ServiceReference1.IGetAudioWCF> factory = new DuplexChannelFactory<ServiceReference1.IGetAudioWCF>(this, binding, address);
            Service = factory.CreateChannel();
            string hostName = Dns.GetHostName(), ipClient = "", ipPattern = Service.getIpPattern();
            IPHostEntry ipv4 = Dns.GetHostByName(hostName);
            foreach (var item in ipv4.AddressList)
            {
                if (Regex.IsMatch(item.ToString(), ipPattern))
                {
                    ipClient = item.ToString(); break;
                }
            }
            Service.Join(hostName, ipClient);
            //Service.GetAudioData();
            //List<User> userList = Service.GetUserList();
            //foreach (var item in userList.Distinct())
            //{
            //    int countUserNames = userList.Where(x => x.userName == item.userName).Count();
            //    if (countUserNames > 1)
            //    {
            //        MessageBox.Show("Idi otsudava " + item.userName); 
            //        //если у нам больше одного пользователя, то надо будет обновить список
            //    }
            //}
            //foreach (var item in userList)
            //userDatas.Items.Add(item);
            Service = factory.CreateChannel();
            List<MyTableAudioData> listAudioDatas = Service.GetAudioData();

            SampleExtractUnit.ErrorTypes errorTypes;
            string pathJsonFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\file_paths.json";
            WorkstationPathsConfig workstationPathsConfig = new WorkstationPathsConfig(pathJsonFile); //Путь к json файлу
            SampleExtractUnit sampleExtractUnit = new SampleExtractUnit(workstationPathsConfig.Dir, "", workstationPathsConfig.Tmp, workstationPathsConfig.Sample); //df, url, load_dir, sample_dir
            for (int indexList = 0; indexList < listAudioDatas.Count; ++indexList)
            {
                errorTypes = sampleExtractUnit.DownloadClip("https://www.youtube.com/watch?v=" + listAudioDatas[indexList].Id, Convert.ToString(listAudioDatas[indexList].Id), Convert.ToDouble(listAudioDatas[indexList].Start, CultureInfo.InvariantCulture), listAudioDatas[indexList].End); //"https://www.youtube.com/watch?v=" + name, name, start, end
                listAudioDatas[indexList].Ready = Convert.ToString(errorTypes);
            }
            audioDatas.ItemsSource = listAudioDatas;
        }

        private void FinishDownload(object sender, RoutedEventArgs e)
        {

        }

        private void RequestStatus(object sender, RoutedEventArgs e)
        {

        }

        public void MessageResult(List<MyTableAudioData> audionDatas)
        {
            throw new NotImplementedException();
        }

        public void ListUpdated(List<User> userList)
        {
            foreach (var item in userList)
            {
                item.clientStatus = Convert.ToString(ClientStatus.NotBusy);
            }
            userDatas.ItemsSource = userList;
        }

        private void BoxCountDowload_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BoxCountDowload_LostFocus(object sender, RoutedEventArgs e)
        {
            //e.OriginalSource
            Service.SendCountDownload(Convert.ToInt32(BoxCountDowload.Text));
        }
    }
}
