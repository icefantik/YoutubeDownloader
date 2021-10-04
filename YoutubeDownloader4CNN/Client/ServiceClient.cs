using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using YDL_Downloader;
using System.Net;
using System.ServiceModel.Channels;
using System.Globalization;
using System.Text.RegularExpressions;
using Functional;

namespace Client
{
    public class ServiceClient : ServiceReference1.IGetAudioWCFCallback
    {
        public string IpClient;
        public string ClientStatus = "NotKnown";
        private static ServiceReference1.IGetAudioWCF Service { set; get; }

        [Obsolete]
        public void ClientConnect()
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
            IpClient = ipClient;
            Service.Join(hostName, ipClient);
        }
        public List<MyTableAudioData> FunctionContext()
        {
            return Service.GetAudioData();
        }
        public List<MyTableAudioData> StartDownload(List<MyTableAudioData> audioDatas)
        {
            //List<string> Ready = new List<string>();
            SampleExtractUnit.ErrorTypes errorTypes;
            string pathJsonFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\file_paths.json";
            WorkstationPathsConfig workstationPathsConfig = new WorkstationPathsConfig(pathJsonFile); //Путь к json файлу
            SampleExtractUnit sampleExtractUnit = new SampleExtractUnit(workstationPathsConfig.Dir, "", workstationPathsConfig.Tmp, workstationPathsConfig.Sample); //df, url, load_dir, sample_dir
            for (int indexList = 0; indexList < audioDatas.Count; ++indexList)
            {
                errorTypes = sampleExtractUnit.DownloadClip("https://www.youtube.com/watch?v=" + audioDatas[indexList].Id, Convert.ToString(audioDatas[indexList].Id), Convert.ToDouble(audioDatas[indexList].Start, CultureInfo.InvariantCulture), audioDatas[indexList].End); //"https://www.youtube.com/watch?v=" + name, name, start, end
                audioDatas[indexList].Ready = Convert.ToString(errorTypes);
                //Ready.Add(Convert.ToString(errorTypes));
            }
            return audioDatas;
        }
        public void MessageResult(List<Functional.MyTableAudioData> audionDatas)
        {
            throw new NotImplementedException();
        }

        public void ListUpdated(List<Functional.User> userList)
        {
            
        }
        public void ClientDisconnect()
        {
            Service.Disconnect(Dns.GetHostName(), IpClient, ClientStatus); 
            Service = null;
        }
    }
}
