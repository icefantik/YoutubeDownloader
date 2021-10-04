using Functional;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceReference1.IGetAudioWCFCallback
    {
        private List<MyTableAudioData> audioDatas = new List<MyTableAudioData>();
        private ServiceClient serviceClient = new ServiceClient();
        public MainWindow()
        {
            InitializeComponent();
            //var temp = Resources.Keys;
        }

        private void RequestList_Click(object sender, RoutedEventArgs e)
        {
            audioDatas = serviceClient.FunctionContext(); //Получаем лист от сервера
            audioTable.ItemsSource = audioDatas;
        }

        private void DownloadList_Click(object sender, RoutedEventArgs e)
        {
            audioDatas = serviceClient.StartDownload(audioDatas);
            audioTable.ItemsSource = null; //возможно надо будет переделать, потому что медленно работает
            audioTable.ItemsSource = audioDatas;
        }

        [Obsolete]
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            serviceClient.ClientConnect(); //Соединяемся с хостом
        }

        public void MessageResult(List<MyTableAudioData> audionDatas)
        {
            throw new NotImplementedException();
        }

        public void ListUpdated(List<User> userList)
        {
            throw new NotImplementedException();
        }

        public string getClientMask()
        {
            throw new NotImplementedException();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            serviceClient.ClientDisconnect();
        }
    }
}
