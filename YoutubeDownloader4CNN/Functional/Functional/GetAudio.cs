using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;


namespace Functional
{
    public class GetAudio : IGetAudioWCF
    {
        public static int countInPart { get; set; }
        public static int CountConnect = 0;
        public static List<User> usersList = new List<User>();
        private static Queue<List<MyTableAudioData>> queueAudioData = new Queue<List<MyTableAudioData>>();
        public string patternIpAddress = @"(11)\.+([0-9]+)\.+([0-9]+)\.+([0-9]+)";
        public void messageForClient(List<MyTableAudioData> audionDatas)
        {
            OperationContext.Current.GetCallbackChannel<IGetAudioWCFCallBack>().MessageResult(audionDatas);
        }
        public List<MyTableAudioData> GetAudioData()
        {
            AudioDataCsv audioDataCsv = new AudioDataCsv();
            List<MyTableAudioData> audioDatas = audioDataCsv.getDataFromCsv();
            shapQueue(audioDatas);
            return audioDatas;
        }

        List<ServiceClientInfo> _callbackList = new List<ServiceClientInfo>();
        public void Join(string UserName, string Ip)
        {
            CountConnect++;
            IGetAudioWCFCallBack registeredUser = OperationContext.Current.GetCallbackChannel<IGetAudioWCFCallBack>();
            ServiceClientInfo userData = new ServiceClientInfo(registeredUser) { IpAddress = Ip, userName = UserName, clientStatus = "NotKnown"};
            if (!_callbackList.Contains(userData))
            {
                _callbackList.Add(userData);
            }

            UpdateUserList();
            foreach (var x in _callbackList)
            {
                x.registeredUser.ListUpdated(usersList);
            }
        }
        public void Disconnect(string UserName, string Ip, string clientStatus)
        {
            CountConnect--;
            IGetAudioWCFCallBack registeredUser = OperationContext.Current.GetCallbackChannel<IGetAudioWCFCallBack>();
            ServiceClientInfo userData = new ServiceClientInfo(registeredUser) { IpAddress = Ip, userName = UserName, clientStatus = clientStatus };
            if (_callbackList.Contains(userData))
                _callbackList.Remove(userData);
            
            UpdateUserList();
            foreach (var x in _callbackList)
            {
                x.registeredUser.ListUpdated(usersList);
            }
        }
        public void UpdateUserList()
        {
            usersList.Clear();
            foreach (var item in _callbackList)
            {
                User users = new User() { IpAddress = item.IpAddress, userName = item.userName };
                usersList.Add(users);
            }
        }
        public List<User> GetUserList()
        {
            return usersList;
        }

        public string getIpPattern()
        {
            return patternIpAddress;
        }
        public void SendCountDownload(int countDownload)
        {
            countInPart = countDownload;
        }
        private void shapQueue(List<MyTableAudioData> audioDatas)
        {
            countInPart = 5;
            List<MyTableAudioData> partAudioDatas = new List<MyTableAudioData>();
            for (int startCut = 0; startCut < audioDatas.Count; startCut += countInPart)
            {
                if ((startCut + countInPart) > audioDatas.Count)
                    countInPart = audioDatas.Count - startCut;
                partAudioDatas = audioDatas.GetRange(startCut, countInPart);
                queueAudioData.Enqueue(partAudioDatas);
            }
        }
    }
    public class AudioDataCsv
    {
        private static int indexDataCsv = 0;
        public string Cat { get; set; }
        public List <MyTableAudioData> getDataFromCsv()
        {
            List<MyTableAudioData> audioDatas = new List<MyTableAudioData>();
            string audioDataCvs, pattern = "([^\\s]+),\\s+(\\d+.\\d+),\\s+(\\d+.\\d+),\\s+[\"]+([0-9a-z,/\"_]+)[\"]+";
            string pathCsv = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\input_data.csv";
            using (TextFieldParser parser = new TextFieldParser(pathCsv))
            {
                for (; (!parser.EndOfData); ++indexDataCsv)
                {
                    audioDataCvs = parser.ReadLine();
                    Match match = Regex.Match(audioDataCvs, pattern);
                    if (Regex.IsMatch(audioDataCvs, pattern, RegexOptions.IgnoreCase)) //Проверка на соответстие строки audioData шаблону
                    {
                        audioDatas.Add(new MyTableAudioData() { Id = match.Groups[1].Value, Start = Convert.ToDouble(match.Groups[2].Value, CultureInfo.InvariantCulture), 
                            End = Convert.ToDouble(match.Groups[3].Value, CultureInfo.InvariantCulture), Cats = match.Groups[4].Value, Ready = "Not download yet"}); //Ready = "unset"
                    }
                }
            }
            return audioDatas;
        }
    }
}