using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net;
using Functional;
using System.ServiceModel.Channels;

namespace HostServer
{
    class Server
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(GetAudio)))
            {
                host.Open();
                string url = host.BaseAddresses[0].AbsoluteUri;
                Console.WriteLine("> Host started...");
                Console.WriteLine("IP: " + GetUriToIp(url));
                Console.ReadLine();
                /**
                foreach (var item in GetAudio.maskUsers) //просто проверка того что лист заполнился
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
                */
            }
        }
        static string GetUriToIp(string url)
        {
            Uri uri = new Uri(url);
            IPHostEntry entry = Dns.GetHostEntry(uri.Host);
            IPAddress e = entry.AddressList[0];
            string ip = e.ToString();
            return ip;
        }
    }
}
