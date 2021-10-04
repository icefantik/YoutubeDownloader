using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Functional
{
    [ServiceContract(CallbackContract = typeof(IGetAudioWCFCallBack))] //SessionMode = SessionMode.Allowed
    public interface IGetAudioWCF
    {
        [OperationContract(IsOneWay=true)]
        void messageForClient(List<MyTableAudioData> audionDatas);
        [OperationContract(IsOneWay=false)]
        List<MyTableAudioData> GetAudioData();
        [OperationContract(IsOneWay = false)]
        List<User> GetUserList();
        [OperationContract(IsOneWay=true)]
        void Join(string UserName, string Ip);
        [OperationContract(IsOneWay=true)]
        void Disconnect(string UserName, string Ip, string clientStatus);
        [OperationContract(IsOneWay = true)]
        void SendCountDownload(int countDownload);
        [OperationContract(IsOneWay = false)]
        string getIpPattern();
    }
    public interface IGetAudioWCFCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MessageResult(List<MyTableAudioData> audionDatas);
        [OperationContract(IsOneWay = true)]
        void ListUpdated(List<User> userList);
    }
    [DataContract]
    public class User
    {
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public string clientStatus { get; set; }
    }

    public class ServiceClientInfo : User, IEquatable<ServiceClientInfo>
    {
        internal IGetAudioWCFCallBack registeredUser;
        public ServiceClientInfo(IGetAudioWCFCallBack registeredUser)
        {
            this.registeredUser = registeredUser;
            //this.userName = Convert.ToString(registeredUser);
        }

        public bool Equals(ServiceClientInfo other)
        {
            return this.userName == other.userName && this.IpAddress == other.IpAddress && this.clientStatus == other.clientStatus ? true : false;
        }
    }

    [DataContract]
    public class MyTableAudioData
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public double Start { get; set; }
        [DataMember]
        public double End { get; set; }
        [DataMember]
        public string Cats { get; set; }
        [DataMember]
        public string Ready { get; set; }
    }
}